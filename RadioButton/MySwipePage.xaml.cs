using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RadioButton
{
	public partial class MySwipePage : ContentPage
	{
		public MySwipePage()
		{
			InitializeComponent();
			sframe.SwipeLeft += async (z, o) => { await DisplayAlert("l", "g", "g"); };
		}


	}
}
