
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
		public event OnProductClickHandler OnProductClickEvent;
		public delegate void OnProductClickHandler(Products.Model.Products.ProductEntity products);

		private Model.ProductsModel Model;
		private ListView ProductsList;
		private Products.Model.ProductsListAdapter Adapter;

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

			LayoutInflater inflater = Activity.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;

			if (inflater != null) {
				Adapter = new Products.Model.ProductsListAdapter (Model.GetProducts (), inflater);
				ProductsList.Adapter = Adapter;

				ProductsList.ItemClick += OnProductClick;
			}
		}

		private void OnProductClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			ProductEntity product = Adapter [e.Position];

			AlertDialog.Builder alertBuilder = new AlertDialog.Builder (Activity);

			alertBuilder.SetTitle (product.Name)
						.SetMessage ($"Price {product.Price} Kč");

			alertBuilder.Show ();
		}
	}
}

