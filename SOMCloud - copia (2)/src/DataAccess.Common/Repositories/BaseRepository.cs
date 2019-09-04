using Group.Salto.Common;
using Group.Salto.Infrastructure.Common;
using Group.Salto.Infrastructure.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Common
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IUnitOfWork _uow;

        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(IUnitOfWork));
        }

        public void Dispose() { }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _uow.Context.Set<TEntity>();
            }
        }

        #region All

        public virtual IQueryable<TEntity> All()
        {
            return DbSet.AsQueryable();
        }

        protected virtual IQueryable<TEntity> All(List<Expression<Func<TEntity, object>>> includes)
        {
            return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include));
        }

        #endregion

        #region Filter

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        protected virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes)
        {
            return includes.Aggregate(DbSet.Where(predicate).AsQueryable(), (current, include) => current.Include(include));
        }

        #endregion 

        #region Find

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.SingleOrDefault(predicate);
        }

        protected virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes)
        {
            return includes.Aggregate(DbSet.Where(predicate).AsQueryable(), (current, include) => current.Include(include)).FirstOrDefault();
        }

        #endregion

        #region Create & Update

        public virtual TEntity Create(TEntity TEntity)
        {
            var result = DbSet.Add(TEntity);
            return result.Entity;
        }

        public virtual void Update(TEntity TEntity)
        {
            var entry = _uow.Context.Entry(TEntity);

            DbSet.Attach(TEntity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity TEntity)
        {
            var entry = _uow.Context.Entry(TEntity);

            DbSet.Remove(TEntity);
            entry.State = EntityState.Deleted;
        }

        public bool CreateRange(IEnumerable<TEntity> collectionEntities)
        {
            try
            {
                _uow.Context.Set<TEntity>().AddRange(collectionEntities);
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteRange(IEnumerable<TEntity> collectionEntities)
        {
            try
            {
                List<TEntity> collectionListEntities = collectionEntities.ToList();

                foreach (TEntity collectionEntity in collectionListEntities)
                {
                    _uow.Context.Entry(collectionEntity).State = EntityState.Deleted;
                }

                _uow.Context.Set<TEntity>().RemoveRange(collectionListEntities);
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region ExecuteRawSQL

        public List<List<DataBaseResultDto>> ExecuteRawSQL(string sql, List<SqlParameter> parameterList)
        {
            List<List<DataBaseResultDto>> result = new List<List<DataBaseResultDto>>();
            using (var command = _uow.Context.Database.GetDbConnection().CreateCommand())
            {
                command.Parameters.AddRange(parameterList.ToArray());
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 120;
                command.Connection.Open();
                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                {
                    List<DataBaseResultDto> rowResult = null;
                    while (reader.Read())
                    {
                        rowResult = new List<DataBaseResultDto>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            object value = reader[i];
                            rowResult.Add(new DataBaseResultDto() { Value = value, Name = name });
                        }
                        result.Add(rowResult);
                    }

                    if (command.Connection != null && command.Connection.State != ConnectionState.Closed && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }

                if (command.Connection.State != ConnectionState.Closed)
                {
                    command.Connection.Close();
                }
            }
            return result;
        }

        public object ExecuteScalarSQL(string sql, List<SqlParameter> parameterList)
        {
            object recordCount = 0;
            using (var command = _uow.Context.Database.GetDbConnection().CreateCommand())
            {
                command.Parameters.AddRange(parameterList.ToArray());
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 120;
                command.Connection.Open();

                recordCount = command.ExecuteScalar();

                if (command.Connection.State != ConnectionState.Closed)
                {
                    command.Connection.Close();
                }
            }
            return recordCount;
        }

        #endregion

        public SaveResult<TEntity> SaveChanges()
        {
            return SaveChange(null);
        }

        protected SaveResult<TEntity> SaveChange(TEntity entity)
        {
            var result = new SaveResult<TEntity>(entity);
            try
            {
                var resultSave = _uow.Context.SaveChanges();
                if (resultSave <= 0)
                {
                    result.IsOk = false;
                    result.Error = new SaveError
                    {
                        ErrorMessage = "Not change has persisted.",
                        ErrorType = ErrorType.SaveChangesNoRows
                    };
                }
            }
            catch (Exception ex)
            {
                result.IsOk = false;
                result.Error = new SaveError
                {
                    ErrorMessage = $"Exception in SaveChanges: {ex.Message}",
                    ErrorType = ErrorType.SaveChangesException,
                };
            }
            return result;
        }
    }
}