using FieldAgent.Core.Entities;
using System.Collections.Generic;

namespace FieldAgent.Core.Interfaces
{
    public interface IAgencyRepository
    {
        Response<Agency> Insert(Agency agency);
        Response Update(Agency agency);
        Response Delete(int agencyId);
        Response<Agency> Get(int agencyId);
        Response<List<Agency>> GetAll();
    }
}
