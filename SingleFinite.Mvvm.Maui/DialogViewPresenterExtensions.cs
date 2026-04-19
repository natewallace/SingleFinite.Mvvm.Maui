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
/// Extension members for <see cref="DialogViewPresenter"/>
/// </summary>
public static class DialogViewPresenterExtensions
{
    extension(DialogViewPresenter viewPresenter)
    {
        /// <summary>
        /// Set the Source property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter Source(IPresenter value) =>
            viewPresenter.Also(it => it.Source = value);

        /// <summary>
        /// Set the BackgroundColor Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter ScrimBackgroundColor(Color value) =>
            viewPresenter.Also(it => it.ScrimBackgroundColor = value);

        /// <summary>
        /// Set the DialogStyle Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter DialogStyle(Style value) =>
            viewPresenter.Also(it => it.DialogStyle = value);

        /// <summary>
        /// Set the ScrimEnterAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter ScrimEnterAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.ScrimEnterAnimation = value);

        /// <summary>
        /// Set the ScrimExitAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter ScrimExitAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.ScrimExitAnimation = value);

        /// <summary>
        /// Set the FirstDialogEnterAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter? FirstDialogEnterAnimation(IViewAnimation? value) =>
            viewPresenter.Also(it => it.FirstDialogEnterAnimation = value);

        /// <summary>
        /// Set the FirstDialogExitAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter? FirstDialogExitAnimation(IViewAnimation? value) =>
            viewPresenter.Also(it => it.FirstDialogExitAnimation = value);

        /// <summary>
        /// Set the DialogEnterForwardAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter DialogEnterForwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.DialogEnterForwardAnimation = value);

        /// <summary>
        /// Set the DialogEnterBackwardAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter DialogEnterBackwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.DialogEnterBackwardAnimation = value);

        /// <summary>
        /// Set the DialogExitForwardAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter DialogExitForwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.DialogExitForwardAnimation = value);

        /// <summary>
        /// Set the DialogExitBackwardAnimation Property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The DialogViewPresenter.</returns>
        public DialogViewPresenter DialogExitBackwardAnimation(IViewAnimation value) =>
            viewPresenter.Also(it => it.DialogExitBackwardAnimation = value);
    }
}
