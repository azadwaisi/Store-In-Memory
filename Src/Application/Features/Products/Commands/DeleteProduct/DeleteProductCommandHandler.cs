using Application.Common.Tags;
using Application.Contracts;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Products;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.DeleteProduct
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
			_mapper = mapper;
			_unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id, cancellationToken);

			if (product == null || product.IsDelete)
				throw new NotFoundEntityException("Product not found or already deleted.");

			product.IsDelete = true;
			//product.DeletedAt = DateTime.UtcNow; 
			await _unitOfWork.Repository<Product>().UpdateAsync(product,cancellationToken);
			await _unitOfWork.Save(cancellationToken);

			return true;
		}
	}
}
