using System;
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
		}

		public static readonly BindableProperty HeaderHeightProperty =
			BindableProperty.Create(
				propertyName: "HeaderHeight",
				returnType: typeof(void),
				declaringType: typeof(TabView),
				propertyChanged: (bindable, oldValue, newValue) => ((TabView)bindable).OnHeaderHeightChanged(newValue)
			);
		private void OnHeaderHeightChanged(object newValue)
		{
			
		}

		private void InitHeaderLayout()
		{
			var layout = new StackLayout();
			layout.BackgroundColor = Color.Gray;
			Children.Add(layout, 0, 0);
		}

		private void InitBodyLayout()
		{
			var layout = new SwipeFrame();
			layout.BackgroundColor = Color.Silver;
			Children.Add(layout, 0, 1);
		}

	}
}
