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

using Example.Models.Pages;
using SingleFinite.Essentials;
using SingleFinite.Mvvm;

namespace Example.App.Views.Pages;

/// <summary>
/// The view for the first page.
/// </summary>
public partial class SecondPageView : ContentView, IView<SecondPageViewModel>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewModel">The view model for this view.</param>
    public SecondPageView(SecondPageViewModel viewModel)
    {
        ViewModel = viewModel;

        Content = new Grid().Also(mainGrid =>
        {
            mainGrid.RowDefinitions = [
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            ];

            mainGrid.Children.Add(
                new Label().Also(label =>
                {
                    Grid.SetRow(label, 0);
                    label.HorizontalOptions = LayoutOptions.Center;
                    label.VerticalOptions = LayoutOptions.Center;
                    label.Text = "This is the second page.";
                })
            );

            mainGrid.Children.Add(
                new Grid().Also(bottomBarGrid =>
                {
                    bottomBarGrid.ColumnDefinitions = [
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    ];

                    Grid.SetRow(bottomBarGrid, 1);
                    bottomBarGrid.HorizontalOptions = LayoutOptions.Start;
                    bottomBarGrid.Padding = 24;
                    bottomBarGrid.Children.Add(
                        new Button().Also(nextPageButton =>
                        {
                            Grid.SetColumn(nextPageButton, 1);
                            nextPageButton.Text = "Back";
                            nextPageButton.Clicked += (_, _) => ViewModel.PreviousPage();
                        })
                    );
                })
            );
        });
    }

    #endregion

    #region Properties

    /// <summary>
    /// The view model for this view.
    /// </summary>
    public SecondPageViewModel ViewModel { get; }

    #endregion
}
