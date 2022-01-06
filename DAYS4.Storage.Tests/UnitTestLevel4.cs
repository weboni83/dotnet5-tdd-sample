using NUnit.Framework;
using DAYS4.Storage;
using DAYS4.Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F4DAYS.Storage.Tests
{
	public class Tests
	{
		Product _product;

		[SetUp]
		public void Setup()
		{
		}

		[Test, Order(0)]
		public void Gets_Products_IsNotNullProducts()
		{
			var localContext = new LocalContext();
			localContext.OnStartup();
			var gets = localContext.GetProductsAsync();

			Assert.IsNotNull(gets.Result);
		}

		[Test, Order(2)]
		public async Task Create_Product_IsTrueInsertedIdAsync()
		{
			var localContext = new LocalContext();
			localContext.OnStartup();

			_product = new Product();
			_product.Title = "Gen1";
			_product.Description = "Edits the specified object's value using the editor style indicated by";
			_product.Price = 5000m;
			_product.Slug = "slug2";
			_product.CreateAt = System.DateTime.Now;
			_product.UpdateAt = System.DateTime.UtcNow;

			var posted = await localContext.PostProductAsync(_product);

			Assert.IsTrue(posted > 0);
			//Assert.IsNotNull(posted);
		}

		[Test, Order(3)]
		public void Validation_ProductList_HasSomeTitle()
		{
			var localContext = new LocalContext();
			localContext.OnStartup();
			var gets = localContext.GetProductsAsync();
			//목록 일부가 2자리를 넘으면 성공
			//Assert.That(gets.Result, Has.Some.Matches<Product>(n => n.Title.Length > 2));

			//목록 전체의 제목이 2자릴 넘지 않으면 오류
			Assert.That(gets.Result, Has.All.Matches<Product>(n => n.Title.Length > 2));
		}

		[Test, Order(3)]
		public void Validation_ProductList_HasSomeTitle2()
		{
			var lists = new List<Product>
			{
				new Product() {
				Title = "Gen",
				Description = "Edits the specified object's value using the editor style indicated by",
					Price = 5000m,
				Slug = "2",
				CreateAt = System.DateTime.Now,
				UpdateAt = System.DateTime.UtcNow,
				Id = "1"
				},
				new Product() {
				Title = "G2",
				Description = "Edits the specified object's value using the editor style indicated by",
					Price = 5000m,
				Slug = "2",
				CreateAt = System.DateTime.Now,
				UpdateAt = System.DateTime.UtcNow,
				Id = "2"
				},
				new Product() {
				Title = "Gen4",
				Description = "Edits the specified object's value using the editor style indicated by",
					Price = 5000m,
				Slug = "2",
				CreateAt = System.DateTime.Now,
				UpdateAt = System.DateTime.UtcNow,
				Id = "2"
				},
				new Product() {
				Title = "Gen3",
				Description = "Edits the specified object's value using the editor style indicated by",
					Price = 5000m,
				Slug = "2",
				CreateAt = System.DateTime.Now,
				UpdateAt = System.DateTime.UtcNow,
				Id = "3"
				},
			};
			//목록 일부가 2자리를 넘으면 성공
			Assert.That(lists, Has.Some.Matches<Product>(n => n.Title.Length > 2));

			//목록 전체의 제목이 2자릴 넘지 않으면 오류
			//Assert.That(lists, Has.All.Matches<Product>(n => n.Title.Length > 2));
		}
	}
}