using Core.Services.Interfaces.DapperServices.Interfaces;
using Dapper;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Services.DapperServices
{
    public class ThirdPartyDapperService : IThirdPartyDepperService, IDisposable
    {
        private readonly IDbConnection db;
        public ThirdPartyDapperService(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("InsConnection"));
        }

        public async Task<List<ThirdParty>> GetThirdPartiesAsync()
        {
            string query = "Select * From ThirdParties";
            IEnumerable<ThirdParty> thirdParties = await db.QueryAsync<ThirdParty>(query);
            return thirdParties.ToList();
        }
        public async Task<List<ThirdPartyStatus>> GetThirdPartyStatusesAsync(int tpId)
        {
            string query = "Select * From ThirdPartyStatuses";
            IEnumerable<ThirdPartyStatus> thirdPartyStatuses = await db.QueryAsync<ThirdPartyStatus>(query);
            return thirdPartyStatuses.ToList();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        
    }
}
