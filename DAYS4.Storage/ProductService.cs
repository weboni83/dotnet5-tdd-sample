using DAYS4.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAYS4.Storage
{
    public class ProductService : IProductService
    {
        private readonly IProductProvider productProvider;
        private readonly IProductRepository productRepository;

        public ProductService(IProductProvider productProvider,
            IProductRepository productRepository)
        {
            this.productProvider = productProvider;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await productProvider.Get();
        }


        public async Task<int> PostAsync(Product product)
        {
            return await productRepository.CreateAsync(product);
        }
    }
}
