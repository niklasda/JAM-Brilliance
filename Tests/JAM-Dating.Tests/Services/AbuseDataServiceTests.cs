﻿using System.Data;

using FakeItEasy;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JAM.Core.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JAM.Brilliance.Tests.Services
{
    [TestClass]
    public class AbuseDataServiceTests
    {
        [TestMethod]
        public void TestReportMessageAbuse()
        {
            var cn = A.Fake<IDbConnection>();
            var dcs = A.Fake<IDatabaseConfigurationService>();
            var ads = new AbuseDataService(dcs);

            A.CallTo(() => dcs.CreateConnection()).Returns(cn);
            ads.ReportMessageAbuse(A.Dummy<AbuseReport>());

            A.CallTo(() => dcs.CreateConnection()).MustHaveHappened();
        }
    }
}
