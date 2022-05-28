using FieldAgent.Core.Entities;
using FieldAgent.DAL.CRUD;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;


namespace FieldAgent.DAL.Test
{
    public class LocationRepositoryTest
    {
        LocationRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            Configuration cp = new Configuration();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new LocationRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void GetOneLocation()
        {
            Assert.AreEqual(1, db.Get(1).Data.LocationId);
        }

        [Test]
        public void TestAddNewLocation()
        {
            Location expected = new Location
            {
                AgencyId = 2,
                LocationName = "FBI Seattle Division",
                Street1 = "1110 3rd Ave",
                City = "Seattle",
                PostalCode = "98101",
                CountryCode = "US"
            };

            db.Insert(expected);
            expected.LocationId = 4;

            Assert.AreEqual(expected.LocationId, db.Get(4).Data.LocationId);
            Assert.AreEqual("FBI Seattle Division", db.Get(4).Data.LocationName);

        }

        [Test]
        public void TestUpdateLocation()
        {
            Location location = db.Get(1).Data;
            location.LocationName = "CIA HeadQuarters";
            db.Update(location);

            Assert.AreEqual(location.AgencyId, db.Get(1).Data.AgencyId);
            Assert.AreEqual("CIA HeadQuarters", db.Get(1).Data.LocationName);
        }

        [Test]
        public void TestGetAllLocationsByAgencyId()
        {
            Assert.AreEqual(1, db.GetByAgency(1).Data.Count);
            Assert.AreEqual(1, db.GetByAgency(2).Data.Count);
            Assert.AreEqual(1, db.GetByAgency(3).Data.Count);
        }

        [Test]
        public void TestDeleteLocation()
        {
            db.Delete(1);
            Assert.AreEqual(0, db.GetByAgency(1).Data.Count);
            Assert.AreEqual(null, db.Get(1).Data);


        }


    }
}
