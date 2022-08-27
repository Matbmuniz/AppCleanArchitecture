using AppCleanArchitecture.Application.Products.Commands;
using AppCleanArchitecture.Domain.Entities;
using AppCleanArchitecture.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Application.Products.Handlers
{
    class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            
            
            if (product == null)
            {
                throw new ApplicationException($"Error creating entity.");
            }
            else
            {

                product.Update(request.Name, request.Description, request.Price, 
                               request.Stock, request.Image, request.CategoryId);
                
                return await _productRepository.UpdateAsync(product);
            }
        } 
    }
}
