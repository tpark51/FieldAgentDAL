using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldAgent.DAL.CRUD
{
    public class AliasRepository : IAliasRepository
    {
        public DBFactory DbFac { get; set; }

        public AliasRepository(DBFactory dBFactory)
        {
            DbFac = dBFactory;
        }
        public Response Delete(int aliasId)
        {
            var response = new Response<Alias>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var alias = new Alias() { AliasId = aliasId };
                    db.Alias.Attach(alias);
                    db.Alias.Remove(alias);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Alias was not deleted.";
            }
            return response;
        }

        public Response<Alias> Get(int aliasId)
        {
            var response = new Response<Alias>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var alias = db.Alias.Find(aliasId);
                    if (alias != null)
                    {
                        response.Success = true;
                        response.Data = alias;
                        response.Message = $"{aliasId} was found.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Alias Id:{aliasId} was not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            var response = new Response<List<Alias>>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var alias = db.Alias
                        .Where(x => x.AgentId == agentId)
                        .ToList();
                    if (alias != null)
                    {
                        response.Success = true;
                        response.Data = alias;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"No alias were found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public Response<Alias> Insert(Alias alias)
        {
            var response = new Response<Alias>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Alias.Add(alias);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Alias was not inserted.";
            }

            return response;
        }

        public Response Update(Alias alias)
        {
            var response = new Response<Alias>();

            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    db.Alias.Update(alias);
                    db.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Alias was not updated.";
            }

            return response;
        }
    }
}
