using FieldAgent.Core.Entities;
using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.DAL.Test
{
    public class AgencyRepositoryTest
    {
        AgencyRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgencyRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGetOneAgency()
        {
            Assert.AreEqual("CIA", db.Get(1).Data.ShortName);
        }

        [Test]
        public void TestGetAllAgencies()
        {
            Assert.AreEqual(3, db.GetAll().Data.Count);
        }

        [Test]
        public void TestAddNewAgency()
        {
            Agency expected = new Agency
            {
                ShortName = "NSA",
                LongName = "National Security Association"
            };

            db.Insert(expected);
            expected.AgencyId = 4;

            Assert.AreEqual(expected.AgencyId, db.Get(4).Data.AgencyId);
        }

        [Test]
        public void TestUpdateAgeny()
        {
            Agency agency = db.Get(1).Data;
            agency.ShortName = "CIA";
            agency.LongName = "Central Intelligent Agency";
            db.Update(agency);

            Assert.AreEqual(agency.AgencyId, db.Get(1).Data.AgencyId);
            Assert.AreEqual(agency.ShortName, db.Get(1).Data.ShortName);
            Assert.AreEqual(agency.LongName, db.Get(1).Data.LongName);
        }

        [Test]
        public void TestDeleteAgency()
        {
            db.Delete(3);
            Assert.AreEqual(3, db.GetAll().Data.Count);

        }
    }
}