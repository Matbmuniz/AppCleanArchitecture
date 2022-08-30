using AppCleanArchitecture.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(int? id);
        //Task<ProductDTO> GetProductCategories(int? id);
        Task Add(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Remove(int? id);
    }
}
