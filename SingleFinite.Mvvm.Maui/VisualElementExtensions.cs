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
/// Extension methods for the VisualElement class.
/// </summary>
public static class VisualElementExtensions
{
    extension(VisualElement visual)
    {
        /// <summary>
        /// Get the resource with the given key.
        /// </summary>
        /// <typeparam name="TResource">The type of resource to get.</typeparam>
        /// <param name="key">The key of the resource to get.</param>
        /// <returns>
        /// The specified resource.  If the resource is not found an exception
        /// will be thrown.
        /// </returns>
        public TResource GetResource<TResource>(string key)
        {
            if (visual.Resources.TryGetValue(key, out var resource))
                return (TResource)resource;

            return (TResource)Application.Current.ThrowIfNull().Resources[key];
        }

        /// <summary>
        /// Get the current location of the VisualElement relative to the
        /// window.
        /// </summary>
        /// <returns>
        /// The current location of the VisualElement relative to the window.
        /// </returns>
        public WindowRelativeLocation GetWindowRelativeLocation()
        {
            var x = 0.0;
            var y = 0.0;
            var width = 0.0;
            var height = 0.0;
            var current = visual;

            Thickness? margin = null;

            while (current is not null)
            {
                x += current.X;
                y += current.Y;
                width = current.Width;
                height = current.Height;
                margin = (current as View)?.Margin;
                current = current.Parent as VisualElement;
            }

            if (current?.Parent is Window window)
            {
                width = window.Width;
                height = window.Height;
            }

            return new WindowRelativeLocation(
                VisualElementX: x,
                VisualElementY: y,
                VisualElementWidth: visual.Width,
                VisualElementHeight: visual.Height,
                WindowWidth: width,
                WindowHeight: height
            );
        }
    }
}
