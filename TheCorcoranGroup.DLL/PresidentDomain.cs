using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using TheCorcoranGroup.DLL.Enumerators;
using TheCorcoranGroup.DLL.Interfaces;
using TheCorcoranGroup.Models;

namespace TheCorcoranGroup.DLL
{
    public class PresidentDomain : IPresidentDomain
    {
        public IList<President> GetPresidents(string name, Columns columnSort, SortDirection sortDirection)
        {
            var presidents = GetPresidentData();
            
            var deadPresidents = SortList(presidents.Where(c => c.DeathDay != null).ToList(), columnSort, sortDirection);
            var presidentsAlive = SortList(presidents.Where(c => c.DeathDay == null).ToList(), columnSort, sortDirection);

            var allPresidents = deadPresidents;
            allPresidents.AddRange(presidentsAlive);

            return string.IsNullOrEmpty(name) ? allPresidents : allPresidents.Where(c=>c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        private List<President> SortList(List<President> presidents, Columns columnSort, SortDirection sortDirection)
        {
            switch (columnSort)
            {
                case Columns.NAME:
                    return sortDirection == SortDirection.Asc
                        ? presidents.OrderBy(c => c.Name).ToList()
                        : presidents.OrderByDescending(c => c.Name).ToList();
                case Columns.BIRTHDAY:
                    return sortDirection == SortDirection.Asc
                        ? presidents.OrderBy(c => c.Birthday).ToList()
                        : presidents.OrderByDescending(c => c.Birthday).ToList();
                case Columns.BIRTHPLACE:
                    return sortDirection == SortDirection.Asc
                        ? presidents.OrderBy(c => c.Birthplace).ToList()
                        : presidents.OrderByDescending(c => c.Birthplace).ToList();
                case Columns.DEAD_DAY:
                    return sortDirection == SortDirection.Asc
                        ? presidents.OrderBy(c => c.DeathDay).ToList()
                        : presidents.OrderByDescending(c => c.DeathDay).ToList();
                case Columns.DEAD_PLACE:
                    return sortDirection == SortDirection.Asc
                        ? presidents.OrderBy(c => c.DeathPlace).ToList()
                        : presidents.OrderByDescending(c => c.DeathPlace).ToList();
                default:
                    return presidents;
            }
        }

        private List<President> GetPresidentData()
        {
            var presidents = new List<President>();
            string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string ApplicationName = "Interview - Corcoran";
            string spreadsheetId = "1i2qbKeasPptIrY1PkFVjbHSrLtKEPIIwES6m2l2Mdd8";
            string range = "!A2:E";

            
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "943542191168-vba5lflprfbfr7rjh85uhqeraua1bddq.apps.googleusercontent.com",
                ClientSecret = "N5x_60GmHwCoaDYg2cpBGmY1",
            },
             Scopes,
             "user",
             CancellationToken.None).Result;
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                presidents.AddRange(values.Select(row => new President
                {
                    Name = row[0].ToString(),
                    Birthday = DateTime.ParseExact(row[1].ToString(), "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Birthplace = row[2].ToString(),
                    DeathDay = row.Count == 5 ? DateTime.ParseExact(row[3].ToString(), "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None) 
                    : (DateTime?) null,
                    DeathPlace = row.Count == 5 ? row[4].ToString() : null
                }));
            }
            else
            {
                return new List<President>();
            }
            return presidents;
        }
    }
}
