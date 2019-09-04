using FastMember;
using Group.Salto.DataAccess.Tenant.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Group.Salto.MigrationData
{
    public class MigrationTenantCommon
    {
        private SOMTenantContext _contextTenant;
        private readonly string _connectionString;

        public MigrationTenantCommon(SOMTenantContext contextTenant, string connectionString)
        {
            _contextTenant = contextTenant;
            _connectionString = connectionString;
        }

        public void InsertToNewDatabaseTenant<TEntity>(IEnumerable<TEntity> dataList, string table, bool identity = true) where TEntity : class
        {
            Console.WriteLine($"Inicio inserción tabla {table} - Registros a insertar: {dataList.Count()}");
            using (var transaction = _contextTenant.Database.BeginTransaction())
            {
                try
                {
                    if (identity) _contextTenant.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {table} ON");
                    _contextTenant.Database.ExecuteSqlCommand((string)$"ALTER TABLE {table} NOCHECK CONSTRAINT ALL");

                    _contextTenant.AddRange(dataList);
                    _contextTenant.SaveChanges();

                    if (identity) _contextTenant.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {table} OFF");
                    _contextTenant.Database.ExecuteSqlCommand((string)$"ALTER TABLE {table} WITH CHECK CHECK CONSTRAINT ALL");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la inserción de la tabla {table}. {ex}");
                }
            }
            dataList = null;
            Console.WriteLine($"Fin inserción tabla {table}");
        }

        public void SQLBulkCopyToNewDatabaseTenant<TEntity>(IEnumerable<TEntity> dataList, string table, string[] copyParameters, bool identity = true) where TEntity : class
        {
            Console.WriteLine($"Inicio inserción tabla {table} - Registros a insertar: {dataList.Count()}");

            try
            {
                using (ObjectReader reader = ObjectReader.Create(dataList, copyParameters))
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(_connectionString, ((identity)) ? SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.TableLock : SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.TableLock))
                {
                    sqlBulk.BulkCopyTimeout = 0;
                    sqlBulk.DestinationTableName = table;

                    sqlBulk.WriteToServer(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la inserción de la tabla {table}. {ex}");
            }

            dataList = null;
            Console.WriteLine($"Fin inserción tabla {table}");
        }        
    }
}