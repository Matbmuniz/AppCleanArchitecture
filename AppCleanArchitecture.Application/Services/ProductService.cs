using AppCleanArchitecture.Application.DTOs;
using AppCleanArchitecture.Application.Interfaces;
using AppCleanArchitecture.Application.Products.Commands;
using AppCleanArchitecture.Application.Products.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Application.Services
{
    public class ProductService : IProductService
    {
        private IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQueries = new GetProductsQuery();

            if (productsQueries == null)
                throw new Exception($"Entity could not be loaded.");

            var result = await _mediator.Send(productsQueries);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productsByIdQuery = new GetProductByIdQuery(id.Value);

            if(productsByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            var result = await _mediator.Send(productsByIdQuery);

            return _mapper.Map<ProductDTO>(result);

        }

        public async Task Add(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if(productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");

            await _mediator.Send(productRemoveCommand);
        }
    }
}
