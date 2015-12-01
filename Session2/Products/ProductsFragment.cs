
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Products.Model.Products;

namespace Products
{
	public class ProductsFragment : Fragment
	{
		private Model.ProductsModel Model;
		private ListView ProductsList;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Model
			Model = new Model.ProductsModel();
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View fragmentView = inflater.Inflate (Resource.Layout.ProductsListFragment, container, false);

			ProductsList = fragmentView.FindViewById<ListView> (Resource.Id.productsList);

			return fragmentView;
		}

		public override void OnStart ()
		{
			base.OnStart ();

			ArrayAdapter<ProductEntity> adapter = new ArrayAdapter<ProductEntity>(Activity, Android.Resource.Layout.SimpleListItem1, Model.GetProducts());
			ProductsList.Adapter = adapter;
		}
	}
}

