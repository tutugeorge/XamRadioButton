using System;
using RadioButton.iOS;
using UIKit;
using RadioButton;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(RadioControl), typeof(RadioButtonRenderer))]
namespace RadioButton.iOS
{
	public class RadioButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			//UIImage _radioButtonImage;
			base.OnElementChanged(e);

			if (Control != null)
			{
				SetBackgroundImage((RadioControl)e.NewElement);
			}
			if (e.OldElement != null)
			{
				// Unsubscribe
				//_radioButtonImage.Tapped //-= OnCameraPreviewTapped;
			}
			if (e.NewElement != null)
			{
				// Subscribe
				//uiCameraPreview.Tapped += OnCameraPreviewTapped;
			}
		}

		void SetBackgroundImage(RadioControl radioButton)
		{
			UIImage _radioButtonImage;
			if (radioButton.IsChecked)
			{
				var _checkedImg = radioButton.CheckedImage;
				_radioButtonImage = UIImage.FromBundle(_checkedImg);
			}
			else
			{
				var _unCheckedImg = radioButton.UnCheckedImage;
				_radioButtonImage = UIImage.FromBundle(_unCheckedImg);
			}
			Control.SetBackgroundImage(_radioButtonImage, UIControlState.Normal);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (Control != null)
			{
				if (e.PropertyName.Equals("IsChecked"))
				{
					var radioButton = sender as RadioControl;
					SetBackgroundImage(radioButton);
				}
			}
		}
	}
}
