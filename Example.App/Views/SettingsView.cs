using CommunityToolkit.Maui.Markup;
using Example.Models;
using Fonts;
using SingleFinite.Essentials;
using SingleFinite.Mvvm;

namespace Example.App.Views;

public partial class SettingsView : ContentView, IView<SettingsViewModel>
{
    public SettingsView(SettingsViewModel viewModel)
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

    public SettingsViewModel ViewModel { get; private set; }
}
