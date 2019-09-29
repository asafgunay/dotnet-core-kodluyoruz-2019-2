using DotNetCoreIdentity.Application.BlogServices.Dtos;
using DotNetCoreIdentity.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Application.BlogServices
{
    public interface IPostService : ICRUDService<Guid,PostDto,CreatePostInput,UpdatePostInput>
    {
        Task<ApplicationResult<PostDto>> GetByUrl(string categoryUrl, string postUrl);
        Task<ApplicationResult> UpdateImageUrl(Guid id, string filePath);
    }
}
