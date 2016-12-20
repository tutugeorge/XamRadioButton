using System;
using Xamarin.Forms;

namespace RadioButton
{
	public class SwipeFrame : Frame
	{
		public SwipeFrame()
		{
		}

		public event EventHandler SwipeLeft;
		public void OnSwipeLeft()
		{
			if (SwipeLeft != null)
				SwipeLeft(this, null);
		}
		public event EventHandler SwipeRight;
	}
}
