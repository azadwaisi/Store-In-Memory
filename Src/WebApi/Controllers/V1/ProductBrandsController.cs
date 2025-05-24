using Application.Features.ProductBrands.Queries.GetAll;
using Application.Features.Products.Queries.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Controllers.V1
{
    public class ProductBrandsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductBrandsQuery(), cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
        }
    }
}
