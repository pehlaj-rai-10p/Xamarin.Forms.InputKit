﻿using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.InputKit.Shared.Controls
{
    /// <summary>
    /// A checkbox for boolean inputs. It Includes a text inside
    /// </summary>
    public class CheckBox : StackLayout
    {
        BoxView boxBackground = new BoxView { HeightRequest = 25, WidthRequest = 25, BackgroundColor = Color.LightGray, VerticalOptions = LayoutOptions.CenterAndExpand };
        BoxView boxSelected = new BoxView { IsVisible = false, HeightRequest = 18, WidthRequest = 18, BackgroundColor = Color.Accent, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Center };
        Label lblSelected = new Label { Text = "✓", FontSize = 19, FontAttributes = FontAttributes.Bold, IsVisible = false, TextColor = Color.Accent, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand };
        Label lblOption = new Label { VerticalOptions = LayoutOptions.CenterAndExpand, FontSize = 14 };
        private CheckType _type = CheckType.Box;
        private bool _isEnabled;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CheckBox()
        {
            this.Orientation = StackOrientation.Horizontal;
            this.Margin = new Thickness(10, 0);
            this.Padding = new Thickness(10);
            this.Spacing = 10;
            this.Children.Add(new Grid { Children = { boxBackground, boxSelected } });
            this.Children.Add(lblOption);
            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => { if (IsDisabled) return; IsChecked = !IsChecked; CheckChanged?.Invoke(this, new EventArgs()); CheckChangedCommand?.Execute(this.IsChecked); }),
            });
        }
        /// <summary>
        /// Invoked when check changed
        /// </summary>
        public event EventHandler CheckChanged;
        /// <summary>
        /// Executed when check changed
        /// </summary>
        public ICommand CheckChangedCommand { get; set; }
        /// <summary>
        /// Quick generator constructor
        /// </summary>
        /// <param name="optionName">Text to Display</param>
        /// <param name="key">Value to keep it like an ID</param>
        public CheckBox(string optionName, int key) : this()
        {
            Key = key;
            lblOption.Text = optionName;
        }
        /// <summary>
        /// You can set a Unique key for each control
        /// </summary>
        public int Key { get; set; }
        /// <summary>
        /// Text to display right of CheckBox
        /// </summary>
        public string Text { get => lblOption.Text; set => lblOption.Text = value; }
        /// <summary>
        /// IsChecked Property
        /// </summary>
        public bool IsChecked
        {
            get => ((this.Children[0] as Grid).Children[1]).IsVisible; set
            {
                (this.Children[0] as Grid).Children[1].IsVisible = value;
                SetValue(IsCheckedProperty, value);
            }
        }
        /// <summary>
        /// Gets or sets the checkbutton enabled or not. If checkbox is disabled, checkbox can not be interacted.
        /// </summary>
        public bool IsDisabled { get => _isEnabled; set { _isEnabled = value; this.Opacity = value ? 0.6 : 1; } }
        public Color Color { get => boxSelected.BackgroundColor; set { boxSelected.BackgroundColor = value; lblSelected.TextColor = value; } }
        public Color TextColor { get => lblOption.TextColor; set => lblOption.TextColor = value; }
        public CheckType Type { get => _type; set { _type = value; UpdateType(value); } }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(CheckBox), Color.Accent, propertyChanged: (bo, ov, nv) => (bo as CheckBox).Color = (Color)nv);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBox), Color.Gray, propertyChanged: (bo, ov, nv) => (bo as CheckBox).TextColor = (Color)nv);
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false, BindingMode.TwoWay, propertyChanged: (bo, ov, nv) => (bo as CheckBox).IsChecked = (bool)nv);
        public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(CheckBox), false, propertyChanged: (bo, ov, nv) => (bo as CheckBox).IsDisabled = (bool)nv);
        public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(int), typeof(CheckBox), 0, propertyChanged: (bo, ov, nv) => (bo as CheckBox).Key = (int)nv);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBox), "", propertyChanged: (bo, ov, nv) => (bo as CheckBox).Text = (string)nv);
        public static readonly BindableProperty CheckChangedCommandProperty = BindableProperty.Create(nameof(CheckChangedCommand), typeof(ICommand), typeof(CheckBox), null, propertyChanged: (bo, ov, nv) => (bo as CheckBox).CheckChangedCommand = (ICommand)nv);



        void UpdateType(CheckType _Type)
        {
            switch (_Type)
            {
                case CheckType.Box:
                    (this.Children[0] as Grid).Children.Remove(lblSelected);
                    (this.Children[0] as Grid).Children.Add(boxSelected);
                    break;
                case CheckType.Check:
                    lblSelected.Text = "✓";
                    (this.Children[0] as Grid).Children.Remove(boxSelected);
                    (this.Children[0] as Grid).Children.Add(lblSelected);
                    break;
                case CheckType.Cross:
                    lblSelected.Text = "✕";
                    (this.Children[0] as Grid).Children.Remove(boxSelected);
                    (this.Children[0] as Grid).Children.Add(lblSelected);
                    break;
                case CheckType.Star:
                    lblSelected.Text = "★";
                    (this.Children[0] as Grid).Children.Remove(boxSelected);
                    (this.Children[0] as Grid).Children.Add(lblSelected);
                    break;
            }
        }
        public enum CheckType
        {
            Box,
            Check,
            Cross,
            Star
        }
    }
}
