using FieldAgent.Core.Entities;
using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class AgentRepositoryTest
    {
        AgentRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGetOneAgent()
        {
            Assert.AreEqual(1, db.Get(1).Data.AgentId);
        }

        [Test]
        public void TestAddNewAgent()
        {
            Agent expected = new Agent
            {
                FirstName = "Wanda",
                LastName = "Maximoff",
                DateOfBirth = new DateTime(1990, 2, 10),
                Height = 67
            };

            db.Insert(expected);
            expected.AgentId = 5;

            Assert.AreEqual(expected.AgentId, db.Get(5).Data.AgentId);
        }

        [Test]
        public void TestUpdateAgeny()
        {
            Agent agent = db.Get(1).Data;
            agent.FirstName = "Aaron";
            agent.LastName = "Hotchner";
            agent.DateOfBirth = new DateTime(1969, 11, 2);
            agent.Height = 73;
            db.Update(agent);

            Assert.AreEqual(agent.AgentId, db.Get(1).Data.AgentId);
            Assert.AreEqual(agent.FirstName, db.Get(1).Data.FirstName);
        }

        [Test]
        public void TestDeleteAgent()
        {
            db.Delete(1);
            Assert.AreEqual(null, db.Get(1).Data);
        }

        [Test]
        public void TestGetMissionsByAgentId()
        {
            Assert.AreEqual(1, db.GetMissions(1).Data.Count);
        }



    }
}