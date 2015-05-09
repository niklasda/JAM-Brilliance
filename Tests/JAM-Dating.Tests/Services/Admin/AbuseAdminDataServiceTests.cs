using System.Data;

using FakeItEasy;
using JAM.Core.Interfaces;
using JAM.Core.Services.Admin;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JAM.Brilliance.Tests.Services.Admin
{
    [TestClass]
    public class AbuseAdminDataServiceTests
    {
        [TestMethod]
        public void TestDeleteOldHandledAbuse()
        {
            var cn = A.Fake<IDbConnection>();
            var dcs = A.Fake<IDatabaseConfigurationService>();
            var aads = new AbuseAdminDataService(dcs);

            A.CallTo(() => dcs.CreateConnection()).Returns(cn);
            aads.DeleteOldHandledAbuse();

            A.CallTo(() => dcs.CreateConnection()).MustHaveHappened();
        }
    }
}