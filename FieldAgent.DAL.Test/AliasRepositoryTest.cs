using FieldAgent.Core.Entities;
using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;


namespace FieldAgent.DAL.Test
{
    public class AliasRepositoryTest
    {
        AliasRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AliasRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGetOneAlias()
        {
            Assert.AreEqual("Lauren Reynolds", db.Get(1).Data.AliasName);
        }

        [Test]
        public void TestAddNewAlias()
        {
            Alias expected = new Alias
            {
                AgentId = 1,
                AliasName = "Hotch"
            };

            db.Insert(expected);
            expected.AliasId = 2;

            Assert.AreEqual(expected.AliasId, db.Get(2).Data.AliasId);
            Assert.AreEqual("Hotch", db.Get(2).Data.AliasName);
        }

        [Test]
        public void TestUpdateAlias()
        {
            Alias alias = db.Get(1).Data;
            alias.Persona = "Black widow";
            db.Update(alias);

            Assert.AreEqual(alias.AliasId, db.Get(1).Data.AliasId);
            Assert.AreEqual("Lauren Reynolds", db.Get(1).Data.AliasName);
        }

        [Test]
        public void TestGetAllAliasByAgentId()
        {
            Assert.AreEqual(0, db.GetByAgent(1).Data.Count);
            Assert.AreEqual(1, db.GetByAgent(4).Data.Count);
        }

        [Test]
        public void TestDeleteAlias()
        {
            db.Delete(1);
            Assert.AreEqual(0, db.GetByAgent(4).Data.Count);

        }
    }
}
