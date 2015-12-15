using System;
using System.Collections.Generic;
using System.Linq;
using Products.Model.Products;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

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

		public ObservableCollection<ProductEntity> GetProducts()
		{
			return new ObservableCollection<ProductEntity>(ProductsList.Select (product => product).ToList ());
		}

		public ProductEntity FindProductByEAN(uint ean)
		{
			return ProductsList.Single (product => product.EAN == ean);
		}

		/*
		public void tt() {
			Thread thread = new Thread (() => {
				Java.Net.Socket socket = new Java.Net.Socket ("192.168.3.91", 8000);
				System.IO.Stream inputStream = socket.InputStream;

				string payload = "Test";

				byte[] payloadBytes = new byte[payload.Length * sizeof(char)];
				System.Buffer.BlockCopy(payload.ToCharArray(), 0, payloadBytes, 0, payloadBytes.Length);

				inputStream.be
				inputStream.Write (payloadBytes, 0, payloadBytes.Length);
				inputStream.Close ();
			});

			thread.Start ();
		}*/
	}
}

