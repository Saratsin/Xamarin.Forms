﻿using System;
using Xamarin.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.ControlGallery.iOS;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

#if __UNIFIED__
using UIKit;
using Foundation;
using RectangleF=CoreGraphics.CGRect;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif

[assembly: ExportRenderer (typeof (Bugzilla31395.CustomContentView), typeof (CustomContentRenderer))]
[assembly: ExportRenderer (typeof (NativeCell), typeof (NativeiOSCellRenderer))]
[assembly: ExportRenderer (typeof (NativeListView2), typeof (NativeiOSListViewRenderer))]
[assembly: ExportRenderer (typeof (NativeListView), typeof (NativeListViewRenderer))]
namespace Xamarin.Forms.ControlGallery.iOS
{
	public class NativeiOSCellRenderer : ViewCellRenderer
	{
		static NSString s_rid = new NSString("NativeCell");

		public NativeiOSCellRenderer ()
		{
		}

		public override UITableViewCell GetCell (Xamarin.Forms.Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var x = (NativeCell)item;
			Console.WriteLine (x);

			NativeiOSCell c = reusableCell as NativeiOSCell; 

			if (c == null) {
				c = new NativeiOSCell (s_rid);
			}

			UIImage i = null;
			if (!string.IsNullOrWhiteSpace (x.ImageFilename)) {
				i = UIImage.FromFile ("Images/" + x.ImageFilename + ".jpg");
			}

			base.WireUpForceUpdateSizeRequested (item, c, tv);

			c.UpdateCell (x.Name, x.Category, i);

			return c;
		}
	}


	/// <summary>
	/// Sample of a custom cell layout, taken from the iOS docs at
	/// http://developer.xamarin.com/guides/ios/user_interface/tables/part_3_-_customizing_a_table's_appearance/
	/// </summary>
	public class NativeiOSCell : UITableViewCell  {
		UILabel _headingLabel;
		UILabel _subheadingLabel;
		UIImageView _imageView;

		public NativeiOSCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			ContentView.BackgroundColor = UIColor.FromRGB (255,255,224);

			_imageView = new UIImageView();

			_headingLabel = new UILabel () {
				Font = UIFont.FromName("Cochin-BoldItalic", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};

			_subheadingLabel = new UILabel () {
				Font = UIFont.FromName("AmericanTypewriter", 12f),
				TextColor = UIColor.FromRGB (38, 127, 0),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			};

			ContentView.Add (_headingLabel);
			ContentView.Add (_subheadingLabel);
			ContentView.Add (_imageView);
		}

		public void UpdateCell (string caption, string subtitle, UIImage image)
		{
			_imageView.Image = image;
			_headingLabel.Text = caption;
			_subheadingLabel.Text = subtitle;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			_imageView.Frame = new RectangleF(ContentView.Bounds.Width - 63, 5, 33, 33);
			_headingLabel.Frame = new RectangleF(5, 4, ContentView.Bounds.Width - 63, 25);
			_subheadingLabel.Frame = new RectangleF(100, 18, 100, 20);
		}
	}

	/// <summary>
	/// Sample of a custom cell layout, taken from the iOS docs at
	/// http://developer.xamarin.com/guides/ios/user_interface/tables/part_3_-_customizing_a_table's_appearance/
	/// </summary>
	public class NativeiOSListViewCell : UITableViewCell  {
		UILabel _headingLabel;
		UILabel _subheadingLabel;
		UIImageView _imageView;

		public NativeiOSListViewCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			ContentView.BackgroundColor = UIColor.FromRGB (218, 255, 127);

			_imageView = new UIImageView();

			_headingLabel = new UILabel () {
				Font = UIFont.FromName("Cochin-BoldItalic", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};

			_subheadingLabel = new UILabel () {
				Font = UIFont.FromName("AmericanTypewriter", 12f),
				TextColor = UIColor.FromRGB (38, 127, 0),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			};

			ContentView.Add (_headingLabel);
			ContentView.Add (_subheadingLabel);
			ContentView.Add (_imageView);
		}

		public void UpdateCell (string caption, string subtitle, UIImage image)
		{
			_imageView.Image = image;
			_headingLabel.Text = caption;
			_subheadingLabel.Text = subtitle;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			_imageView.Frame = new RectangleF(ContentView.Bounds.Width - 63, 5, 33, 33);
			_headingLabel.Frame = new RectangleF(5, 4, ContentView.Bounds.Width - 63, 25);
			_subheadingLabel.Frame = new RectangleF(100, 18, 100, 20);
		}
	}

	public class NativeiOSListViewRenderer : ViewRenderer<NativeListView2, UITableView>
	{
		public NativeiOSListViewRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<NativeListView2> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				SetNativeControl (new UITableView ());
			}

			if (e.OldElement != null) {
				// unsubscribe
			}

