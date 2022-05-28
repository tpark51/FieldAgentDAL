using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldAgent.DAL.CRUD
{
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        public DBFactory DbFac { get; set; }

        public SecurityClearanceRepository(DBFactory dBFactory)
        {
            DbFac = dBFactory;
        }
        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            var response = new Response<SecurityClearance>();
                try
                {
                using (var db = DbFac.GetDbContext())
                {
                    var clearance = db.SecurityClearance.Find(securityClearanceId);
                    if (clearance != null)
                    {
                        response.Success = true;
                        response.Data = clearance;
                        response.Message = $"{securityClearanceId} was found.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Security Clearance Id:{securityClearanceId} was not found.";
                    }
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            return response;
        }

        public Response<List<SecurityClearance>> GetAll()
        {
            var response = new Response<List<SecurityClearance>>();

                try
                {
                using (var db = DbFac.GetDbContext())
                {
                    var clearances = db.SecurityClearance.ToList();
                    if (clearances != null)
                    {
                        response.Success = true;
                        response.Data = clearances;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No clearances were found.";
                    }
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
            return response;
        }
    }
}
