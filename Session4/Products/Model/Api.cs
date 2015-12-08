using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Model.Products;
using System.Net;
using Newtonsoft.Json;

namespace Products.Model
{
	public class Api
	{
		private const string BaseUrl = "http://private-ed36c-xamarinandroidcoursefai.apiary-mock.com";
		private readonly WebClient Client;

		public Api ()
		{
			Client = new WebClient ();
		}

		public async Task<List<ProductEntity>> GetProducts()
		{
			string target = $"{BaseUrl}/products";
			string result = await Client.DownloadStringTaskAsync (new Uri(target));

			List<ProductEntity> list = JsonConvert.DeserializeObject<List<ProductEntity>> (result);

			return list;
		}

		public async Task<ProductEntity> GetProductByEAN(string ean)
		{
			string target = $"{BaseUrl}/products/{ean}";
			string result = await Client.DownloadStringTaskAsync (new Uri (target));

			ProductEntity product = JsonConvert.DeserializeObject<ProductEntity> (result);

			return product;
		}
	}
}

