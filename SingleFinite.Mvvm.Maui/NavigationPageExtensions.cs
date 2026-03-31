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

using SingleFinite.Essentials;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// Extensions for the <see cref="NavigationPage"/> class.
/// </summary>
public static class NavigationPageExtensions
{
    /// <summary>
    /// Display dialogs from the given presenter on this page.
    /// </summary>
    public static readonly BindableProperty DialogPageProperty =
        BindableProperty.Create(
            propertyName: "DialogPage",
            returnType: typeof(DialogViewPresenterPage),
            declaringType: typeof(NavigationPage),
            defaultValue: null,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is not NavigationPage navigationPage)
                    return;
                if (oldValue is DialogViewPresenterPage oldDialogPage)
                    oldDialogPage.NavigationPage = null;
                if (newValue is DialogViewPresenterPage newDialogPage)
                    newDialogPage.NavigationPage = navigationPage;
            }
        );

    /// <summary>
    /// Set the DialogPage bindable property on the
    /// <see cref="NavigationPage"/>.
    /// </summary>
    /// <param name="navigationPage">
    /// The navigation page to set DialogPage for.
    /// </param>
    /// <param name="dialogPage">The dialog page to set.</param>
    /// <returns>The NavigationPage.</returns>
    public static NavigationPage DialogPage(
        this NavigationPage navigationPage,
        DialogViewPresenterPage? dialogPage
    ) => navigationPage.Also(
        it =>
        {
            it.SetValue(
                property: DialogPageProperty,
                value: dialogPage
            );
        }
    );
}
