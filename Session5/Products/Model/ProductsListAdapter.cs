using System;
using Android.Widget;
using Products.Model.Products;
using Android.Views;
using System.Collections.ObjectModel;
using Android.OS;
using Java.Lang;

namespace Products.Model
{
	public class ProductsListAdapter : BaseAdapter<ProductEntity>
	{
		private readonly ObservableCollection<ProductEntity> ProductsList;
		private readonly LayoutInflater Inflater;

		public ProductsListAdapter (ObservableCollection<ProductEntity> products, LayoutInflater inflater)
		{
			ProductsList = products;
			ProductsList.CollectionChanged += DataSourceChanged;

			Inflater = inflater;
		}

		private void DataSourceChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			// UI thread check
			bool isUIThread = Looper.MainLooper.Thread == Thread.CurrentThread();

			if (!isUIThread) {
				Console.WriteLine ("Data source has been updated from different thread. UI is not changed!");
				return;
			}

			NotifyDataSetChanged();
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

