using FieldAgent.Core.Entities;
using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class AgencyAgentRepoTest
    {
        AgencyAgentRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgencyAgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }


        [Test]
        public void TestGetOneAgencyAgent()
        {
            Assert.AreEqual(1, db.Get(2, 1).Data.AgentId);
            Assert.AreEqual(2, db.Get(2, 1).Data.AgencyId);
        }

        [Test]
        public void TestDeleteAgencyAgent()
        {
            db.Delete(2, 1);
            Assert.AreEqual(null, db.Get(2, 1).Data);

        }

        [Test]
        public void TestUpdateAgencyAgent()
        {
            AgencyAgent agencyAgent = db.Get(2, 1).Data;
            agencyAgent.SecurityClearanceId = 1;
            db.Update(agencyAgent);

            Assert.AreEqual(agencyAgent.AgencyId, db.Get(2,1).Data.AgencyId);
            Assert.AreEqual(agencyAgent.AgentId, db.Get(2, 1).Data.AgentId);
            Assert.AreEqual(1, db.Get(2,1).Data.SecurityClearanceId);
        }

        [Test]
        public void TestAddNewLocation()
        {
            AgencyAgent expected = new AgencyAgent
            {
                AgencyId = 1,
                AgentId = 4,
                SecurityClearanceId = 1,
                BadgeId = new Guid(),
                ActivationDate = DateTime.Now,
                IsActive = true
            };

            db.Insert(expected);

            Assert.AreEqual(4, db.Get(1,4).Data.AgentId);
            Assert.AreEqual(1, db.Get(1,4).Data.SecurityClearanceId);

        }
        [Test]
        public void TestGetAgencyAgentsByAgencyId()
        {
            Assert.AreEqual(1, db.GetByAgency(1).Data.Count);
            Assert.AreEqual(3, db.GetByAgency(2).Data.Count);
        }

        [Test]
        public void TestGetAgencyAgentsByAgentId()
        {
            Assert.AreEqual(1, db.GetByAgent(1).Data.Count);
            Assert.AreEqual(1, db.GetByAgent(2).Data.Count);
            Assert.AreEqual(1, db.GetByAgent(3).Data.Count);
            Assert.AreEqual(1, db.GetByAgent(4).Data.Count);
        }
    }
}
