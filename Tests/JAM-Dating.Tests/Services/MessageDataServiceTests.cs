using System.Data;

using FakeItEasy;
using JAM.Core.Interfaces;
using JAM.Core.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JAM.Brilliance.Tests.Services
{
    [TestClass]
    public class MessageDataServiceTests
    {
        [TestMethod]
        public void TestDeleteMessage()
        {
            var cn = A.Fake<IDbConnection>();
            var dcs = A.Fake<IDatabaseConfigurationService>();
            var mds = new MessageDataService(dcs);

            A.CallTo(() => dcs.CreateConnection()).Returns(cn);
            mds.DeleteMessage(0, 0);

            A.CallTo(() => dcs.CreateConnection()).MustHaveHappened();
        }
    }
}