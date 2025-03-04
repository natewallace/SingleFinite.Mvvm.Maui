using SingleFinite.Example.Models;
using SingleFinite.Mvvm;

namespace Example.App.Views;

public partial class MainWindow : Window, IView<MainViewModel>
{
    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        Page = new ContentPage()
        {
            Content = new VerticalStackLayout
            {
                Children = {
                    new Label {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        Text = "Welcome to .NET MAUI!"
                    }
                }
            }
        };
    }

    public MainViewModel ViewModel { get; }
}
