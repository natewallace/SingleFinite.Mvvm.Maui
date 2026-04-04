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

namespace Example.Models;

/// <summary>
/// The possible app theme settings.
/// </summary>
public class AppTheme
{
    #region Fields

    /// <summary>
    /// Holds the name of the app theme.
    /// </summary>
    private readonly string _name;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">The name of the app theme.</param>
    private AppTheme(string name)
    {
        _name = name;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The app theme defined for the system.
    /// </summary>
    public readonly static AppTheme System = new("System");

    /// <summary>
    /// The light app theme.
    /// </summary>
    public readonly static AppTheme Light = new("Light");

    /// <summary>
    /// The dark app theme.
    /// </summary>
    public readonly static AppTheme Dark = new("Dark");

    /// <summary>
    /// All app themes.
    /// </summary>
    public readonly static AppTheme[] All =
    [
        System,
        Light,
        Dark
    ];

    #endregion

    #region Methods

    /// <summary>
    /// Convert this theme into an equivalent value.
    /// </summary>
    /// <typeparam name="TType">The type of the equivalent value.</typeparam>
    /// <param name="system">
    /// The value to return when the theme is System.
    /// </param>
    /// <param name="light">The value to return when the theme is Light.</param>
    /// <param name="dark">The value to return when the theme is Dark.</param>
    /// <returns>The converted value.</returns>
    public TType Convert<TType>(
        TType system,
        TType light,
        TType dark
    )
    {
        if (this == System)
            return system;
        if (this == Light)
            return light;
        if (this == Dark)
            return dark;
        throw new InvalidOperationException($"Unknown app theme: {this}");
    }

    /// <summary>
    /// Returns the name of the app theme.
    /// </summary>
    /// <returns>The name of the app theme.</returns>
    public override string ToString() => _name;

    #endregion
}
