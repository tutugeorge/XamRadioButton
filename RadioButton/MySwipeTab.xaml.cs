using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RadioButton
{
	public partial class MySwipeTab : ContentPage
	{
		public MySwipeTab()
		{
			InitializeComponent();
			tab.TabChanged += (sender, e) => { foo(sender, e); };
			tab.ItemSource = new List<Boo>()
			{
				new Boo(){ Obj1 = "one", Obj2 = "1"},
				new Boo(){ Obj1 = "two", Obj2 = "2"}
			};
		}

		void foo(object sender, EventArgs e)
		{
			//tab.TabLayout = new SwipeFrame() { BackgroundColor = Color.Purple };
		}
	}

	public class Boo
	{
		public string Obj1
		{
			get;
			set;
		}
		public string Obj2
		{
			get;
			set;
		}
	}
}
