using System;
using Xamarin.Forms;

namespace RadioButton
{
	public class SelectedButtonChangedEventArgs : EventArgs
	{
		public SelectedButtonChangedEventArgs(int selectedButton)
		{
			SelectedButton = selectedButton;
		}
		public object SelectedButton { get; private set; }
	}

	public class RadioButtonGroup : Frame
	{
		StackLayout _stackLayout = new StackLayout();
		public RadioButtonGroup()
		{
			_stackLayout.Orientation = StackOrientation.Horizontal;
		}

		#region SelectedButtonProperty
		public event EventHandler<SelectedButtonChangedEventArgs> ButtonSelected;
		public static readonly BindableProperty SelectedButtonProperty =
			BindableProperty.Create(
				propertyName: "SelectedButton",
				returnType: typeof(int),
				declaringType: typeof(RadioButtonGroup),
				defaultValue: -1,
				propertyChanged: (b, o, n) => ((RadioButtonGroup)b).OnSelectedButtonChanged());

		void OnSelectedButtonChanged()
		{
			ButtonSelected.Invoke(this, new SelectedButtonChangedEventArgs(SelectedButton));
		}

		public int SelectedButton
		{
			get { return (int)GetValue(SelectedButtonProperty); }
			set { SetValue(SelectedButtonProperty, value); }
		}
		#endregion

		#region CountProperty
		public static readonly BindableProperty CountProperty =
			BindableProperty.Create(
				propertyName: "Count",
				returnType: typeof(int),
				declaringType: typeof(RadioButtonGroup),
				defaultValue: 2);
		public int Count
		{
			get { return (int)GetValue(CountProperty); }
			set
			{
				SetValue(CountProperty, value);
				_stackLayout.Children.Clear();
				for (int i = 0; i < value; i++)
				{
					_stackLayout.Children.Add(new RadioControl());
				}
				Content = _stackLayout;
			}
		}
		#endregion

		#region CheckedImageProperty
		public static readonly BindableProperty CheckedImageProperty =
			BindableProperty.Create(
				propertyName: "CheckedImage",
				returnType: typeof(string),
				declaringType: typeof(RadioButtonGroup),
				defaultValue: "checked.png");
		public string CheckedImage
		{
			get { return (string)GetValue(CheckedImageProperty); }
			set { SetValue(CheckedImageProperty, value); }
		}
		public static readonly BindableProperty UnCheckedImageProperty =
			BindableProperty.Create(
				propertyName: "UnCheckedImage",
				returnType: typeof(string),
				declaringType: typeof(RadioButtonGroup),
				defaultValue: "uncheck.png");
		public string UnCheckedImage
		{
			get { return (string)GetValue(UnCheckedImageProperty); }
			set { SetValue(UnCheckedImageProperty, value); }
		}
		#endregion

		public void LoadFrame()
		{
			_stackLayout.Children.Clear();
			for (int i = 0; i < Count; i++)
			{
				_stackLayout.Children.Add(new RadioControl()
				{
					CheckedImage = this.CheckedImage,
					UnCheckedImage = this.UnCheckedImage,
					Key = i
				});
			}
			Content = _stackLayout;
		}

		 
	}

	public class RadioControl : Button
	{
		public RadioControl()
		{
			var _tap = new TapGestureRecognizer();
			_tap.Tapped += (s, e) => Tapped();
			this.GestureRecognizers.Add(_tap);

			this.Clicked += (sender, e) => Tapped();
		}

		public static readonly BindableProperty KeyProperty =
			BindableProperty.Create(
				propertyName: "KeyProperty",
				returnType: typeof(int),
				declaringType: typeof(RadioControl),
				defaultValue: 0);
		public int Key
		{
			get { return (int)GetValue(KeyProperty); }
			set { SetValue(KeyProperty, value); }
		}

		public static readonly BindableProperty IsCheckedProperty =
			BindableProperty.Create(
				propertyName: "IsChecked",
				returnType: typeof(bool),
				declaringType: typeof(RadioControl),
				defaultValue: false);

