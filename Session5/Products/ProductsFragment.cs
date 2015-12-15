
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
		private Button ScanButton;

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
			ScanButton = fragmentView.FindViewById<Button> (Resource.Id.productsListScanBtn);

			ScanButton.Click += OnScanButtonClick;

			return fragmentView;
		}

		private async void OnScanButtonClick (object sender, EventArgs e)
		{
			ZXing.Mobile.MobileBarcodeScanner scanner = new ZXing.Mobile.MobileBarcodeScanner();
			ZXing.Result result = await scanner.Scan();

			ProductEntity product = Model.FindProductByEAN (uint.Parse(result.Text));

			AlertDialog.Builder alertBuilder = new AlertDialog.Builder (Activity);

			if (product != null) {
				alertBuilder.SetTitle ($"Product - {product.Name}");
				alertBuilder.SetMessage (product.Description);
			} else {
				alertBuilder.SetTitle ("Error");
				alertBuilder.SetMessage ($"Product with EAN {result.Text} couldnt be found!");
			}

			alertBuilder.Show ();
		}

		public async override void OnStart ()
		{
			base.OnStart ();

			LayoutInflater inflater = Activity.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;

			if (inflater != null) {
				DateTime lastDataRefresh = await Model.RefreshData ();

				RefreshTimestamp.Text = lastDataRefresh.ToLongTimeString ();

				Adapter = new Products.Model.ProductsListAdapter (Activity, Model.GetProducts (), inflater);
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

