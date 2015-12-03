
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
		private ProductsFragment ProductsList;
		private ProductsDetailFragment ProductsDetail;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Products);
		}

		protected override void OnStart ()
		{
			base.OnStart ();

			FragmentManager fragmentManager = FragmentManager;

			ProductsList = fragmentManager.FindFragmentById<ProductsFragment> (Resource.Id.productsFragment);

			if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape) {
				ProductsDetail = fragmentManager.FindFragmentById<ProductsDetailFragment> (Resource.Id.productsDetailFragment);
			}
		}
	}
}

