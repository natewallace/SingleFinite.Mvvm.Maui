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

using SingleFinite.Mvvm.Maui.Internal;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// An animation that can be applied to a View.
/// </summary>
public abstract class ViewAnimation
{
    #region Methods

    /// <summary>
    /// Perform any initialization needed on the view before the animation is
    /// run.
    /// </summary>
    /// <param name="view">The view to initialize.</param>
    public abstract void Initialize(View view);

    /// <summary>
    /// Run this animation on the view.
    /// </summary>
    /// <param name="view">The view to animate.</param>
    /// <returns>
    /// A task that completes once the animation has finished.  If the task
    /// result is true it indicates that the animation was canceled.
    /// </returns>
    public abstract Task<bool> RunAsync(View view);

    /// <summary>
    /// Combine two ViewAnimation instances into a single ViewAnimation that
    /// runs both animations in parallel.
    /// </summary>
    /// <param name="va1">
    /// The first ViewAnimation to combine with the second.
    /// </param>
    /// <param name="va2">
    /// The second ViewAnimation to combine with the first.
    /// </param>
    /// <returns>
    /// A new ViewAnimation that runs both provided ViewAnimations in parallel.
    /// </returns>
    public static ViewAnimation operator +(ViewAnimation va1, ViewAnimation va2) =>
        new ViewAnimationCollection(va1, va2);

    #endregion
}
