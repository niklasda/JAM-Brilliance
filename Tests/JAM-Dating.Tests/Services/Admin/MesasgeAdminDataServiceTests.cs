using System.Data;

using FakeItEasy;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JAM.Core.Services.Admin;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JAM.Brilliance.Tests.Services.Admin
{
    [TestClass]
    public class MessageAdminDataServiceTests
    {
        [TestMethod]
        public void TestBroadcastMessage()
        {
            var cn = A.Fake<IDbConnection>();
            var dc = A.Fake<IDataCache>();
            var dcs = A.Fake<IDatabaseConfigurationService>();

            var mads = new MessageAdminDataService(dc, dcs);

            A.CallTo(() => dcs.CreateConnection()).Returns(cn);
            mads.BroadcastMessage(A.Dummy<SendMessage>());

            A.CallTo(() => dcs.CreateConnection()).MustHaveHappened();
        }
    }
}