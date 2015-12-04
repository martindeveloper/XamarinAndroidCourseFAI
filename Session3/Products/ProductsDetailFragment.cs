
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
		private TextView ProductPrice;
		private TextView ProductDescription;
		private TextView ProductLink;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View fragmentView = inflater.Inflate (Resource.Layout.ProductsDetailFragment, container, false);

			ProductTitle = fragmentView.FindViewById<TextView> (Resource.Id.productDetailTitle);
			ProductPrice = fragmentView.FindViewById<TextView> (Resource.Id.productDetailPrice);
			ProductDescription = fragmentView.FindViewById<TextView> (Resource.Id.productDetailDescription);

			ProductLink = fragmentView.FindViewById<TextView> (Resource.Id.productDetailLink);
			ProductLink.LinksClickable = true;

			return fragmentView;
		}

		public void ShowProduct(Products.Model.Products.ProductEntity product)
		{
			string price = Activity.Resources.GetString (Resource.String.price);

			ProductTitle.Text = product.Name;
			ProductPrice.Text = $"{price}: {product.Price} Kč";
			ProductDescription.Text = product.Description;
			ProductLink.Text = product.Link?.ToString ();
		}

		public void OnProductClickHandler(Products.Model.Products.ProductEntity product)
		{
			ShowProduct (product);
		}
	}
}

