using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RadioButton
{
	public class TabView : Grid
	{
		private int _count = -1;
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
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
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
		public static readonly BindableProperty TabTemplateProperty =
			BindableProperty.Create(
				propertyName: "TabTemplate",
				declaringType: typeof(TabView),
				returnType: typeof(DataTemplate),
				propertyChanged: (bindable, oldValue, newValue) => 
								{ 
									((TabView)bindable).OnTabTemplateChanged(); 
								});
		public DataTemplate TabTemplate
		{
			get { return (DataTemplate)GetValue(TabTemplateProperty); }
			set { SetValue(TabTemplateProperty, value); }
		}
		public void OnTabTemplateChanged()
		{
			
		}
		public static readonly BindableProperty SelectedTabProperty =
			BindableProperty.Create(
				propertyName: "SelectedTab",
				returnType: typeof(TabLayout),
				declaringType: typeof(TabView),
				propertyChanged: (bindable, oldValue, newValue) => { }
			);
		public TabLayout SelectedTab
		{
			get { return (TabLayout)GetValue(SelectedTabProperty); }
			set { SetValue(SelectedTabProperty, value); }
		}
		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create(
				propertyName: "SelectedIndex",
				returnType: typeof(int),
				declaringType: typeof(TabView),
				defaultValue: -1,
				propertyChanged: (bindable, oldValue, newValue) => 
								{ 
									((TabView)bindable).OnTabChanged();
								}
			);
		public int SelectedIndex
		{
			get { return (int)GetValue(SelectedIndexProperty); }
			set { SetValue(SelectedIndexProperty, value); }
		}
		//TODO
		//Bindable Property for Selected Tab button
		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(
				propertyName: "SelectedItem",
				returnType: typeof(TabButton),
				declaringType: typeof(TabView),
				propertyChanged: (bindable, oldValue, newValue) => { }
			);
		public TabButton SelectedItem
		{
			get { return (TabButton)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		private IEnumerable _itemSource;
		public IEnumerable ItemSource
		{
			get { return _itemSource; }
			set
			{
				_itemSource = value;
				UpdateLayout();
				UpdateSwipeFrameLayout();
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
				_count = 0;
				int index = 0;
				foreach (var item in _itemSource)
				{
					_count++;
					AddHeaderTabs(item, index++);
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
			var btn = new TabButton() { BackgroundColor = Color.Accent };
			foreach (var item in _itemSource)
			{
				btn.BindingContext = item;
				break;
			}
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

			int index = 0;
			foreach (var item in _itemSource)
			{
				if (index.Equals(SelectedIndex))
				{
					view.BindingContext = item;
					break;
				}
				++index;
			}

			Children.RemoveAt(1);
			_tabLayout = new SwipeFrame();
			_tabLayout.SwipeLeft += (sender, e) => 
			{
				if ((SelectedIndex + 1).Equals(_count))
					return;//SelectedIndex = 0;
				SelectedIndex++; 
			};
			_tabLayout.SwipeRight += (sender, e) => 
			{
				if ((SelectedIndex - 1) < 0)
					return ;// SelectedIndex = _count;
				SelectedIndex--; 
			};
			_tabLayout.Content = view;
			Children.Add(_tabLayout, 0, 1);


			//TODO
			//update selected tab
			var headerLayout = Children[0] as StackLayout;
			foreach (var i in headerLayout.Children)
			{
				var j = i as TabLayout;
			}
			var c = headerLayout.Children.ElementAt(SelectedIndex) as TabLayout;
			foreach (var child in headerLayout.Children)
			{
				(child as TabLayout).IsSelected = false;
				(child as TabLayout).BackgroundColor = Color.Silver;
			}
			c.IsSelected = true;
			c.BackgroundColor = Color.White;

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

		private void AddHeaderTabs(object item, int index)
		{
			var headerLayout = Children[0] as StackLayout;

			var content = TabTemplate.CreateContent();
			var view = content as Xamarin.Forms.View;
			if (view == null)
				throw new InvalidOperationException($"DataTemplate returned non-view content: '{content}'.");

			view.Parent = this;
			view.BindingContext = item;
			var tabLayout = new TabLayout();
			tabLayout.Content = view;
			tabLayout.Index = index;
			headerLayout.Children.Add(tabLayout);
		}
	}

	public class TabLayout : Frame
	{
		TapGestureRecognizer tap = new TapGestureRecognizer();
		public TabLayout()
		{
			tap.Tapped += (sender, e) => { Tapped(); };
			this.GestureRecognizers.Add(tap);
		}

		void Tapped()
		{
			var stackLayout = this.Parent as StackLayout;
			var tab = stackLayout.Parent as TabView;
			foreach (var child in stackLayout.Children)
			{
				(child as TabLayout).IsSelected = false;
				(child as TabLayout).BackgroundColor = Color.Silver;
			}
			this.IsSelected = true;
			this.BackgroundColor = Color.White;
			//TODO
			//Update selcted tab button on parent tab
			tab.SelectedTab = this;
			tab.SelectedIndex = Index;
			tab.OnTabChanged();
		}
		public bool IsSelected
		{
			get;
			set;
		}
		public int Index { get; set; }
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
			tab.SelectedItem = this;
			tab.OnTabChanged();
		}

		public bool IsSelected
		{
			get;
			set;
		}
	}
}
