using Dapper;
using Group.Salto.DataAccess.Context;
using Group.Salto.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.MigrationData
{
    public class MigrationInfrastructure
    {
        private SOMContext _contextInfrastructure;
        private SqlConnection _sqlConnectionOldDatabase;

        public MigrationInfrastructure(SOMContext contextInfrastructure, SqlConnection sqlConnectionOldDatabase)
        {
            _contextInfrastructure = contextInfrastructure;
            _sqlConnectionOldDatabase = sqlConnectionOldDatabase;
        }

        public void MapContractType(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapContractType - Registros a tratar: {dataList.Count()}");
            List<ContractType> list = dataList.Cast<ContractType>().ToList();
            List<ContractType> listFilter = new List<ContractType>();
            List<ContractType> listContext = _contextInfrastructure.ContractType.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<ContractType>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Value.Equals(item.Value)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapAvailabilityCategories(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapAvailabilityCategories - Registros a tratar: {dataList.Count()}");
            List<AvailabilityCategories> list = dataList.Cast<AvailabilityCategories>().ToList();
            List<AvailabilityCategories> listFilter = new List<AvailabilityCategories>();
            List<AvailabilityCategories> listContext = _contextInfrastructure.AvailabilityCategories.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<AvailabilityCategories>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapPredefinedDaysStates(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapPredefinedDaysStates - Registros a tratar: {dataList.Count()}");
            List<PredefinedDayStates> list = dataList.Cast<PredefinedDayStates>().ToList();
            List<PredefinedDayStates> listFilter = new List<PredefinedDayStates>();
            List<PredefinedDayStates> listContext = _contextInfrastructure.PredefinedDayStates.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PredefinedDayStates>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapPredefinedReasons(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapPredefinedReasons - Registros a tratar: {dataList.Count()}");
            List<PredefinedReasons> list = dataList.Cast<PredefinedReasons>().ToList();
            List<PredefinedReasons> listFilter = new List<PredefinedReasons>();
            List<PredefinedReasons> listContext = _contextInfrastructure.PredefinedReasons.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PredefinedReasons>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapOrigins(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapOrigins - Registros a tratar: {dataList.Count()}");
            List<Origins> list = dataList.Cast<Origins>().ToList();
            List<Origins> listFilter = new List<Origins>();
            List<Origins> listContext = _contextInfrastructure.Origins.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Origins>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapActions(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapActions - Registros a tratar: {dataList.Count()}");
            List<Actions> list = dataList.Cast<Actions>().ToList();
            List<Actions> listFilter = new List<Actions>();
            List<Actions> listContext = _contextInfrastructure.Actions.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Actions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapCities(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapCities - Registros a tratar: {dataList.Count()}");
            List<Cities> list = dataList.Cast<Cities>().ToList();
            List<Cities> listFilter = new List<Cities>();
            List<Cities> listContext = _contextInfrastructure.Cities.ToList();

            List<Municipalities> newListMunicipalities = _contextInfrastructure.Municipalities.ToList();
            List<Municipalities> oldListMunicipalities = _sqlConnectionOldDatabase.Query<Municipalities>("SELECT [Id],[Name],[MunicipalityCode],[StateId] FROM Municipalities").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Cities>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        var newMunicipality = newListMunicipalities.FirstOrDefault(x => x.Id == item.MunicipalityId);
                        var municipalityOldWithSameName = oldListMunicipalities.FirstOrDefault(x => x.Name == newMunicipality.Name);
                        if (municipalityOldWithSameName != null && newMunicipality.Id != municipalityOldWithSameName.Id) item.MunicipalityId = newMunicipality.Id;
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapCitiesOtherNames(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapCitiesOtherNames - Registros a tratar: {dataList.Count()}");
            List<CitiesOtherNames> list = dataList.Cast<CitiesOtherNames>().ToList();
            List<CitiesOtherNames> listFilter = new List<CitiesOtherNames>();
            List<CitiesOtherNames> listContext = _contextInfrastructure.CitiesOtherNames.ToList();

            List<Cities> newListCities = _contextInfrastructure.Cities.ToList();
            List<Cities> oldListCities = _sqlConnectionOldDatabase.Query<Cities>("SELECT [Id],[Name],MunicipalityId FROM Cities").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<CitiesOtherNames>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        var newCity = newListCities.FirstOrDefault(x => x.Id == item.Id);
                        var cityOldWithSameName = oldListCities.FirstOrDefault(x => x.Name == newCity.Name);
                        if (cityOldWithSameName != null && newCity.Id != cityOldWithSameName.Id) item.Id = newCity.Id;
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key, false);
        }

        public void MapCountries(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapCountries - Registros a tratar: {dataList.Count()}");
            List<Countries> list = dataList.Cast<Countries>().ToList();
            List<Countries> listFilter = new List<Countries>();
            List<Countries> listContext = _contextInfrastructure.Countries.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Countries>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name.Equals(item.Name)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapMunicipalities(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapMunicipalities - Registros a tratar: {dataList.Count()}");
            List<Municipalities> list = dataList.Cast<Municipalities>().ToList();
            List<Municipalities> listFilter = new List<Municipalities>();
            List<Municipalities> listContext = _contextInfrastructure.Municipalities.ToList();

            List<States> newListStates = _contextInfrastructure.States.ToList();
            List<States> oldListStates = _sqlConnectionOldDatabase.Query<States>("SELECT [Id],[Name],RegionId FROM States").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Municipalities>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Name == item.Name))
                    {
                        var newState = newListStates.FirstOrDefault(x => x.Id == item.StateId);
                        var stateOldWithSameName = oldListStates.FirstOrDefault(x => x.Name == newState.Name);
                        if (stateOldWithSameName != null && newState.Id != stateOldWithSameName.Id) item.StateId = newState.Id;
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapPostalCodes(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapPostalCodes - Registros a tratar: {dataList.Count()}");
            List<PostalCodes> list = dataList.Cast<PostalCodes>().ToList();
            List<PostalCodes> listFilter = new List<PostalCodes>();
            List<PostalCodes> listContext = _contextInfrastructure.PostalCodes.ToList();

            List<Cities> newListCities = _contextInfrastructure.Cities.ToList();
            List<Cities> oldListCities = _sqlConnectionOldDatabase.Query<Cities>("SELECT [Id],[Name],MunicipalityId FROM Cities").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PostalCodes>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PostalCode.Equals(item.PostalCode)))
                    {
                        var newCity = newListCities.FirstOrDefault(x => x.Id == item.CityId);
                        var cityOldWithSameName = oldListCities.FirstOrDefault(x => x.Name == newCity.Name);
                        if (cityOldWithSameName != null && newCity.Id != cityOldWithSameName.Id) item.CityId = newCity.Id;
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapRegions(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapRegions - Registros a tratar: {dataList.Count()}");
            List<Regions> list = dataList.Cast<Regions>().ToList();
            List<Regions> listFilter = new List<Regions>();
            List<Regions> listContext = _contextInfrastructure.Regions.ToList();

            List<Countries> newListCountries = _contextInfrastructure.Countries.ToList();
            List<Countries> oldListCountries = _sqlConnectionOldDatabase.Query<Countries>("SELECT Id, Name FROM Countries").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<Regions>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       var newCountry = newListCountries.FirstOrDefault(x => x.Id == item.CountryId);
                       var countryOldWithSameName = oldListCountries.FirstOrDefault(x => x.Name == newCountry.Name);
                       if (countryOldWithSameName != null && newCountry.Id != countryOldWithSameName.Id) item.CountryId = newCountry.Id;
                       item.UpdateDate = DateTime.UtcNow;
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapStates(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapStates - Registros a tratar: {dataList.Count()}");
            List<States> list = dataList.Cast<States>().ToList();
            List<States> listFilter = new List<States>();
            List<States> listContext = _contextInfrastructure.States.ToList();

            List<Regions> newListRegions = _contextInfrastructure.Regions.ToList();
            List<Regions> oldListRegions = _sqlConnectionOldDatabase.Query<Regions>("SELECT Id, Name, CountryId FROM Regions").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<States>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       var newRegion = newListRegions.FirstOrDefault(x => x.Id == item.RegionId);
                       var regionOldWithSameName = oldListRegions.FirstOrDefault(x => x.Name == newRegion.Name);
                       if (regionOldWithSameName != null && newRegion.Id != regionOldWithSameName.Id) item.RegionId = newRegion.Id;
                       item.UpdateDate = DateTime.UtcNow;
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapServiceStates(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapServiceStates - Registros a tratar: {dataList.Count()}");
            List<ServiceStates> list = dataList.Cast<ServiceStates>().ToList();
            List<ServiceStates> listFilter = new List<ServiceStates>();
            List<ServiceStates> listContext = _contextInfrastructure.ServiceStates.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<ServiceStates>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       item.UpdateDate = DateTime.UtcNow;
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapRepetitionTypes(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapRepetitionTypes - Registros a tratar: {dataList.Count()}");
            List<RepetitionTypes> list = dataList.Cast<RepetitionTypes>().ToList();
            List<RepetitionTypes> listFilter = new List<RepetitionTypes>();
            List<RepetitionTypes> listContext = _contextInfrastructure.RepetitionTypes.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<RepetitionTypes>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       item.UpdateDate = DateTime.UtcNow;
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key, false);
        }

        public void MapStatesOtherNames(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapStatesOtherNames - Registros a tratar: {dataList.Count()}");
            List<StatesOtherNames> list = dataList.Cast<StatesOtherNames>().ToList();
            List<StatesOtherNames> listFilter = new List<StatesOtherNames>();
            List<StatesOtherNames> listContext = _contextInfrastructure.StatesOtherNames.ToList();

            List<States> newListStates = _contextInfrastructure.States.ToList();
            List<States> oldListStates = _sqlConnectionOldDatabase.Query<States>("SELECT [Id],[Name],RegionId FROM States").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<StatesOtherNames>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       var newState = newListStates.FirstOrDefault(x => x.Id == item.Id);
                       var stateOldWithSameName = oldListStates.FirstOrDefault(x => x.Name == newState.Name);
                       if (stateOldWithSameName != null && newState.Id != stateOldWithSameName.Id) item.Id = newState.Id;
                       item.UpdateDate = DateTime.UtcNow;
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key, false);
        }

        public void MapCalendarEventCategories(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapCalendarEventCategories - Registros a tratar: {dataList.Count()}");
            List<CalendarEventCategories> list = dataList.Cast<CalendarEventCategories>().ToList();
            List<CalendarEventCategories> listFilter = new List<CalendarEventCategories>();
            List<CalendarEventCategories> listContext = _contextInfrastructure.CalendarEventCategories.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<CalendarEventCategories>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       item.UpdateDate = DateTime.UtcNow;
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key);
        }

        public void MapUsers(IEnumerable<object> dataList, KeyValuePair<string, string> value, string nameCustomer)
        {
            Console.WriteLine($"MapUsers - Registros a tratar: {dataList.Count()}");
            var customer = _contextInfrastructure.Customers.FirstOrDefault(c => c.Name == nameCustomer);

            if (customer != null)
            {
                List<Users> list = dataList.Cast<Users>().ToList();
                List<Users> listFilter = new List<Users>();
                List<Users> listContext = _contextInfrastructure.Users.ToList();
                string emptyMail = "MailEmpty";
                string duplicateMail = "MailDuplicate";

                foreach (var item in list)
                {
                    if (!listContext.Any(u => (u.OldUserId == item.OldUserId && u.CustomerId == customer.Id)))
                    {
                        string userNameFormat = !string.IsNullOrEmpty(item.UserName) ? item.UserName.Trim() : string.Empty;
                        var existUserName = false;
                        if(listFilter.Any()) existUserName = listFilter.Any(x => x.UserName.Trim() == userNameFormat);

                        if (string.IsNullOrEmpty(item.UserName))
                        {
                            Console.WriteLine($"VACIO OldId: {item.OldUserId} - Usuario: {item.Name} {item.FirstSurname}");
                            string emptyMailValue = $"{emptyMail}{Guid.NewGuid()}@testmigration.com";
                            item.UserName = emptyMailValue;
                            item.NormalizedUserName = emptyMailValue.ToUpper();
                        }
                        else if (existUserName || listContext.Any(u => u.UserName.Trim() == item.UserName.Trim()))
                        {
                            Console.WriteLine($"DUPLICADO OldId: {item.OldUserId} - Usuario: {item.UserName}");
                            string repeatMail = $"{duplicateMail}{Guid.NewGuid()}@testmigration.com";
                            item.UserName = repeatMail;
                            item.NormalizedUserName = repeatMail.ToUpper();
                        }
                        else Console.WriteLine($"Id: {item.Id} - Usuario: {item.UserName}");
                        item.Email = item.UserName;
                        item.NormalizedUserName = item.UserName.ToUpper();
                        item.SecurityStamp = Guid.NewGuid().ToString();
                        item.Customer = customer;
                        listFilter.Add(item);
                    }
                    else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.UserName}");
                }
                listContext.Clear();
                if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key, false);
            }
            else Console.WriteLine($"Map users. El tenant {nameCustomer} no existe");
        }

        public void MapRoles(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapRoles - Registros a tratar: {dataList.Count()}");
            List<Roles> list = dataList.Cast<Roles>().ToList();
            List<Roles> listFilter = new List<Roles>();
            List<IdentityRole> listContext = _contextInfrastructure.Roles.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<Roles>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(c => c.Name.Equals(item.Name)))
                   {
                       localList.Add(item);
                   }
                   //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key, false);
        }

        public void MapActionsRoles(IEnumerable<object> dataList, KeyValuePair<string, string> value)
        {
            Console.WriteLine($"MapActionsRoles - Registros a tratar: {dataList.Count()}");
            List<ActionsRoles> list = dataList.Cast<ActionsRoles>().ToList();
            List<ActionsRoles> listFilter = new List<ActionsRoles>();
            List<ActionsRoles> listContext = _contextInfrastructure.ActionsRoles.ToList();
            List<Actions> newListActions = _contextInfrastructure.Actions.ToList();
            List<Actions> oldListActions = _sqlConnectionOldDatabase.Query<Actions>("SELECT IdAccio AS Id, [Nom] AS [Name], Descripcio AS [Description] FROM Accions").ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
               () => { return new List<ActionsRoles>(); },
               (item, state, localList) =>
               {
                   if (!listContext.Any(x => x.RoleId == item.RoleId && x.ActionId == item.ActionId))
                   {
                       var newAction = newListActions.Find(x => x.Id == item.ActionId);
                       var actionOldWithSameName = oldListActions.Find(x => x.Name == newAction.Name);
                       if (actionOldWithSameName != null && newAction.Id != actionOldWithSameName.Id) item.ActionId = newAction.Id;
                       localList.Add(item);
                   }
                   return localList;
               },
               (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            newListActions.Clear();

            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, value.Key, false);
        }

        public void MapUserRoles(string nameCustomer)
        {
            List<Users> newListUsersByCustomer = _contextInfrastructure.Users.Where(u => u.Customer.Name.Equals(nameCustomer)).ToList();
            List<IdentityRole> newListRoles = _contextInfrastructure.Roles.ToList();
            List<IdentityUserRole<string>> identityUserRoles = new List<IdentityUserRole<string>>();
            var contextUserRoles = _contextInfrastructure.UserRoles.ToList();
            var oldUsersRoles = _sqlConnectionOldDatabase.Query<Users, Roles, dynamic>("SELECT u.IdUsuari AS Id, u.NomUsuari AS UserName, p.IdPerfil AS Id, p.Nom AS Description from Usuaris u inner join Perfils p on (p.IdPerfil = u.IdPerfil)"
                , (u, r) =>
                {
                    dynamic result = new ExpandoObject();
                    result.IdUser = u.Id;
                    result.UserName = u.UserName;
                    result.IdRole = r.Id;
                    result.Description = r.Description;

                    return result;
                });

            Console.WriteLine($"MapActionsRoles - Registros a tratar: {oldUsersRoles.Count()}");

            object localLockObject = new object();
            Parallel.ForEach(oldUsersRoles,
               () => { return new List<IdentityUserRole<string>>(); },
               (item, state, localList) =>
               {
                   Users user = newListUsersByCustomer.FirstOrDefault(u => u.OldUserId.ToString() == item.IdUser);
                   string idRolSameName = newListRoles.FirstOrDefault(r => r.Name == item.Description).Id;
                   string rolId = idRolSameName != item.IdRole ? idRolSameName : item.IdRole;
                   if (!contextUserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals(rolId)))
                   {
                       localList.Add(new IdentityUserRole<string>() { RoleId = rolId, UserId = user.Id });
                   }

                   return localList;
               },
               (finalResult) => { lock (localLockObject) identityUserRoles.AddRange(finalResult); });
            newListUsersByCustomer.Clear();
            newListRoles.Clear();
            if (identityUserRoles.Any()) InsertToNewDatabaseInfrastructure(identityUserRoles, "AspNetUserRoles", false);
        }

        #region scripts

        public void MapLanguage(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapLanguage");
            List<Language> listFilter = new List<Language>();
            List<Language> listContext = _contextInfrastructure.Languages.ToList();

            if (listContext.Find(x => x.Id == 1) == null) listFilter.Add(new Language() { Id = 1, Name = "Español", CultureCode = "es", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 2) == null) listFilter.Add(new Language() { Id = 2, Name = "Catalán", CultureCode = "ca", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 3) == null) listFilter.Add(new Language() { Id = 3, Name = "Inglés", CultureCode = "en", UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapModules(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapModules");
            List<Module> listFilter = new List<Module>();
            List<Module> listContext = _contextInfrastructure.Modules.ToList();

            if (listContext.Find(x => x.Key == "IOTIntegration") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "IOTIntegration", Description = "Integración vía IOT", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "OutsourcingManagement") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "OutsourcingManagement", Description = "Gestión de empresas subcontratadas", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "FleetManagement") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "FleetManagement", Description = "Gestión de Flotas", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "AdvancedReporting") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "AdvancedReporting", Description = "Reporting avanzadas", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "InteractiveNotifications") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "InteractiveNotifications", Description = "Notificaciones interactivas", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "AutomaticDispatcher") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "AutomaticDispatcher", Description = "Dispatcher automático", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "EmailIntegration") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "EmailIntegration", Description = "Integración vía correo electrónico", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "ERPIntegration") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "ERPIntegration", Description = "Integración con ERP", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "ClientsReportingAccess") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "ClientsReportingAccess", Description = "Integración vía IOT", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "MachineLearning") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "MachineLearning", Description = "Machine Learning", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "ExpensesManagement") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "ExpensesManagement", Description = "Gestión de Gastos", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "ClientIncidencesAccess") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "ClientIncidencesAccess", Description = "Acceso de clientes a incidencias", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "GeoposicioningFunctions") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "GeoposicioningFunctions", Description = "Funciones de Geoposicionamiento", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Key == "ToolsManagement") == null) listFilter.Add(new Module() { Id = Guid.NewGuid(), Key = "ToolsManagement", Description = "Gestión de Herramientas", UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapReferenceTimeSla(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapReferenceTimeSla");
            List<ReferenceTimeSla> listFilter = new List<ReferenceTimeSla>();
            List<ReferenceTimeSla> listContext = _contextInfrastructure.ReferenceTimeSla.ToList();

            if (listContext.Find(x => x.Name == "DataActuacio") == null) listFilter.Add(new ReferenceTimeSla() { Id = Guid.NewGuid(), Name = "DataActuacio", Description = string.Empty, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "DataCreacio") == null) listFilter.Add(new ReferenceTimeSla() { Id = Guid.NewGuid(), Name = "DataCreacio", Description = string.Empty, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "DataAssignacio") == null) listFilter.Add(new ReferenceTimeSla() { Id = Guid.NewGuid(), Name = "DataAssignacio", Description = string.Empty, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "DataRecullidaInterna") == null) listFilter.Add(new ReferenceTimeSla() { Id = Guid.NewGuid(), Name = "DataRecullidaInterna", Description = string.Empty, UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapExpenseTicketStatus(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapExpenseTicketStatuses");
            List<ExpenseTicketStatus> listFilter = new List<ExpenseTicketStatus>();
            List<ExpenseTicketStatus> listContext = _contextInfrastructure.ExpenseTicketStatuses.ToList();

            if (listContext.Find(x => x.Description == "Paid") == null) listFilter.Add(new ExpenseTicketStatus() { Id = Guid.NewGuid(), Description = "Paid", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Description == "Accepted") == null) listFilter.Add(new ExpenseTicketStatus() { Id = Guid.NewGuid(), Description = "Accepted", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Description == "Pending") == null) listFilter.Add(new ExpenseTicketStatus() { Id = Guid.NewGuid(), Description = "Pending", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Description == "Finished") == null) listFilter.Add(new ExpenseTicketStatus() { Id = Guid.NewGuid(), Description = "Finished", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Description == "Rejected") == null) listFilter.Add(new ExpenseTicketStatus() { Id = Guid.NewGuid(), Description = "Rejected", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Description == "Escaled") == null) listFilter.Add(new ExpenseTicketStatus() { Id = Guid.NewGuid(), Description = "Escaled", UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapExtraFieldTypes(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapExtraFieldTypes");
            List<ExtraFieldTypes> listFilter = new List<ExtraFieldTypes>();
            List<ExtraFieldTypes> listContext = _contextInfrastructure.ExtraFieldTypes.ToList();

            if (listContext.Find(x => x.Id == 1) == null) listFilter.Add(new ExtraFieldTypes() { Id = 1, Name = "Data", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 2) == null) listFilter.Add(new ExtraFieldTypes() { Id = 2, Name = "Temps", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 3) == null) listFilter.Add(new ExtraFieldTypes() { Id = 3, Name = "Periode", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 4) == null) listFilter.Add(new ExtraFieldTypes() { Id = 4, Name = "Enter", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 5) == null) listFilter.Add(new ExtraFieldTypes() { Id = 5, Name = "Decimal", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 6) == null) listFilter.Add(new ExtraFieldTypes() { Id = 6, Name = "Boolea", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 7) == null) listFilter.Add(new ExtraFieldTypes() { Id = 7, Name = "Fitxer", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 8) == null) listFilter.Add(new ExtraFieldTypes() { Id = 8, Name = "Select", IsMandatoryVisibility = true, AllowedValuesVisibility = true, MultipleChoiceVisibility = true, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 9) == null) listFilter.Add(new ExtraFieldTypes() { Id = 9, Name = "Barcode", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 10) == null) listFilter.Add(new ExtraFieldTypes() { Id = 10, Name = "Instalation", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = true, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 11) == null) listFilter.Add(new ExtraFieldTypes() { Id = 11, Name = "Signature", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Id == 12) == null) listFilter.Add(new ExtraFieldTypes() { Id = 12, Name = "Text", IsMandatoryVisibility = true, AllowedValuesVisibility = false, MultipleChoiceVisibility = false, ErpSystemVisibility = false, UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapCalculationType(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapCalculationType");
            List<CalculationType> listFilter = new List<CalculationType>();
            List<CalculationType> listContext = _contextInfrastructure.CalculationTypes.ToList();

            if (listContext.Find(x => x.Name == "Mensual") == null) listFilter.Add(new CalculationType() { Id = Guid.NewGuid(), Name = "Mensual", NumDays = 30, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Bimestral") == null) listFilter.Add(new CalculationType() { Id = Guid.NewGuid(), Name = "Bimestral", NumDays = 60, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Trimestral") == null) listFilter.Add(new CalculationType() { Id = Guid.NewGuid(), Name = "Trimestral", NumDays = 90, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Quatrimestral") == null) listFilter.Add(new CalculationType() { Id = Guid.NewGuid(), Name = "Quatrimestral", NumDays = 120, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Semestral") == null) listFilter.Add(new CalculationType() { Id = Guid.NewGuid(), Name = "Semestral", NumDays = 180, UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapDaysType(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapDaysType");
            List<DaysType> listFilter = new List<DaysType>();
            List<DaysType> listContext = _contextInfrastructure.DaysTypes.ToList();

            if (listContext.Find(x => x.Name == "Laborables") == null) listFilter.Add(new DaysType() { Id = Guid.NewGuid(), Name = "Laborables", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Naturals") == null) listFilter.Add(new DaysType() { Id = Guid.NewGuid(), Name = "Naturals", UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapDamagedEquipment(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapDamagedEquipment");
            List<DamagedEquipment> listFilter = new List<DamagedEquipment>();
            List<DamagedEquipment> listContext = _contextInfrastructure.DamagedEquipment.ToList();

            if (listContext.Find(x => x.Name == "Número de Sèrie") == null) listFilter.Add(new DamagedEquipment() { Id = Guid.NewGuid(), Name = "Número de Sèrie", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Element") == null) listFilter.Add(new DamagedEquipment() { Id = Guid.NewGuid(), Name = "Element", UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.Name == "Mòdul") == null) listFilter.Add(new DamagedEquipment() { Id = Guid.NewGuid(), Name = "Mòdul", UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapWorkOrderColumns(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapWorkOrderColumns");
            List<WorkOrderColumns> listFilter = new List<WorkOrderColumns>();
            List<WorkOrderColumns> listContext = _contextInfrastructure.WorkOrderColumns.ToList();

            if (listContext.Find(x => x.Id == 0) == null) listFilter.Add(new WorkOrderColumns() { Id = 0, Name = "Id", Align = "left", Width = 100, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 1) == null) listFilter.Add(new WorkOrderColumns() { Id = 1, Name = "InternalIdentifier", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 2) == null) listFilter.Add(new WorkOrderColumns() { Id = 2, Name = "ExternalIdentifier", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 3) == null) listFilter.Add(new WorkOrderColumns() { Id = 3, Name = "CreationDate", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 1 });
            if (listContext.Find(x => x.Id == 4) == null) listFilter.Add(new WorkOrderColumns() { Id = 4, Name = "AssignmentTime", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 1 });
            if (listContext.Find(x => x.Id == 5) == null) listFilter.Add(new WorkOrderColumns() { Id = 5, Name = "PickUpTime", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 1 });
            if (listContext.Find(x => x.Id == 6) == null) listFilter.Add(new WorkOrderColumns() { Id = 6, Name = "FinalClientClosingTime", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 1 });
            if (listContext.Find(x => x.Id == 7) == null) listFilter.Add(new WorkOrderColumns() { Id = 7, Name = "InternalClosingTime", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 1 });
            if (listContext.Find(x => x.Id == 8) == null) listFilter.Add(new WorkOrderColumns() { Id = 8, Name = "WorkOrderStatusId", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 9) == null) listFilter.Add(new WorkOrderColumns() { Id = 9, Name = "TextRepair", Align = "left", Width = 250, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 10) == null) listFilter.Add(new WorkOrderColumns() { Id = 10, Name = "Observations", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 11) == null) listFilter.Add(new WorkOrderColumns() { Id = 11, Name = "InsertedBy", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 12) == null) listFilter.Add(new WorkOrderColumns() { Id = 12, Name = "WorkOrderType", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 13) == null) listFilter.Add(new WorkOrderColumns() { Id = 13, Name = "Project", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 14) == null) listFilter.Add(new WorkOrderColumns() { Id = 14, Name = "SaltoClient", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 15) == null) listFilter.Add(new WorkOrderColumns() { Id = 15, Name = "EndClient", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 16) == null) listFilter.Add(new WorkOrderColumns() { Id = 16, Name = "ResolutionDateSla", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 17) == null) listFilter.Add(new WorkOrderColumns() { Id = 17, Name = "Phone", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 18) == null) listFilter.Add(new WorkOrderColumns() { Id = 18, Name = "Area", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 19) == null) listFilter.Add(new WorkOrderColumns() { Id = 19, Name = "Zone", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 20) == null) listFilter.Add(new WorkOrderColumns() { Id = 20, Name = "Subzona", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 21) == null) listFilter.Add(new WorkOrderColumns() { Id = 21, Name = "SiteName", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 22) == null) listFilter.Add(new WorkOrderColumns() { Id = 22, Name = "SitePhone", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 23) == null) listFilter.Add(new WorkOrderColumns() { Id = 23, Name = "Address", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 24) == null) listFilter.Add(new WorkOrderColumns() { Id = 24, Name = "ClosingCode", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 25) == null) listFilter.Add(new WorkOrderColumns() { Id = 25, Name = "ActionDate", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = 1 });
            if (listContext.Find(x => x.Id == 26) == null) listFilter.Add(new WorkOrderColumns() { Id = 26, Name = "ResponsiblePersonName", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 27) == null) listFilter.Add(new WorkOrderColumns() { Id = 27, Name = "Queue", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 28) == null) listFilter.Add(new WorkOrderColumns() { Id = 28, Name = "City", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 29) == null) listFilter.Add(new WorkOrderColumns() { Id = 29, Name = "Province", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 30) == null) listFilter.Add(new WorkOrderColumns() { Id = 30, Name = "Manufacturer", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 31) == null) listFilter.Add(new WorkOrderColumns() { Id = 31, Name = "WorkOrderCategory", Align = "left", Width = 150, CanSort = true, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 33) == null) listFilter.Add(new WorkOrderColumns() { Id = 33, Name = "ParentWOId", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 34) == null) listFilter.Add(new WorkOrderColumns() { Id = 34, Name = "ActuationEndDate", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 35) == null) listFilter.Add(new WorkOrderColumns() { Id = 35, Name = "NumWOsInSite", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 36) == null) listFilter.Add(new WorkOrderColumns() { Id = 36, Name = "NumSubWOs", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 37) == null) listFilter.Add(new WorkOrderColumns() { Id = 37, Name = "SiteCode", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 38) == null) listFilter.Add(new WorkOrderColumns() { Id = 38, Name = "AssetSerialNumber", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 39) == null) listFilter.Add(new WorkOrderColumns() { Id = 39, Name = "AssetNumber", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 40) == null) listFilter.Add(new WorkOrderColumns() { Id = 40, Name = "Maintenance", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 41) == null) listFilter.Add(new WorkOrderColumns() { Id = 41, Name = "StandardWarranty", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 42) == null) listFilter.Add(new WorkOrderColumns() { Id = 42, Name = "ManufacturerWarranty", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 43) == null) listFilter.Add(new WorkOrderColumns() { Id = 43, Name = "ExternalWorOrderStatus", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = 2 });
            if (listContext.Find(x => x.Id == 44) == null) listFilter.Add(new WorkOrderColumns() { Id = 44, Name = "TotalWorkedTime", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 45) == null) listFilter.Add(new WorkOrderColumns() { Id = 45, Name = "OnSiteTime", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 46) == null) listFilter.Add(new WorkOrderColumns() { Id = 46, Name = "TravelTime", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 47) == null) listFilter.Add(new WorkOrderColumns() { Id = 47, Name = "Kilometers", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 48) == null) listFilter.Add(new WorkOrderColumns() { Id = 48, Name = "NumberOfVisitsToClient", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 49) == null) listFilter.Add(new WorkOrderColumns() { Id = 49, Name = "NumberOfIntervention", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 50) == null) listFilter.Add(new WorkOrderColumns() { Id = 50, Name = "MeetResolutionSLA", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 51) == null) listFilter.Add(new WorkOrderColumns() { Id = 51, Name = "MeetResponseSLA", Align = "left", Width = 80, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });
            if (listContext.Find(x => x.Id == 52) == null) listFilter.Add(new WorkOrderColumns() { Id = 52, Name = "ClosingWODate", Align = "left", Width = 150, CanSort = false, UpdateDate = DateTime.UtcNow, EditType = null });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        public void MapWorkOrderDefaultColumns(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapWorkOrderDefaultColumns");
            List<WorkOrderDefaultColumns> listFilter = new List<WorkOrderDefaultColumns>();
            List<WorkOrderDefaultColumns> listContext = _contextInfrastructure.WorkOrderDefaultColumns.ToList();

            if (listContext.Find(x => x.WorkOrderColumnId == 0) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 0, Position = 1, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 1) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 1, Position = 2, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 8) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 8, Position = 3, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 25) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 25, Position = 4, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 16) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 16, Position = 5, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 9) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 9, Position = 6, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 13) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 13, Position = 7, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 26) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 26, Position = 8, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 31) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 31, Position = 9, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 29) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 29, Position = 10, UpdateDate = DateTime.UtcNow });
            if (listContext.Find(x => x.WorkOrderColumnId == 28) == null) listFilter.Add(new WorkOrderDefaultColumns() { WorkOrderColumnId = 28, Position = 11, UpdateDate = DateTime.UtcNow });

            listContext.Clear();
            if (listFilter.Any()) InsertToNewDatabaseInfrastructure(listFilter, val.Key, false);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        #endregion

        public void InsertToNewDatabaseInfrastructure<TEntity>(List<TEntity> dataList, string table, bool identity = true) where TEntity : class
        {
            Console.WriteLine($"Inicio inserción tabla {table} - Registros a insertar: {dataList.Count()}");
            using (var transaction = _contextInfrastructure.Database.BeginTransaction())
            {
                try
                {
                    if (table == "Users") table = "AspNetUsers";
                    else if (table == "Roles") table = "AspNetRoles";
                    else if (table == "Language") table = "Languages";
                    else if (table == "ExpenseTicketStatus") table = "ExpenseTicketStatuses";
                    else if (table == "Module") table = "Modules";
                    else if (table == "CalculationType") table = "CalculationTypes"; else if (table == "DaysType") table = "DaysTypes";
                    if (identity) _contextInfrastructure.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {table} ON");
                    _contextInfrastructure.Database.ExecuteSqlCommand((string)$"ALTER TABLE {table} NOCHECK CONSTRAINT ALL");

                    _contextInfrastructure.AddRange(dataList);
                    _contextInfrastructure.SaveChanges();

                    if (identity) _contextInfrastructure.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {table} OFF");
                    _contextInfrastructure.Database.ExecuteSqlCommand((string)$"ALTER TABLE {table} WITH CHECK CHECK CONSTRAINT ALL");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (identity) _contextInfrastructure.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {table} OFF");
                    _contextInfrastructure.Database.ExecuteSqlCommand((string)$"ALTER TABLE {table} WITH CHECK CHECK CONSTRAINT ALL");
                    transaction.Commit();
                    Console.WriteLine($"Error en la inserción de la tabla {table}. {ex}");
                }
            }
            Console.WriteLine($"Fin inserción tabla {table}");
        }
    }
}