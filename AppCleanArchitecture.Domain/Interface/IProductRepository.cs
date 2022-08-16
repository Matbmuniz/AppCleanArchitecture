﻿using AppCleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Domain.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetProductCategoryAsync(int? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Remove(Product product);
    }
}