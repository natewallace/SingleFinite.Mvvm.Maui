using SingleFinite.Essentials;
using SingleFinite.Example.Models.Pages.Dialogs;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Services;

namespace Example.App.Views.Pages;

public class DialogsPageView : ContentView, IView<DialogsPageViewModel>
{
    public DialogsPageView(DialogsPageViewModel viewModel, IAppDialog dialog)
    {
        ViewModel = viewModel;

        Content = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 1000,
            HeightRequest = 400,
            Padding = new Thickness(20),
            Children =
            {
                new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Text = $"Welcome to .NET MAUI! {Guid.NewGuid()}"
                },
                new Button
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Text = "Close"
                }.Also(button =>
                {
                    button.Clicked += (_, _) =>
                    {
                        ViewModel.CloseDialog();
                    };
                }),
                new Button
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Text = "Next"
                }.Also(button =>
                {
                    button.Clicked += (_, _) =>
                    {
                        dialog.Show<FirstDialogViewModel>();
                    };
                })
            }
        };
    }

    public DialogsPageViewModel ViewModel { get; }
}
