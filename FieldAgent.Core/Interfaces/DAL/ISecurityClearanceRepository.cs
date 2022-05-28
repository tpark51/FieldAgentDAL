using FieldAgent.Core.Entities;
using System.Collections.Generic;

namespace FieldAgent.Core.Interfaces
{
    public interface ISecurityClearanceRepository
    {
        Response<SecurityClearance> Get(int securityClearanceId);
        Response<List<SecurityClearance>> GetAll();
    }
}
