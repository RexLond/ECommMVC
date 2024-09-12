using ECommMVC.BL.Abstact;
using ECommMVC.DAL.Abstact;
using ECommMVC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class ProductManager : GenericManager<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetByIdWithRefAsync(int id)
        {
            return await _productRepository.GetByIdWithRefAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllWithRefAsync()
        {
            return await _productRepository.GetAllWithRefAsync();
        }
    }
}
