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

using Example.Models.Services;
using SingleFinite.Essentials;
using SingleFinite.Mvvm;

namespace Example.Models;

/// <summary>
/// The view model for settings.
/// </summary>
/// <param name="appThemeManager">App theme manager service.</param>
public class SettingsViewModel(IAppThemeManager appThemeManager) :
    ViewModel,
    ICloseObservable
{
    #region Properties

    /// <summary>
    /// The current app theme.
    /// </summary>
    public AppTheme? CurrentAppTheme
    {
        get;
        set => ChangeProperty(
            field: ref field,
            value: value,
            onPropertyChanged: () => appThemeManager.Current = value ?? AppTheme.System
        );
    }

    #endregion

    #region Methods

    /// <summary>
    /// Close the settings dialog.
    /// </summary>
    public void Close() => _closedSource.Emit(this);

    /// <summary>
    /// Set initial values.
    /// </summary>
    protected override void OnCreated()
    {
        CurrentAppTheme = appThemeManager.Current;
    }

    /// <summary>
    /// Attach observers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to use.</param>
    protected override void OnActivate(CancellationToken cancellationToken)
    {
        appThemeManager.CurrentChanged
            .Observe()
            .OnEach(appTheme => CurrentAppTheme = appTheme)
            .Until(cancellationToken);
    }

    #endregion

    #region Events

    /// <summary>
    /// Emits when a request has been made to close the settings dialog.
    /// </summary>
    public IEventObservable<ICloseObservable> Closed => _closedSource.Observable;
    private readonly EventObservableSource<ICloseObservable> _closedSource = new();

    #endregion
}
