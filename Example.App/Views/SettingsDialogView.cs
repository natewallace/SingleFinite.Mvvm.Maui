// MIT License
// Copyright (c) 2026 Single Finite
//
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in 
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using CommunityToolkit.Maui.Markup;
using Example.Models.ViewModels;
using Fonts;
using SingleFinite.Essentials;
using SingleFinite.Mvvm;

namespace Example.App.Views;

/// <summary>
/// The settings dialog view.
/// </summary>
public partial class SettingsDialogView : ContentView, IView<SettingsViewModel>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewModel">The view model for this view.</param>
    public SettingsDialogView(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;

        WidthRequest = MaximumWidthRequest = 600;
        HeightRequest = MaximumHeightRequest = 800;
        MinimumWidthRequest = 300;
        MinimumHeightRequest = 400;

        var rowPadding = new Thickness(
            left: 16,
            top: 16,
            right: 16,
            bottom: 0
        );

        Content = new VerticalStackLayout
        {
            Children =
            {
                // Title bar
                //
                new Grid
                {
                    Padding = rowPadding,

                    ColumnDefinitions =
                    {
                        new(width: GridLength.Star),
                        new(width: GridLength.Auto)
                    },

                    Children =
                    {
                        new Label
                        {
                            HorizontalOptions = LayoutOptions.Start,
                            FontSize = 20,
                            FontAttributes = FontAttributes.Bold,
                            Text = "Settings"
                        },
                        new ImageButton
                        {
                            HorizontalOptions = LayoutOptions.End,
                            CornerRadius = 50,
                            Padding = new(0),
                            Aspect = Aspect.Center,
                            Source = new FontImageSource
                            {
                                FontFamily = FluentUI.FontFamily,
                                Glyph = FluentUI.dismiss_circle_24_regular
                            },
                            Apply = it =>
                            {
                                it.Column(1);
                                it.Clicked += (_, _) => ViewModel.Close();
                            }
                        }
                    }
                },

                new BoxView
                {
                    HeightRequest = 1,
                    BackgroundColor = Colors.LightGray,
                    Margin = new Thickness(
                        left: 0,
                        top: 8,
                        right: 0,
                        bottom: 0
                    )
                },

                // App theme
                //
                new Grid
                {
                    Padding = rowPadding,

                    ColumnDefinitions =
                    {
                        new(width: GridLength.Star),
                        new(width: GridLength.Auto)
                    },

                    Children =
                    {
                        new Label
                        {
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                            FontSize = 20,
                            Text = "Theme"
                        },

                        new Picker
                        {
                            ItemsSource = Models.AppTheme.All,
                            Apply = it =>
                            {
                                it.Column(1);
                                it.Bind(
                                    targetProperty: Picker.SelectedItemProperty,
                                    getter: static (SettingsViewModel vm) => vm.CurrentAppTheme,
                                    setter: static (SettingsViewModel vm, Models.AppTheme? selected) => vm.CurrentAppTheme = selected
                                );
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// The ViewModel for this view.
    /// </summary>
    public SettingsViewModel ViewModel { get; private set; }

    #endregion
}
