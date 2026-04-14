using API.RequestHelper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
            Ispecifications<T> spec, int pageindex, int pagesize) where T : BaseEntity
        {
            var items = await repo.GetAllAsync(spec);
            var count=await repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageindex, pagesize, count, items);
            return Ok(pagination);
        }
    }
}
