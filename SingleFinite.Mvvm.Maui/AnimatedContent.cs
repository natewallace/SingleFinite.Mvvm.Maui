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

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// Animates the transition from one view to another.
/// </summary>
public partial class AnimatedContent : TemplatedView
{
    #region Fields

    /// <summary>
    /// The underlying control that is used to display views.
    /// </summary>
    private readonly Grid _layout = [];

    #endregion

    #region Methods

    /// <summary>
    /// Get the content presenter from the template and assign the layout to it.
    /// </summary>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        var contentPresenter = GetTemplateChild("ContentPresenter")
            as ContentPresenter;
        contentPresenter?.Content = _layout;
    }

    /// <summary>
    /// Transition from the current view to the provided view using the
    /// ExitTransition and EnterTransition animations respectively.
    /// </summary>
    /// <param name="view">The view to transition to.</param>
    /// <param name="enterTransition">
    /// The animation to run on the view that is entering.
    /// This defaults to ViewAnimation.FadeIn() if one is not specified.
    /// </param>
    /// <param name="exitTransition">
    /// The animation to run on the view that is exiting.
    /// This defaults to ViewAnimation.FadeOut() if one is not specified.
    /// </param>
    /// <returns>
    /// A task that completes once all of the transition animations have
    /// finished.
    /// </returns>
    public async Task SetContentAsync(
        View? view,
        ViewAnimation? enterTransition = null,
        ViewAnimation? exitTransition = null
    )
    {
        var enterAnimation = enterTransition ?? ViewAnimation.FadeIn();
        var exitAnimation = exitTransition ?? ViewAnimation.FadeOut();

        var exitingView = _layout.Children.FirstOrDefault() as View;
        if (view is not null && view == exitingView)
            return;

        if (exitingView is not null)
        {
            exitingView.CancelAnimations();
        }

        if (view is not null)
        {
            view.CancelAnimations();
            _layout.Children.Add(view);
        }

        var exitTask = exitingView is not null ?
            exitAnimation.RunAsync(exitingView) :
            Task.CompletedTask;

        var enterTask = view is not null ?
            enterAnimation.RunAsync(view) :
            Task.CompletedTask;

        await Task.WhenAll(exitTask, enterTask);

        if (view is null)
        {
            _layout.Children.Clear();
        }
        else
        {
            while (_layout.Children.Count > 1)
                _layout.RemoveAt(0);
        }
    }

    #endregion
}
