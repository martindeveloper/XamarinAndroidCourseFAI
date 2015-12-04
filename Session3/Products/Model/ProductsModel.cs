using System;
using System.Collections.Generic;
using System.Linq;
using Products.Model.Products;

namespace Products.Model
{
	public class ProductsModel
	{
		private List<ProductEntity> ProductsList;

		public ProductsModel ()
		{
			ProductsList = new List<ProductEntity> ();

			PopulateWithDemoData ();
		}

		public List<ProductEntity> GetProducts()
		{
			return ProductsList.Select (product => product).ToList ();
		}

		private void PopulateWithDemoData()
		{
			for (int i = 0; i < 20; i++)
			{
				ProductEntity product = new ProductEntity { 
					Name = $"Product #{i}",
					Price = (i + 1) * 25,
					Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sit.",
					Link = new Uri("http://www.google.com")
				};

				ProductsList.Add (product);
			}
		}
	}
}