		/// <summary>
		/// Gets or sets a value indicating whether the control is checked.
		/// </summary>
		/// <value>The checked state.</value> 
		public bool IsChecked
		{
			get
			{
				return (bool)GetValue(IsCheckedProperty);
			}
			set
			{
				SetValue(IsCheckedProperty, value);
				//var eventHandler = this.CheckedChanged;
				//if (eventHandler != null)
				//{

				//	eventHandler.Invoke(this, value);
				//}
			}
		}

		#region CheckedImageProperty
		public static readonly BindableProperty CheckedImageProperty =
			BindableProperty.Create(
				propertyName: "CheckedImage",
				returnType: typeof(string),
				declaringType: typeof(RadioButtonGroup),
				defaultValue: "checked.png");
		public string CheckedImage
		{
			get { return (string)GetValue(CheckedImageProperty); }
			set { SetValue(CheckedImageProperty, value); }
		}
		public static readonly BindableProperty UnCheckedImageProperty =
			BindableProperty.Create(
				propertyName: "UnCheckedImage",
				returnType: typeof(string),
				declaringType: typeof(RadioButtonGroup),
				defaultValue: "uncheck.png");
		public string UnCheckedImage
		{
			get { return (string)GetValue(UnCheckedImageProperty); }
			set { SetValue(UnCheckedImageProperty, value); }
		}
		#endregion

		void Tapped()
		{
			if (!IsChecked)
			{
				IsChecked = true;
				var stackLayout = this.Parent as StackLayout;
				var radioGroup = stackLayout.Parent as RadioButtonGroup;
				radioGroup.SelectedButton = Key;
				foreach (var child in stackLayout.Children)
				{
					if (!(child as RadioControl).Key.Equals(Key))
						(child as RadioControl).IsChecked = false;
				}
			}
			//else
			//	IsChecked = true;	
		}

		//public static readonly BindableProperty TextProperty = BindableProperty.Create(
		//	propertyName: "Text",
		//	returnType: typeof(string),
		//	declaringType: typeof(RadioButton),
		//	defaultValue: string.Empty);

		//public string Text
		//{
		//	get { return (string)GetValue(TextProperty); }
		//	set { SetValue(TextProperty, value); }
		//}

		///// <summary>
		///// The default text property.
		///// </summary>
		//public static readonly BindableProperty TextProperty =
		//	BindableProperty.Create<CustomRadioButton, string>(
		//		p => p.Text, string.Empty);

		//public static readonly BindableProperty GroupNameProperty =
		//	BindableProperty.Create<CustomRadioButton, string>(p => p.GroupName, null, BindingMode.TwoWay);

		//public EventHandler<EventArgs<bool>> CheckedChanged;


		///// <summary>
		///// Identifies the TextColor bindable property.
		///// </summary>
		///// 
		///// <remarks/>
		//public static readonly BindableProperty TextColorProperty =
		//	BindableProperty.Create<CustomRadioButton, Color>(
		//		p => p.TextColor, Color.Black);




		//public string Text
		//{
		//	get
		//	{
		//		return this.GetValue<string>(TextProperty);
		//	}

		//	set
		//	{
		//		this.SetValue(TextProperty, value);
		//	}
		//}

		//public Color TextColor
		//{
		//	get
		//	{
		//		return this.GetValue<Color>(TextColorProperty);
		//	}

		//	set
		//	{
		//		this.SetValue(TextColorProperty, value);
		//	}
		//}

		//public int Id { get; set; }

		//public string GroupName
		//{ get; set; }

		//public int GroupId
		//{ get; set; }

		//public int ExpLevel
		//{ get; set; }

		//Label lblExperience = new Label();
		//public Label ExperienceLabel
		//{
		//	get
		//	{
		//		return lblExperience;
		//	}

		//	set
		//	{
		//		lblExperience = value;

		//	}
		//}
		//Action<CustomRadioButton> action;

		//public void RegisterAction(Action<CustomRadioButton> callback)
		//{
		//	action = callback;
		//}

		//public void Cleanup()
		//{
		//	action = null;
		//}

		//public void InvokeAction(CustomRadioButton data)
		//{
		//	if (action == null || data == null)
		//	{
		//		return;
		//	}
		//	//CountJobCards = data;

		//	action.Invoke(data);
		//}

	}
}