			if (e.NewElement != null) {
				// subscribe

				var s = new NativeiOSListViewSource (e.NewElement);
				Control.Source = s;
			}
		}
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (e.PropertyName == NativeListView.ItemsProperty.PropertyName) {
				// update the Items list in the UITableViewSource
				var s = new NativeiOSListViewSource (Element);

				Control.Source = s;
			}
		}
		public override SizeRequest GetDesiredSize (double widthConstraint, double heightConstraint)
		{
			return Control.GetSizeRequest (widthConstraint, heightConstraint, 44, 44);
		}
	}

	public class NativeListViewRenderer : ViewRenderer<NativeListView, UITableView>
	{
		public NativeListViewRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<NativeListView> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				SetNativeControl (new UITableView ());
			}

			if (e.OldElement != null) {
				// unsubscribe
			}

			if (e.NewElement != null) {
				// subscribe

				var s = new NativeListViewSource (e.NewElement);
				Control.Source = s;
			}
		}
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (e.PropertyName == NativeListView.ItemsProperty.PropertyName) {
				// update the Items list in the UITableViewSource
				var s = new NativeListViewSource (Element);
				Control.Source = s;
			}
		}
		public override SizeRequest GetDesiredSize (double widthConstraint, double heightConstraint)
		{
			return Control.GetSizeRequest (widthConstraint, heightConstraint, 44, 44);
		}
	}

	public class NativeiOSListViewSource : UITableViewSource
	{
		// declare vars
		IList<DataSource> _tableItems;
		NativeListView2 _listView;
		readonly NSString _cellIdentifier = new NSString("TableCell");

		public IEnumerable<DataSource> Items {
			//get{ }
			set{
				_tableItems = value.ToList();
			}
		}

		public NativeiOSListViewSource (NativeListView2 view)
		{
			_tableItems = view.Items.ToList();
			_listView = view;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>

		#if __UNIFIED__
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _tableItems.Count;
		}
		#else
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _tableItems.Count;
		}

		#endif
	

		#region user interaction methods

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_listView.NotifyItemSelected (_tableItems [indexPath.Row]);
			Console.WriteLine("Row " + indexPath.Row.ToString() + " selected");	
			tableView.DeselectRow (indexPath, true);
		}

		public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row " + indexPath.Row.ToString() + " deselected");	
		}

		#endregion

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular section and row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			NativeiOSListViewCell cell = tableView.DequeueReusableCell (_cellIdentifier) as NativeiOSListViewCell;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new NativeiOSListViewCell (_cellIdentifier);
			}

			if (string.IsNullOrWhiteSpace (_tableItems [indexPath.Row].ImageFilename)) {
				cell.UpdateCell (_tableItems [indexPath.Row].Name
					, _tableItems [indexPath.Row].Category
					, null);
			} else {
				cell.UpdateCell (_tableItems[indexPath.Row].Name
					, _tableItems[indexPath.Row].Category
					, UIImage.FromFile ("Images/" +_tableItems[indexPath.Row].ImageFilename + ".jpg") );
			}

			return cell;
		}
	}

	public class NativeListViewSource : UITableViewSource
	{
		// declare vars
		IList<string> _tableItems;
		string _cellIdentifier = "TableCell";
		NativeListView _listView;

		public IEnumerable<string> Items {
			set{
				_tableItems = value.ToList();
			}
		}

		public NativeListViewSource (NativeListView view)
		{
			_tableItems = view.Items.ToList ();
			_listView = view;
		}

		#if __UNIFIED__
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _tableItems.Count;
		}
		#else
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _tableItems.Count;
		}
		#endif
		#region user interaction methods

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_listView.NotifyItemSelected (_tableItems [indexPath.Row]);

			Console.WriteLine("Row " + indexPath.Row.ToString() + " selected");	

			tableView.DeselectRow (indexPath, true);
		}

		public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row " + indexPath.Row.ToString() + " deselected");	
		}

		#endregion

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular section and row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// declare vars
			UITableViewCell cell = tableView.DequeueReusableCell (_cellIdentifier);
			//string item = tableItems [indexPath.Row]; //.Items[indexPath.Row];

			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _cellIdentifier);

			// set the item text
			cell.TextLabel.Text = _tableItems [indexPath.Row];//.Items[indexPath.Row].Heading;

			// if it's a cell style that supports a subheading, set it
			//			if(item.CellStyle == UITableViewCellStyle.Subtitle 
			//				|| item.CellStyle == UITableViewCellStyle.Value1
			//				|| item.CellStyle == UITableViewCellStyle.Value2)
			//			{ cell.DetailTextLabel.Text = item.SubHeading; }

			// if the item has a valid image, and it's not the contact style (doesn't support images)
			//			if(!string.IsNullOrEmpty(item.ImageName) && item.CellStyle != UITableViewCellStyle.Value2)
			//			{
			//				if(File.Exists(item.ImageName))
			//					cell.ImageView.Image = UIImage.FromBundle(item.ImageName);
			//			}

			// set the accessory
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			return cell;
		}

	}

	public class CustomContentRenderer : ViewRenderer
	{
	}
}
