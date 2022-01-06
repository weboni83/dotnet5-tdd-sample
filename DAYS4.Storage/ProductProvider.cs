using Dapper;
using DAYS4.Storage.Database;
using DAYS4.Storage.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DAYS4.Storage
{
    public class ProductProvider : IProductProvider
    {
        private readonly DatabaseConfig databaseConfig;

        public ProductProvider(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            using var connection = new SQLiteConnection(databaseConfig.Name);

            return await connection.QueryAsync<Product>("SELECT rowid AS Id, Title, Description, Price, Slug, Status, CreateAt, UpdateAt FROM Product;");
        }
    }
}
