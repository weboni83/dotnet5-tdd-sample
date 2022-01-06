using DAYS4.Storage.Database;
using Dapper;
using System.Linq;
using System.Data.SQLite;

namespace DAYS4.Storage
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SQLiteConnection(databaseConfig.Name);
            
            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Product';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "Product")
                return;

            connection.Execute("Create Table Product (" +
                "Title VARCHAR(100) NOT NULL," +
                "Description VARCHAR(1000) NULL," +
                "Price decimal(15,4) NULL," +
                "Slug VARCHAR(100) NULL," +
                "Status VARCHAR(100) NULL," +
                "CreateAt DATETIME NULL," +
                "UpdateAt DATETIME NULL" +
                ");");
        }
    }
}
