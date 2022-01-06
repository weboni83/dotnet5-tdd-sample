using DAYS4.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAYS4.Storage
{
	public interface IProductRepository
	{
		Task<int> CreateAsync(Product product);
	}
}
