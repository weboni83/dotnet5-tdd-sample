using Dapper;
using DAYS4.Storage.Database;
using DAYS4.Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAYS4.Storage
{
	public class ProductRepository : IProductRepository
	{
		private readonly DatabaseConfig databaseConfig;

		public ProductRepository(DatabaseConfig databaseConfig)
		{
			this.databaseConfig = databaseConfig;
		}

		public async Task<int> CreateAsync(Product product)
		{
			using (SQLiteConnection connection = new SQLiteConnection(databaseConfig.Name))
			{
				//복수의 쿼리 실행을 위해서는 Open 해주고 실행해야한다.
				connection.Open();

				var affected = await connection.ExecuteAsync(insert, product).ConfigureAwait(false);
				if (affected == 0) return 0;

				var insertedId = await connection.ExecuteScalarAsync<int>(scope).ConfigureAwait(false);
				return insertedId;
			}
		}

		const string insert = "INSERT INTO Product (Title, Description, Price, Slug, Status, CreateAt, UpdateAt)" +
					"VALUES (@Title, @Description, @Price, @Slug, @Status, @CreateAt, @UpdateAt)";
		//const string scope = "SELECT CAST(scope_identity() AS int);";   //for mssql
		const string scope = @" SELECT last_insert_rowid()";   //for sqlite

	}
}
