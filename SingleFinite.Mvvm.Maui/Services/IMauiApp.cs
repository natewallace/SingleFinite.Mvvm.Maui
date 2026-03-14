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

namespace SingleFinite.Mvvm.Maui.Services;

/// <summary>
/// The root object for the application responsible for initializing the
/// host window and launching it.
/// </summary>
public interface IMauiApp
{
    /// <summary>
    /// The main window for the app.
    /// </summary>
    Window Window { get; }

    /// <summary>
    /// Start the app.
    /// </summary>
    void Start();

    /// <summary>
    /// Bring the app to the foreground.
    /// </summary>
    void Activate();
}

/// <summary>
/// The root object for the application responsible for initializing the
/// host window and launching it. 
/// </summary>
/// <typeparam name="TViewModel">The main view model type.</typeparam>
public interface IMauiApp<TViewModel> : IMauiApp where TViewModel : IViewModel
{
    /// <summary>
    /// The main view for the app.
    /// </summary>
    IView<TViewModel> View { get; }
}
