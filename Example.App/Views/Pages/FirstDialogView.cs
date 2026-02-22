using SingleFinite.Essentials;
using SingleFinite.Example.Models.Pages.Dialogs;
using SingleFinite.Mvvm;

namespace Example.App.Views.Pages;

public class FirstDialogView : ContentView, IView<FirstDialogViewModel>
{
    public FirstDialogView(FirstDialogViewModel viewModel)
    {
        ViewModel = viewModel;

        Content = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 800,
            HeightRequest = 600,
            Padding = new Thickness(20),
            Children =
            {
                new Label
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Text = $"First dialog view"
                },
                new Button
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Text = "Close"
                }.Also(button =>
                {
                    button.Clicked += (_, _) =>
                    {
                        ViewModel.Close();
                    };
                })
            }
        };
    }

    public FirstDialogViewModel ViewModel { get; }
}
