using System;
using System.Collections.Generic;
using System.Linq;
using Products.Model.Products;
using System.Threading.Tasks;

namespace Products.Model
{
	public class ProductsModel
	{
		private List<ProductEntity> ProductsList;
		private readonly Api WebApi;
		private DateTime RefreshedOn;
		private const uint DataCacheLifetimeInMinutes = 5;

		public ProductsModel (Api api)
		{
			ProductsList = new List<ProductEntity> ();
			WebApi = api;

			RefreshedOn = DateTime.Now.AddMinutes (-DataCacheLifetimeInMinutes);
		}

		public async Task<DateTime> RefreshData()
		{
			if (DateTime.Now > RefreshedOn.AddMinutes(DataCacheLifetimeInMinutes)) {
				ProductsList = await WebApi.GetProducts ();

				RefreshedOn = DateTime.Now;
			}

			return RefreshedOn;
		}

		public List<ProductEntity> GetProducts()
		{
			return ProductsList.Select (product => product).ToList ();
		}

		public ProductEntity FindProductByEAN(uint ean)
		{
			return ProductsList.Single (product => product.EAN == ean);
		}
	}
}

