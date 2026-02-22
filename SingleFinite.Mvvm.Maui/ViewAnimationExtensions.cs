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
/// Extension members for ViewAnimation.
/// </summary>
public static class ViewAnimationExtensions
{
    /// <summary>
    /// Animation duration in milleseconds used as the default.
    /// </summary>
    private const uint DefaultAnimationDuration = 375;

    extension(ViewAnimation)
    {
        /// <summary>
        /// A ViewAnimation that does nothing.
        /// </summary>
        public static ViewAnimation None() => new ViewAnimationCustom(
            runAsync: _ => Task.FromResult(false)
        );

        /// <summary>
        /// Create a custom animation.
        /// </summary>
        /// <param name="runAsync">The function called by the RunAsync method.</param>
        /// <param name="initialize">
        /// The optional action called by the Initialize method.
        /// </param>
        /// <returns>A custom ViewAnimation.</returns>
        public static ViewAnimation Custom(
            Action<View>? initialize = null,
            Func<View, Task<bool>>? runAsync = null
        ) => new ViewAnimationCustom(
            initialize: initialize,
            runAsync: runAsync ?? (_ => Task.FromResult(true))
        );

        /// <summary>
        /// Create an animation that fades a view in.
        /// </summary>
        /// <param name="duration">
        /// The duration for the animation in milliseconds.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the animation.  If not specified the
        /// Easing.SinIn function is used.
        /// </param>
        /// <returns>A ViewAnimation that fades a view in.</returns>
        public static ViewAnimation FadeIn(
            uint duration = DefaultAnimationDuration,
            Easing? easing = null
        ) => new ViewAnimationCustom(
            initialize: view => view.Opacity = 0.0,
            runAsync: view => view.FadeToAsync(
                opacity: 1.0,
                length: duration,
                easing: easing ?? Easing.SinIn
            )
        );

        /// <summary>
        /// Create an animation that fades a view out.
        /// </summary>
        /// <param name="duration">
        /// The duration for the animation in milliseconds.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the animation.  If not specified the
        /// Easing.SinOut function is used.
        /// </param>
        /// <returns>A ViewAnimation that fades a view out.</returns>
        public static ViewAnimation FadeOut(
            uint duration = DefaultAnimationDuration,
            Easing? easing = null
        ) => new ViewAnimationCustom(
            runAsync: view => view.FadeToAsync(
                opacity: 0.0,
                length: duration,
                easing: easing ?? Easing.SinOut
            )
        );

        /// <summary>
        /// Create an animation that scales a view in.
        /// </summary>
        /// <param name="duration">
        /// The duration for the animation in milliseconds.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the animation.  If not specified the
        /// Easing.SinIn function is used.
        /// </param>
        /// <returns>A ViewAnimation that scales a view in.</returns>
        public static ViewAnimation ScaleIn(
            uint duration = DefaultAnimationDuration,
            Easing? easing = null
        ) => new ViewAnimationCustom(
            initialize: view => view.Scale = 0.0,
            runAsync: view => view.ScaleToAsync(
                scale: 1.0,
                length: duration,
                easing: easing ?? Easing.SinIn
            )
        );

        /// <summary>
        /// Create an animation that scales a view out.
        /// </summary>
        /// <param name="duration">
        /// The duration for the animation in milliseconds.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the animation.  If not specified the
        /// Easing.SinIn function is used.
        /// </param>
        /// <returns>A ViewAnimation that scales a view out.</returns>
        public static ViewAnimation ScaleOut(
            uint duration = DefaultAnimationDuration,
            Easing? easing = null
        ) => new ViewAnimationCustom(
            runAsync: view => view.ScaleToAsync(
                scale: 0.0,
                length: duration,
                easing: easing ?? Easing.SinOut
            )
        );

        /// <summary>
        /// Create an animation that translates the view position.
        /// </summary>
        /// <param name="x">The target x coordinate.</param>
        /// <param name="y">The target y coordinate.</param>
        /// <param name="initialX">The starting x coordinate.</param>
        /// <param name="initialY">The starting y coordinate.</param>
        /// <param name="duration">
        /// The duration for the animation in milliseconds.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the animation.  If not specified the
        /// Easing.SinInOut function is used.
        /// </param>
        /// <returns>
        /// A ViewAnimation that translates the view position.
        /// </returns>
        public static ViewAnimation Translate(
            double x,
            double y,
            double initialX = 0.0,
            double initialY = 0.0,
            uint duration = DefaultAnimationDuration,
            Easing? easing = null
        ) => new ViewAnimationCustom(
            initialize: view => 
            {
                view.TranslationX = initialX;
                view.TranslationY = initialY;
            },
            runAsync: view => view.TranslateToAsync(
                x: x,
                y: y,
                length: duration,
                easing: easing ?? Easing.SinOut
            )
        );
    }
}
