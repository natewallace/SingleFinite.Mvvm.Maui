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

using System.ComponentModel;
using Microsoft.Maui.Controls.Shapes;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// A dialog that is displayed within a DialogViewPresenter.
/// </summary>
internal partial class Dialog : TemplatedView
{
    #region Fields

    /// <summary>
    /// The dialog content.
    /// </summary>
    public static readonly BindableProperty DialogContentProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogContent),
            returnType: typeof(object),
            declaringType: typeof(Dialog),
            defaultValue: null
        );

    /// <summary>
    /// The background color for the dialog.
    /// </summary>
    public static readonly BindableProperty DialogBackgroundColorProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(Dialog),
            defaultValue: null
        );

    /// <summary>
    /// The dialog shape.
    /// </summary>
    public static readonly BindableProperty DialogShapeProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogShape),
            returnType: typeof(IShape),
            declaringType: typeof(Dialog),
            defaultValue: new Rectangle()
        );

    /// <summary>
    /// The dialog shadow.
    /// </summary>
    public static readonly BindableProperty DialogShadowProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogShadow),
            returnType: typeof(Shadow),
            declaringType: typeof(Dialog),
            defaultValue: null
        );

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public Dialog()
    {
        // Dialogs are designed for one time use.
        //
        this.Unloaded += (_, _) =>
        {
            DialogContent = null;
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// The dialog content.
    /// </summary>
    public object? DialogContent
    {
        get => GetValue(DialogContentProperty);
        set => SetValue(DialogContentProperty, value);
    }

    /// <summary>
    /// The background color for the dialog.
    /// </summary>
    public Color DialogBackgroundColor
    {
        get => (Color)GetValue(DialogBackgroundColorProperty);
        set => SetValue(DialogBackgroundColorProperty, value);
    }

    /// <summary>
    /// The dialog shape.
    /// </summary>
    [TypeConverter(typeof(StrokeShapeTypeConverter))]
    public IShape? DialogShape
    {
        get => (IShape)GetValue(DialogShapeProperty);
        set => SetValue(DialogShapeProperty, value);
    }

    /// <summary>
    /// The dialog shadow.
    /// </summary>
    [TypeConverter(typeof(ShadowTypeConverter))]
    public Shadow DialogShadow
    {
        get => (Shadow)GetValue(DialogShadowProperty);
        set => SetValue(DialogShadowProperty, value);
    }

    #endregion
}
