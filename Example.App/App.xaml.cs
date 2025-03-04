// MIT License
// Copyright (c) 2024 Single Finite
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

using SingleFinite.Example.Models;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Maui;
using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace Example.App;

public partial class App : Application
{
    #region Fields

    /// <summary>
    /// Holds the app host.
    /// </summary>
    private IAppHost? _appHost = null;

    #endregion

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        _appHost ??= new AppHostBuilder()
            .AddMaui<MainViewModel>()
            .AddExampleViews()
            .BuildAndStart();

        return _appHost.ServiceProvider.GetRequiredService<IMainWindow>().Current ??
            throw new InvalidOperationException("The main window isn't set.");
    }
}
