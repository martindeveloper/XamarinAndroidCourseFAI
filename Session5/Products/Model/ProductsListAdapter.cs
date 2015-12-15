using System;
using System.Collections.Generic;
using Android.Widget;
using Products.Model.Products;
using Android.Views;
using System.Collections.ObjectModel;
using System.Timers;
using Android.App;

namespace Products.Model
{
	public class ProductsListAdapter : BaseAdapter<ProductEntity>
	{
		private readonly ObservableCollection<ProductEntity> ProductsList;
		private readonly LayoutInflater Inflater;
		private readonly Activity HostActivity;

		public ProductsListAdapter (Activity context, ObservableCollection<ProductEntity> products, LayoutInflater inflater)
		{
			ProductsList = products;
			ProductsList.CollectionChanged += DataSourceChanged;
			Inflater = inflater;
			HostActivity = context;

			// Timer for adding new dummy product
			// Just for testing purpose
			Timer timer = new Timer (2 * 1000);
			timer.Elapsed += (object sender, ElapsedEventArgs e) => {
				ProductsList.Add(new ProductEntity {
					Name = $"Product Timer {e.SignalTime}",
					Price = 100
				});
			};

			timer.AutoReset = true;
			timer.Start ();
		}

		private void DataSourceChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			HostActivity.RunOnUiThread (() => {
				NotifyDataSetChanged();
			});
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

