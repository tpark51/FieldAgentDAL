using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldAgent.DAL.CRUD
{
    public class AgencyRepository : IAgencyRepository
    {
        public DBFactory DbFac { get; set; }

        public AgencyRepository(DBFactory dBFactory)
        {
            DbFac = dBFactory;
        }
        public Response Delete(int agencyId)
        {
            var response = new Response<Agency>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agency = new Agency() { AgencyId = agencyId };
                    db.Agency.Attach(agency);
                    db.Agency.Remove(agency);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Agency was not deleted.";
            }
            return response;
        }

        public Response<Agency> Get(int agencyId)
        {
            var response = new Response<Agency>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agency = db.Agency.Find(agencyId);
                    if (agency != null)
                    {
                        response.Success = true;
                        response.Data = agency;
                        response.Message = $"{agencyId} was found.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Agency Id:{agencyId} was not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return response;
        }

        public Response<List<Agency>> GetAll()
        {
            var response = new Response<List<Agency>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencies = db.Agency.ToList();
                    if (agencies != null)
                    {
                        response.Success = true;
                        response.Data = agencies;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No agencies were found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return response;
        }

        public Response<Agency> Insert(Agency agency)
        {
            var response = new Response<Agency>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agency.Add(agency);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Agency was not inserted.";
            }
            
            return response;
        }

        public Response Update(Agency agency)
        {
            var response = new Response<Agency>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agency.Update(agency);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Agency was not updated.";
            }
            
            return response;
        }
    }
}
