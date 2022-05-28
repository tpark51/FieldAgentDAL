using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldAgent.DAL.CRUD
{
    public class LocationRepository : ILocationRepository
    {
        public DBFactory DbFac { get; set; }

        public LocationRepository(DBFactory dBFactory)
        {
            DbFac = dBFactory;
        }
        public Response Delete(int locationId)
        {
            var response = new Response<Location>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var location = new Location() { LocationId = locationId };
                    db.Location.Attach(location);
                    db.Location.Remove(location);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Location was not deleted.";
            }
            return response;
        }

        public Response<Location> Get(int locationId)
        {
            var response = new Response<Location>();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var location = db.Location.Find(locationId);
                    if (location != null)
                    {
                        response.Success = true;
                        response.Data = location;
                        response.Message = $"{locationId} was found";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Location Id: {locationId} was not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            var response = new Response<List<Location>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var locations = db.Location
                        .Where(x => x.AgencyId == agencyId)
                        .ToList();
                    if (locations != null)
                    {
                        response.Success = true;
                        response.Data = locations;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No locations were found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public Response<Location> Insert(Location location)
        {
            var response = new Response<Location>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Location.Add(location);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Location was not inserted.";
            }

            return response;
        }

        public Response Update(Location location)
        {
            var response = new Response<Location>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Location.Update(location);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Location was not updated.";
            }

            return response;
        }
    }
}
