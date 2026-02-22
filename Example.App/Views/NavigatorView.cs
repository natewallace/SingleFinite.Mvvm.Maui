using System.Diagnostics;
using SingleFinite.Essentials;
using SingleFinite.Example.Models;
using SingleFinite.Example.Models.Pages.Dialogs;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Services;

namespace Example.App.Views;

public class NavigatorView : ContentView, IView<NavigatorViewModel>
{
    public NavigatorView(NavigatorViewModel viewModel, IAppDialog dialog)
    {
        Content = new VerticalStackLayout
        {
            Children = {
                new Label
                {
                    Text = "navigator!"
                },
                new Button
                {
                    Text = "Go to Details",
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(8)
                }.Also(button =>
                {
                    button.Clicked += (_, _) =>
                    {
                        Debug.WriteLine("Navigating to DetailsPageViewModel");
                        dialog.Show<DialogsPageViewModel>();
                    };
                })
            }
        };
        ViewModel = viewModel;
    }

    public NavigatorViewModel ViewModel { get; }
}
