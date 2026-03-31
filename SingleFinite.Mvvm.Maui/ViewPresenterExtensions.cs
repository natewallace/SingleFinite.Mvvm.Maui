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
/// Extension members for <see cref="ViewPresenter"/>
/// </summary>
public static class ViewPresenterExtensions
{
    extension(ViewPresenter viewPresenter)
    {
        /// <summary>
        /// Set the Source property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The ViewPresenter.</returns>
        public ViewPresenter Source(IPresenter value) =>
            viewPresenter.Also(it => it.Source = value);

        /// <summary>
        /// Set the EnterForwardAnimation property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The ViewPresenter.</returns>
        public ViewPresenter EnterForwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.EnterForwardAnimation = value);

        /// <summary>
        /// Set the EnterBackwardAnimation property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The ViewPresenter.</returns>
        public ViewPresenter EnterBackwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.EnterBackwardAnimation = value);

        /// <summary>
        /// Set the ExitForwardAnimation property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The ViewPresenter.</returns>
        public ViewPresenter ExitForwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.ExitForwardAnimation = value);

        /// <summary>
        /// Set the ExitBackwardAnimation property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The ViewPresenter.</returns>
        public ViewPresenter ExitBackwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.ExitBackwardAnimation = value);
    }
}
