using Example.Models;
using SingleFinite.Essentials;
using SingleFinite.Mvvm;

namespace Example.App.Views;

public partial class SettingsView : ContentView, IView<SettingsViewModel>
{
    public SettingsView(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;

        HeightRequest = 800;
        WidthRequest = 600;
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Text = "Welcome to .NET MAUI!"
                },

                new Button
                {
                    Text = "Close"
                }.Also(it => it.Clicked += (_, _) => ViewModel.Close())
            }
        };
    }

    public SettingsViewModel ViewModel { get; private set; }
}
