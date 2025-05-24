using Application.Dtos.Products;
using Application.Features.Products.Commands.AddProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Controllers.V1
{
    public class ProductsController : BaseApiController
    {
        //CQRS
        //Commands => add , edit , delete    // barai admin
        //Queries => select( get )           //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
        }
        [HttpPost]
		public async Task<ActionResult<ProductDto>> Add(AddProductCommand addProduct, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(addProduct, cancellationToken));
        }
        [HttpPut]
        public async Task<ActionResult<ProductDto>> Update(UpdateProductCommand updateProduct , CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(updateProduct,cancellationToken));
        }
		[HttpPut("Delete")]
		public async Task<ActionResult<ProductDto>> DeleteProduct(DeleteProductCommand deleteProduct, CancellationToken cancellationToken)
		{
			return Ok(await Mediator.Send(deleteProduct, cancellationToken));
		}
	}
}
