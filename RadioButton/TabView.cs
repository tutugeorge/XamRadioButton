using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RadioButton
{
	public class TabView : Grid
	{
		public TabView()
		{
			RowDefinitions.Add(new RowDefinition()
			{
				Height = new GridLength(0.25, GridUnitType.Star)
			});
			RowDefinitions.Add(new RowDefinition()
			{
				Height = new GridLength(0.75, GridUnitType.Star)
			});
			InitHeaderLayout();
			InitBodyLayout();
			AddHeaderTabs();
		}

		#region events
		public event EventHandler TabChanged;
		public void OnTabChanged()
		{
			if (TabChanged != null)
			{
				TabChanged(this, null);
				UpdateSwipeFrameLayout();
			}
		}
		#endregion

		#region Properties
		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create(
				propertyName: "ItemTemplate",
				declaringType: typeof(TabView),
				returnType: typeof(DataTemplate)
			);
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		//TODO
		//Bindable Property for Selected Tab button

		private IEnumerable _itemSource;
		public IEnumerable ItemSource
		{
			get { return _itemSource; }
			set
			{
				_itemSource = value;
				UpdateLayout();
			}
		}
		private SwipeFrame _tabLayout;
		public SwipeFrame TabLayout
		{
			set
			{
				_tabLayout = value;
				//UpdateBodyLayout();
				//UpdateSwipeFrameLayout();
			}
		}
		#endregion

		private void InitHeaderLayout()
		{
			var layout = new StackLayout();
			layout.BackgroundColor = Color.Gray;
			layout.Orientation = StackOrientation.Horizontal;
			layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
			Children.Add(layout, 0, 0);
		}

		private void InitBodyLayout()
		{
			var layout = new SwipeFrame();
			layout.BackgroundColor = Color.Silver;
			Children.Add(layout, 0, 1);
		}

		private void UpdateLayout()
		{
			if (_itemSource != null)
			{
				ClearTabsHeader();
				foreach (var item in _itemSource)
				{
					AddButtonTabsToHeader();
				}
			}
		}

		private void ClearTabsHeader()
		{
			var headerLayout = Children[0] as StackLayout;
			headerLayout.Children.Clear();
		}

		private void AddButtonTabsToHeader()
		{
			var btn = new TabButton() { Text = "Btn1", BackgroundColor = Color.Accent };
			var headerLayout = Children[0] as StackLayout;
			headerLayout.Children.Add(btn);
		}

		private void UpdateBodyLayout()
		{
			Children.RemoveAt(1);
			//Define data template 
			//update ui
			Children.Add(_tabLayout, 0, 1);
		}

		private void UpdateSwipeFrameLayout()
		{
			var content = ItemTemplate.CreateContent();
			var view = content as Xamarin.Forms.View;
			if (view == null)
				throw new InvalidOperationException($"DataTemplate returned non-view content: '{content}'.");

			view.Parent = this;

			Children.RemoveAt(1);
			_tabLayout = view as SwipeFrame;
			Children.Add(_tabLayout, 0, 1);
		}

		//public static readonly BindableProperty HeaderHeightProperty =
		//	BindableProperty.Create(
		//		propertyName: "HeaderHeight",
		//		returnType: typeof(void),
		//		declaringType: typeof(TabView),
		//		propertyChanged: (bindable, oldValue, newValue) => ((TabView)bindable).OnHeaderHeightChanged(newValue)
		//	);
		//private void OnHeaderHeightChanged(object newValue)
		//{
			
		//}

		private void AddHeaderTabs()
		{
			var headerLayout = Children[0] as StackLayout;
			var btn1 = new TabButton() { Text = "Btn1", BackgroundColor = Color.Accent };
			var btn2 = new TabButton() { Text = "Btn2", BackgroundColor = Color.Accent };
			var btn3 = new TabButton() { Text = "Btn3", BackgroundColor = Color.Accent };
			headerLayout.Children.Add(btn1);
			headerLayout.Children.Add(btn2);
			headerLayout.Children.Add(btn3);
		}




	}

	public class TabButton : Button
	{
		public TabButton()
		{
			this.Clicked += (sender, e) => { Tapped(); };
		}

		void Tapped()
		{
			var stackLayout = this.Parent as StackLayout;
			var tab = stackLayout.Parent as TabView;
			foreach (var child in stackLayout.Children)
			{
				(child as TabButton).IsSelected = false;
				(child as TabButton).BackgroundColor = Color.Silver;
			}
			this.IsSelected = true;
			this.BackgroundColor = Color.White;
			//TODO
			//Update selcted tab button on parent tab
			tab.OnTabChanged();
		}

		public bool IsSelected
		{
			get;
			set;
		}
	}
}
