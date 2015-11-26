
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Products
{
	[Activity (Label = "ProductsActivity")]			
	public class ProductsActivity : Activity
	{
		private Model.ProductsModel Model;
		private ListView ProductsList;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Products);

			// UI elements
			ProductsList = FindViewById<ListView> (Resource.Id.productsList);

			// Model
			Model = new Model.ProductsModel();

			// Set data to ListView
			ArrayAdapter<Model.Products.ProductEntity> stringAdapter = new ArrayAdapter<Model.Products.ProductEntity>(this, Android.Resource.Layout.SimpleListItem1, Model.GetProducts());
			ProductsList.Adapter = stringAdapter;
		}
	}
}

