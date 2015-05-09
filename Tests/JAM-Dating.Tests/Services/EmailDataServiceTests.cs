using System;
using System.Data;

using FakeItEasy;
using JAM.Core.Interfaces;
using JAM.Core.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JAM.Brilliance.Tests.Services
{
    [TestClass]
    public class EmailDataServiceTests
    {
        [TestMethod]
        public void TestGetVerificationGuid()
        {
            var cn = A.Fake<IDbConnection>();
            var dcs = A.Fake<IDatabaseConfigurationService>();
            var eds = new EmailDataService(dcs);

            A.CallTo(() => dcs.CreateConnection()).Returns(cn);
            Guid dummyToken = eds.GetVerificationGuid("test@test.com");

            Assert.AreEqual(dummyToken, Guid.Empty);
            A.CallTo(() => dcs.CreateConnection()).MustHaveHappened();
        }
    }
}