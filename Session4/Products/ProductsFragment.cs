
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
		public delegate void OnProductClickHandler(Products.Model.Products.ProductEntity product);

		private Model.ProductsModel Model;
		private Products.Model.ProductsListAdapter Adapter;

		private ListView ProductsList;
		private TextView RefreshTimestamp;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// API
			Products.Model.Api api = new Products.Model.Api();

			// Model
			Model = new Model.ProductsModel(api);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View fragmentView = inflater.Inflate (Resource.Layout.ProductsListFragment, container, false);

			ProductsList = fragmentView.FindViewById<ListView> (Resource.Id.productsList);
			RefreshTimestamp = fragmentView.FindViewById<TextView> (Resource.Id.productsListTimestamp);

			return fragmentView;
		}

		public async override void OnStart ()
		{
			base.OnStart ();

			LayoutInflater inflater = Activity.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;

			if (inflater != null) {
				DateTime lastDataRefresh = await Model.RefreshData ();

				RefreshTimestamp.Text = lastDataRefresh.ToLongTimeString ();

				Adapter = new Products.Model.ProductsListAdapter (Model.GetProducts (), inflater);
				ProductsList.Adapter = Adapter;

				ProductsList.ItemClick += OnProductClick;
			}
		}

		private void OnProductClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			ProductEntity product = Adapter [e.Position];

			OnProductClickEvent?.Invoke (product);
		}
	}
}

