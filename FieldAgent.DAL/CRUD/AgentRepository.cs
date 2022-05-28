using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldAgent.DAL.CRUD
{
    public class AgentRepository : IAgentRepository
    {
        public DBFactory DbFac { get; set; }

        public AgentRepository(DBFactory dBFactory)
        {
            DbFac = dBFactory;
        }
        public Response Delete(int agentId)
        {
            var response = new Response<Agent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var missionAgents = (from ma in db.MissionAgent
                                         join a in db.Agent on ma.AgentId equals a.AgentId
                                         where a.AgentId == agentId
                                         select ma).ToList();
                    foreach (var missionAgent in missionAgents)
                    {
                        db.MissionAgent.Attach(missionAgent);
                        db.MissionAgent.Remove(missionAgent);
                        db.SaveChanges();
                    }
                    var agencyAgents = (from aa in db.AgencyAgent
                                         join a in db.Agent on aa.AgentId equals a.AgentId
                                         where a.AgentId == agentId
                                         select aa).ToList();
                    foreach (var agencyAgent in agencyAgents)
                    {
                        db.AgencyAgent.Attach(agencyAgent);
                        db.AgencyAgent.Remove(agencyAgent);
                        db.SaveChanges();
                    }
                    var agent = new Agent() { AgentId = agentId };
                    db.Agent.Attach(agent);
                    db.Agent.Remove(agent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Agent was not deleted.";
            }
            return response;
        }

        public Response<Agent> Get(int agentId)
        {
            var response = new Response<Agent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agent = db.Agent.Find(agentId);
                    if (agent != null)
                    {
                        response.Success = true;
                        response.Data = agent;
                        response.Message = $"{agentId} was found.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Agent Id:{agentId} was not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public Response<List<Mission>> GetMissions(int agentId)
        {
            var response = new Response<List<Mission>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {

                    var missions = (from m in db.Mission
                                    join ma in db.MissionAgent on m.MissionId equals ma.MissionId
                                    join a in db.Agent on ma.AgentId equals a.AgentId
                                    where a.AgentId == agentId
                                    select m).ToList();
                    if (missions != null)
                    {
                        response.Success = true;
                        response.Data = missions;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No missions were found for Agent Id: {agentId}.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public Response<Agent> Insert(Agent agent)
        {
            var response = new Response<Agent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agent.Add(agent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Agent was not inserted.";
            }

            return response;
        }

        public Response Update(Agent agent)
        {
            var response = new Response<Agent>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Agent.Update(agent);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Agent was not updated.";
            }

            return response;
        }
    }
}
