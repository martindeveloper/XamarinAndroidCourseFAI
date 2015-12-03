using System;
using System.Collections.Generic;
using Android.Widget;
using Products.Model.Products;
using Android.Views;

namespace Products.Model
{
	public class ProductsListAdapter : BaseAdapter<ProductEntity>
	{
		private readonly List<ProductEntity> ProductsList;
		private readonly LayoutInflater Inflater;

		public ProductsListAdapter (List<ProductEntity> products, LayoutInflater inflater)
		{
			ProductsList = products;
			Inflater = inflater;
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			View cellView = convertView;

			if (cellView == null) {
				cellView = Inflater.Inflate (Resource.Layout.ProductsListCell, null);
			}

			ProductEntity product = this [position];

			TextView title = cellView.FindViewById<TextView> (Resource.Id.productsListCellTitle);
			title.Text = product.Name;

			TextView price = cellView.FindViewById<TextView> (Resource.Id.productsListCellPrice);
			price.Text = $"{product.Price} Kč";

			return cellView;
		}

		public override int Count {
			get {
				return ProductsList.Count;
			}
		}

		#endregion

		#region implemented abstract members of BaseAdapter

		public override Model.Products.ProductEntity this [int index] {
			get {
				return (ProductsList.Count - 1 < index) ? null : ProductsList [index];
			}
		}

		#endregion
	}
}

