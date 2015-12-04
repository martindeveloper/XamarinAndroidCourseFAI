
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
		private bool HaveTwoPanels = false;
		private ProductsFragment ProductsList;
		private ProductsDetailFragment ProductsDetail;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Products);

			HaveTwoPanels = (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape);

			if (!HaveTwoPanels) {
				ProductsList = new ProductsFragment ();

				FragmentTransaction transaction = FragmentManager.BeginTransaction ();

				transaction.Add (Resource.Id.productsFragmentContainer, ProductsList);

				transaction.Commit ();
			}
		}

		protected override void OnStart ()
		{
			base.OnStart ();

			FragmentManager fragmentManager = FragmentManager;

			if (HaveTwoPanels) {
				ProductsList = fragmentManager.FindFragmentById<ProductsFragment> (Resource.Id.productsFragment);
				ProductsDetail = fragmentManager.FindFragmentById<ProductsDetailFragment> (Resource.Id.productsDetailFragment);

				ConnectListWithDetail ();
			} else {
				HandleListClick ();
			}
		}

		private void HandleListClick()
		{
			ProductsList.OnProductClickEvent += (Products.Model.Products.ProductEntity product) => {
				ProductsDetail = new ProductsDetailFragment();

				FragmentTransaction transaction = FragmentManager.BeginTransaction();

				transaction.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_left);
				transaction.Replace(Resource.Id.productsFragmentContainer, ProductsDetail);
				transaction.AddToBackStack(product.Name);

				transaction.Commit();

				FragmentManager.ExecutePendingTransactions();

				ProductsDetail.ShowProduct(product);
			};
		}

		private void ConnectListWithDetail()
		{
			ProductsList.OnProductClickEvent += ProductsDetail.OnProductClickHandler;
		}
	}
}

