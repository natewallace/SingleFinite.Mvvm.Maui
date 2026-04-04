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
using SingleFinite.Mvvm.Services.Presenters;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// Presents page views through a NavigationPage.
/// </summary>
public partial class NavigationPageViewPresenter : NavigationPage
{
    #region Fields

    /// <summary>
    /// Observer for view changes on the source presenter.
    /// </summary>
    private IDisposable? _sourceViewObserver;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public NavigationPageViewPresenter()
    {
        Navigation.PushAsync(
            page: new ContentPage(),
            animated: false
        );
        Loaded += (_, _) => SubscribeToPresenter();
        Unloaded += (_, _) => UnsubscribeFromPresenter();
    }

    #endregion

    #region Properties

    /// <summary>
    /// The presenter whose views will be displayed in this control.
    /// </summary>
    public IPresenter? Source
    {
        get;
        set
        {
            if (field == value)
                return;

            UnsubscribeFromPresenter();

            field = value;

            SubscribeToPresenter();
        }
    }

    /// <summary>
    /// Action invoked when the built in back button is pressed.  The function
    /// should return true if the back action was handled and no further
    /// processing of the back event should happen.
    /// </summary>
    public Func<bool>? OnBack { get; set; }

    /// <summary>
    /// Function invoked to determine if the back button should be shown for the
    /// given view.
    /// </summary>
    public Func<IView, bool>? HasBack { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Observe changes to the current view of the presenter.
    /// </summary>
    private void SubscribeToPresenter()
    {
        if (_sourceViewObserver is not null)
            return;

        _sourceViewObserver = Source?.CurrentChanged
            ?.Observe()
            ?.OnEach(
                args => UpdateSourceContent(
                    view: args.View,
                    isNew: args.IsNew
                )
            );

        UpdateSourceContent(
            view: Source?.Current,
            isNew: true
        );
    }

    /// <summary>
    /// Stop observing changes to the current view of the presenter.
    /// </summary>
    private void UnsubscribeFromPresenter()
    {
        _sourceViewObserver?.Dispose();
        _sourceViewObserver = null;
    }

    /// <summary>
    /// Update the view that is being displayed.
    /// </summary>
    /// <param name="view">The view to display.</param>
    /// <param name="isNew">Indicates if the view was newly added.</param>
    private void UpdateSourceContent(IView? view, bool isNew)
    {
        var page = view as Page;
        if (page is null && view is View control)
        {
            page = new ContentPage
            {
                Content = control
            };
        }

        MainThread.BeginInvokeOnMainThread(
            async () =>
            {
                if (page is null)
                {
                    while (Navigation.NavigationStack.Count > 2)
                        Navigation.RemovePage(Navigation.NavigationStack[1]);
                    if (Navigation.NavigationStack.Count > 1)
                        await Navigation.PopAsync();
                }
                else if (isNew || Navigation.NavigationStack.Count == 1)
                {
                    await Navigation.PushAsync(page);
                    while (Navigation.NavigationStack.Count > 2)
                        Navigation.RemovePage(Navigation.NavigationStack[1]);
                }
                else
                {
                    Navigation.InsertPageBefore(
                        page: page,
                        before: Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]
                    );
                    await Navigation.PopAsync();
                }

                if (page is not null)
                {
                    SetHasBackButton(
                        page: page,
                        value: ResolveHasBackButton(view)
                    );
                }
            }
        );
    }

    /// <summary>
    /// Call the OnBack action if it's set.
    /// </summary>
    /// <returns>Always returns true.</returns>
    protected override bool OnBackButtonPressed()
    {
        if (OnBack?.Invoke() != true && Source is IStackPresenter presenter)
            presenter.Pop();

        return true;
    }

    /// <summary>
    /// Determine if the back button should be shown for the given view.
    /// </summary>
    /// <param name="view">The view to evaluate.</param>
    /// <returns>
    /// true if the back button should be shown for the given view.
    /// </returns>
    private bool ResolveHasBackButton(IView? view)
    {
        if (view is null)
            return false;

        if (HasBack is not null)
            return HasBack(view);

        if (Source is IStackPresenter presenter)
            return presenter.ViewModels.Length > 1;

        return false;
    }

    #endregion
}
