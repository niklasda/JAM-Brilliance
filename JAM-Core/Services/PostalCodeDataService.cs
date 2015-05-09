using System.Linq;

using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class PostalCodeDataService : IPostalCodeDataService
    {
        protected readonly IDatabaseConfigurationService Config;

        public PostalCodeDataService(IDatabaseConfigurationService configurationService)
        {
            Config = configurationService;
        }

        public PostalCodeInfo GetPostalCodeInfo(string postalCode)
        {
            const string sqlLookupPostalCode = "SELECT * FROM PostalCodes WHERE PostalCode = @PostalCode";
            using (var cn = Config.CreateConnection())
            {
                var pci = cn.Query<PostalCodeInfo>(sqlLookupPostalCode, new { PostalCode = postalCode }).FirstOrDefault();
                return pci;
            }
        }
    }
}