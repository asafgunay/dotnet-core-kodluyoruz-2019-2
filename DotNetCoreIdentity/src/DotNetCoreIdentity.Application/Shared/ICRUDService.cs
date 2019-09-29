using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Application.Shared
{
    public interface ICRUDService<PK,MainDto,CreateDto,UpdateDto>
    {
        Task<ApplicationResult<MainDto>> Get(PK id);
        Task<ApplicationResult<List<MainDto>>> GetAll();
        Task<ApplicationResult<MainDto>> Create(CreateDto input);
        Task<ApplicationResult<MainDto>> Update(UpdateDto input);
        Task<ApplicationResult> Delete(PK id);
    }
}
