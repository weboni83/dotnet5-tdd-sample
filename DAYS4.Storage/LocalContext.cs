using DAYS4.Storage.Database;
using DAYS4.Storage.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAYS4.Storage
{
	public class LocalContext
    {
        private readonly IServiceProvider _serviceProvider;
        public LocalContext()
        {
            IServiceCollection services = new ServiceCollection();

            services = new ServiceCollection();
            services.AddSingleton(sp => new DatabaseConfig { Name = "Product.sqlite" });
            services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
            services.AddSingleton<IProductProvider, ProductProvider>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddTransient<IProductProvider>(s => new ProductProvider(_serviceProvider.GetRequiredService<DatabaseConfig>()));
            services.AddTransient<IProductRepository>(s => new ProductRepository(_serviceProvider.GetRequiredService<DatabaseConfig>()));
            services.AddTransient<IProductService>(s => new ProductService(
                _serviceProvider.GetService<IProductProvider>()
                , _serviceProvider.GetService<IProductRepository>()));

            _serviceProvider = services.BuildServiceProvider();
        }

        public void OnStartup()
		{
            _serviceProvider.GetService<IDatabaseBootstrap>().Setup();
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var service = _serviceProvider.GetService<IProductService>();
            return await service.Get();
        }

        public async Task<int> PostProductAsync(Product product)
        {
            var service = _serviceProvider.GetService<IProductService>();
            return await service.PostAsync(product);
        }

    }
}
