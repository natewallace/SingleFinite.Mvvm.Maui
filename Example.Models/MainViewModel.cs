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

using Example.Models.ViewModels;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Services.Presenters;

namespace Example.Models;

/// <summary>
/// Interface for the main view model.
/// </summary>
public interface IMainViewModel
{
    /// <summary>
    /// The dialog presenter for the app.
    /// </summary>
    IDialogPresenter Dialogs { get; }

    /// <summary>
    /// The content for the app.
    /// </summary>
    IStackPresenter Content { get; }
}

/// <summary>
/// The main view model.
/// </summary>
/// <param name="dialogs">Dialog presenter.</param>
/// <param name="content">Content presenter.</param>
public partial class MainViewModel(
    IDialogPresenter dialogs,
    IStackPresenter content
) : ViewModel, IMainViewModel
{
    #region Properties

    /// <inheritdoc />
    public IDialogPresenter Dialogs => dialogs;

    /// <inheritdoc />
    public IStackPresenter Content => content;

    #endregion

    #region Methods

    /// <summary>
    /// Start the app on the first page.
    /// </summary>
    protected override void OnCreated()
    {
        Content.Push<FirstPageViewModel>();
    }

    /// <summary>
    /// Show the settings view.
    /// </summary>
    public void ShowSettings()
    {
        Dialogs.Show<SettingsViewModel>();
    }

    #endregion
}
