using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.DAL.Test
{
    public class SecurityClearanceTest
    {
        SecurityClearanceRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new SecurityClearanceRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGetOneClearance()
        {
            Assert.AreEqual("BlackOps", db.Get(1).Data.SecurityClearanceName);
        }

        [Test]
        public void TestGetAllClearances()
        {
            Assert.AreEqual(5, db.GetAll().Data.Count);
        }
    }
}