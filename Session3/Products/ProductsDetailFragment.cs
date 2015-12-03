
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

namespace Products
{
	public class ProductsDetailFragment : Fragment
	{
		private TextView ProductTitle;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View fragmentView = inflater.Inflate (Resource.Layout.ProductsDetailFragment, container, false);

			ProductTitle = fragmentView.FindViewById<TextView> (Resource.Id.productDetailTitle);

			return fragmentView;
		}

		public void OnProductClickHandler(Products.Model.Products.ProductEntity product)
		{
			
		}
	}
}

