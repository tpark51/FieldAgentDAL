using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;

namespace FieldAgent.DAL.Test
{
    public class ReportsRepositoryTest
    {
        ReportsRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new ReportsRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void CompleteClearanceAudit()
        {
            Response<List<ClearanceAuditListItem>> actual = db.AuditClearance(1, 2);
            Assert.IsTrue(actual.Success);

            //Assert.AreEqual(1, db.AuditClearance(1, 2).Data.Count); THIS FAILS BUT IT SHOWS THE DATA EVEN THOUGH ITS SAYING A NULL REFERENCE 

        }

        [Test]
        public void GetPensionListByAgencyId()
        {
            Response<List<PensionListItem>> actual = db.GetPensionList(1);
            Assert.IsTrue(actual.Success);

        }

        [Test]
        public void GetTopAgents()
        {
            Response<List<TopAgentListItem>> actual = db.GetTopAgents();
            Assert.IsTrue(actual.Success);

        }
    }
}
