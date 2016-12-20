using System;
using RadioButton;
using RadioButton.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SwipeFrame), typeof(SwipeFrameRenderer))]
namespace RadioButton.iOS
{
	public class SwipeFrameRenderer : FrameRenderer
	{
		public SwipeFrameRenderer()
		{
		}

		UISwipeGestureRecognizer swipeLeft;	

		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);
			swipeLeft = new UISwipeGestureRecognizer(() =>
			{
				var control = this.Element as SwipeFrame;
				control.OnSwipeLeft();
			});

			if (e.NewElement == null)
			{
				if (swipeLeft != null)
				{
					this.RemoveGestureRecognizer(swipeLeft);
				}
			}
			if (e.OldElement == null)
			{
				this.AddGestureRecognizer(swipeLeft);
			}
		}
	}
}
