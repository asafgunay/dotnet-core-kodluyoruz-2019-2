using DotNetCoreIdentity.Application.BlogServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Application.BlogServices
{
    public interface IPostService
    {
        Task<ApplicationResult<PostDto>> Get(Guid id);
        Task<ApplicationResult<List<PostDto>>> GetAll();
        Task<ApplicationResult<PostDto>> GetByUrl(string categoryUrl, string postUrl);
        Task<ApplicationResult<PostDto>> Create(CreatePostInput input);
        Task<ApplicationResult<PostDto>> Update(UpdatePostInput input);
        Task<ApplicationResult> Delete(Guid id);
        Task<ApplicationResult> UpdateImageUrl(Guid id, string filePath);
    }
}
