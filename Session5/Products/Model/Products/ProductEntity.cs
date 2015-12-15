using System;

namespace Products.Model.Products
{
	public class ProductEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public uint EAN { get; set; }
		public Uri Link { get; set; }

		public ProductEntity ()
		{
		}

		public override string ToString()
		{
			return Name;
		}
	}
}

