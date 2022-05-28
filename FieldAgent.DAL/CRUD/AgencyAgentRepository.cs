using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldAgent.DAL.CRUD
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        public DBFactory DbFac { get; set; }

        public AgencyAgentRepository(DBFactory dBFactory)
        {
            DbFac = dBFactory;
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            var response = new Response<AgencyAgent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgent.Add(agencyAgent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"AgencyAgent was not inserted.";
            }

            return response;
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            var response = new Response<AgencyAgent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgent.Update(agencyAgent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"AgencyAgent was not updated.";
            }

            return response;
        }

        public Response Delete(int agencyid, int agentid)
        {
            AgencyAgent agencyagent = Get(agencyid, agentid).Data;
            var response = new Response<Agency>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.AgencyAgent.Attach(agencyagent);
                    db.AgencyAgent.Remove(agencyagent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"AgencyAgent was not deleted.";
            }
            return response;
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
                var response = new Response<AgencyAgent>();

                try
                {
                    using (var db = DbFac.GetDbContext())
                    {
                    var agencyagent = db.AgencyAgent
                        .Where(a => a.AgencyId == agencyid)
                        .Where(a => a.AgentId == agentid)
                        .FirstOrDefault();
                        if (agencyagent != null)
                        {
                            response.Success = true;
                            response.Data = agencyagent;
                            response.Message = "AgencyAgent was found.";
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "AgencyAgent was not found.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return response;
            }     

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            var response = new Response<List<AgencyAgent>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencyagents = db.AgencyAgent
                        .Where(a => a.AgencyId == agencyId)
                        .ToList();
                    if (agencyagents != null)
                    {
                        response.Success = true;
                        response.Data = agencyagents;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No AgencyAgents were found for Agency Id: {agencyId}.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            var response = new Response<List<AgencyAgent>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agencyagents = db.AgencyAgent
                        .Where(a => a.AgentId == agentId)
                        .ToList();
                    if (agencyagents != null)
                    {
                        response.Success = true;
                        response.Data = agencyagents;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No AgencyAgents were found for Agent Id: {agentId}.";
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
