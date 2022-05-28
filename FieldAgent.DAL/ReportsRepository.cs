using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
namespace FieldAgent.DAL
{
    public class ReportsRepository : IReportsRepository
    {
        public string ConnectionString { get; set; }

        public ReportsRepository(DBFactory dBFactory)
        {
            ConnectionString = dBFactory.GetConnection();
        }
        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            var response = new Response<List<ClearanceAuditListItem>>();
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand("ClearanceAudit", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AgencyId", agencyId);
                cmd.Parameters.AddWithValue("@SecurityClearanceId", securityClearanceId);

                try
                {
                    connection.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var auditListItem = new ClearanceAuditListItem();
                            auditListItem.BadgeId = new Guid(dr["BadgeId"].ToString());
                            auditListItem.NameLastFirst = dr["NameLastFirst"].ToString();
                            auditListItem.DateOfBirth = (DateTime)dr["DateOfBirth"];
                            auditListItem.ActivationDate = (DateTime)(dr["ActivationDate"]);
                            auditListItem.DeactivationDate = dr["DeactivationDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["DeactivationDate"]);

                            response.Data.Add(auditListItem);
                            
                        }
                    }
                    response.Success = true;
                }
                catch (Exception)
                {
                    response.Success = false;
                    response.Message = "Clearance Audit information unavailable.";
                }
            }
            return response;
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            var response = new Response<List<PensionListItem>>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand("PensionList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AgencyId", agencyId);

                try
                {
                    connection.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var pensionListItem = new PensionListItem();
                            pensionListItem.BadgeId = new Guid(dr["BadgeId"].ToString());
                            pensionListItem.NameLastFirst = dr["NameLastFirst"].ToString();
                            pensionListItem.DateOfBirth = (DateTime)dr["DateOfBirth"];
                            pensionListItem.DeactivationDate = (DateTime)dr["DeactivationDate"];

                            response.Data.Add(pensionListItem);
                        }
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Pension List not found.";
                }
            }
            return response;
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            var response = new Response<List<TopAgentListItem>>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand("TopAgents", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            var topAgentListItem = new TopAgentListItem();
                            topAgentListItem.NameLastFirst = dr["NameLastFirst"].ToString();
                            topAgentListItem.DateOfBirth = (DateTime)dr["DateOfBirth"];
                            topAgentListItem.CompletedMissionCount = (int)dr["CompletedMissionCount"];

                            response.Data.Add(topAgentListItem);
                        }
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Top Agents not found.";
                }
            }
            return response;
        }
    }
}
