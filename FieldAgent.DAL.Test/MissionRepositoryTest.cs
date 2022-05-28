using FieldAgent.Core.Entities;
using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class MissionRepositoryTest
    {
        MissionRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new MissionRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGetOneMission()
        {
            Assert.AreEqual("Laced", db.Get(1).Data.CodeName);
        }

        [Test]
        public void TestAddNewMission()
        {
            Mission expected = new Mission
            {
                AgencyId = 2,
                CodeName = "100",
                Notes = "save him",
                StartDate = new DateTime (2010, 01, 10),
                ProjectedEndDate = new DateTime (2010, 03, 10)
            };

            db.Insert(expected);
            expected.MissionId = 3;

            Assert.AreEqual(expected.AgencyId, db.Get(3).Data.AgencyId);
            Assert.AreEqual("100", db.Get(3).Data.CodeName);
        }

        [Test]
        public void TestUpdateAgeny()
        {
            Mission mission = db.Get(2).Data;
            mission.Notes = "case closed";
            db.Update(mission);

            Assert.AreEqual(mission.MissionId, db.Get(2).Data.MissionId);
            Assert.AreEqual("case closed", db.Get(2).Data.Notes);
        }

        [Test]
        public void TestDeleteAlias()
        {
            db.Delete(1);
            Assert.AreEqual(null, db.Get(1).Data);
        }

        [Test]
        public void TestGetMissionsByAgency()
        {
            Assert.AreEqual(1, db.GetByAgency(1).Data.Count);
        }

        [Test]
        public void TestGetMissionsByAgent()
        {
            Assert.AreEqual(1, db.GetByAgent(4).Data.Count);
        }

    }
}
