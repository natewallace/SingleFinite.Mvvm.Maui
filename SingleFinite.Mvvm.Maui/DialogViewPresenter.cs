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
using SingleFinite.Essentials;
using SingleFinite.Mvvm.Services.Presenters;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// A view that hosts dialog views.
/// </summary>
public partial class DialogViewPresenter : TemplatedView
{
    #region Fields

    /// <summary>
    /// The background color for the scrim.
    /// </summary>
    public static readonly BindableProperty ScrimBackgroundColorProperty =
        BindableProperty.Create(
            propertyName: nameof(ScrimBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(DialogViewPresenter),
            defaultValue: null
        );

    /// <summary>
    /// The background color for the dialog.
    /// </summary>
    public static readonly BindableProperty DialogBackgroundColorProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(DialogViewPresenter),
            defaultValue: null
        );

    /// <summary>
    /// The dialog shape.
    /// </summary>
    public static readonly BindableProperty DialogShapeProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogShape),
            returnType: typeof(IShape),
            declaringType: typeof(DialogViewPresenter),
            defaultValue: new Rectangle()
        );

    /// <summary>
    /// The dialog shadow.
    /// </summary>
    public static readonly BindableProperty DialogShadowProperty =
        BindableProperty.Create(
            propertyName: nameof(DialogShadow),
            returnType: typeof(Shadow),
            declaringType: typeof(DialogViewPresenter),
            defaultValue: null
        );

    /// <summary>
    /// Enable or disable the DismissRequested event from being raised when the
    /// user taps outside of the dialog.
    /// </summary>
    public static readonly BindableProperty DismissOnTouchOutsideDialogProperty =
        BindableProperty.Create(
            propertyName: nameof(DismissOnTouchOutsideDialog),
            returnType: typeof(bool),
            declaringType: typeof(DialogViewPresenter),
            defaultValue: false
        );

    /// <summary>
    /// Key for IsDialogOpenProperty.
    /// </summary>
    private static readonly BindablePropertyKey s_isDialogOpenPropertyKey =
        BindableProperty.CreateReadOnly(
            propertyName: nameof(IsDialogOpen),
            returnType: typeof(bool),
            declaringType: typeof(DialogViewPresenter),
            defaultValue: false,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is DialogViewPresenter presenter)
                {
                    presenter.IsDialogOpenChanged?.Invoke(
                        sender: presenter,
                        e: (bool)newValue
                    );
                }
            }
        );

    /// <summary>
    /// Indicates if a dialog is currently open.
    /// </summary>
    public static readonly BindableProperty IsDialogOpenProperty =
        s_isDialogOpenPropertyKey.BindableProperty;

    /// <summary>
    /// The controls read in from the control template.  This will be null until
    /// the template has been applied.
    /// </summary>
    private TemplateControls? _templateControls;

    /// <summary>
    /// Observer for view changes on the source presenter.
    /// </summary>
    private IDisposable? _sourceViewObserver;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public DialogViewPresenter()
    {
        Loaded += (_, _) =>
        {
            if (!IsSubscribeManaged)
                SubscribeToPresenter();
        };
        Unloaded += (_, _) =>
        {
            if (!IsSubscribeManaged)
                UnsubscribeFromPresenter();
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// The background color for the scrim.
    /// </summary>
    public Color ScrimBackgroundColor
    {
        get => (Color)GetValue(ScrimBackgroundColorProperty);
        set => SetValue(ScrimBackgroundColorProperty, value);
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

    /// <summary>
    /// The animation to run when the scrim enters.
    /// </summary>
    public IViewAnimation ScrimEnterAnimation { get; set; } =
        IViewAnimation.FadeIn();

    /// <summary>
    /// The animation to run when the scrim exits.
    /// </summary>
    public IViewAnimation ScrimExitAnimation { get; set; } =
        IViewAnimation.FadeOut();

    /// <summary>
    /// The animation to run when the first dialog enters.  If not set the
    /// DialogEnterForwardAnimation value will be used.
    /// </summary>
    public IViewAnimation? FirstDialogEnterAnimation { get; set; }

    /// <summary>
    /// The animation to run when the first dialog exits.  If not set the
    /// DialogExitBackwardAnimation value will be used.
    /// </summary>
    public IViewAnimation? FirstDialogExitAnimation { get; set; }

    /// <summary>
    /// The animation to run when a dialog enters going forward.
    /// </summary>
    public IViewAnimation DialogEnterForwardAnimation { get; set; } =
        IViewAnimation.FadeIn();

    /// <summary>
    /// The animation to run when a content enters going backward.
    /// </summary>
    public IViewAnimation DialogEnterBackwardAnimation { get; set; } =
        IViewAnimation.FadeIn();

    /// <summary>
    /// The animation to run when a dialog exits going forward.
    /// </summary>
    public IViewAnimation DialogExitForwardAnimation { get; set; } =
        IViewAnimation.FadeOut();

    /// <summary>
    /// The animation to run when a dialog exits going backward.
    /// </summary>
    public IViewAnimation DialogExitBackwardAnimation { get; set; } =
        IViewAnimation.FadeOut();

    /// <summary>
    /// Enable or disable the DismissRequested event from being raised when the
    /// user taps outside of the dialog.
    /// </summary>
    public bool DismissOnTouchOutsideDialog
    {
        get => (bool)GetValue(DismissOnTouchOutsideDialogProperty);
        set => SetValue(DismissOnTouchOutsideDialogProperty, value);
    }

    /// <summary>
    /// Indicates if a dialog is currently open.
    /// </summary>
    public bool IsDialogOpen
    {
        get => (bool)GetValue(IsDialogOpenProperty);
        private set => SetValue(s_isDialogOpenPropertyKey, value);
    }

    /// <summary>
    /// When set to true the Subscribe and Unsubscribe methods will be called
    /// by an external control instead of when this control is loaded and
    /// unloaded.
    /// </summary>
    internal bool IsSubscribeManaged { get; set; } = false;

    /// <summary>
    /// The presenter whose views will be displayed in this control.
    /// </summary>
    public IPresenter? Source
    {
        get;
        set
        {
            if (field == value)
                return;

            UnsubscribeFromPresenter();

            field = value;

            SubscribeToPresenter();
        }
    }

    /// <summary>
    /// Get the control from the template with the matching name.
    /// </summary>
    /// <typeparam name="TType">The type of control.</typeparam>
    /// <param name="name">The name of the control.</param>
    /// <returns>The control with the matching name.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when a control with the given name isn't found in the template.
    /// </exception>
    private TType GetTemplateControl<TType>(string name)
    {
        var child = GetTemplateChild(name) ??
            throw new ArgumentException(
                $"Unable to find element with name '{name}' in XAML template."
            );

        return (TType)child;
    }

    /// <summary>
    /// Read in controls from the control template.
    /// </summary>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _templateControls = new(
            Root: GetTemplateControl<View>("Root"),
            Scrim: GetTemplateControl<View>("Scrim"),
            ContentPresenter: GetTemplateControl<AnimatedContent>("ContentPresenter")
        );

        _templateControls.Scrim.GestureRecognizers.Add(
            new TapGestureRecognizer()
                .Also(gesture => gesture.Tapped += OnScrimTapped)
        );
    }

    /// <summary>
    /// If DismissOnTouchOutsideDialog is enabled emit the DismissRequested
    /// event.
    /// </summary>
    /// <param name="sender">Object that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnScrimTapped(object? sender, TappedEventArgs e)
    {
        if (DismissOnTouchOutsideDialog)
            DismissRequested?.Invoke(
                sender: this,
                e: EventArgs.Empty
            );
    }

    /// <summary>
    /// Start observing changes to the current view of the presenter.
    /// </summary>
    internal void SubscribeToPresenter()
    {
        if (_sourceViewObserver is not null)
            return;

        _sourceViewObserver = Source?.CurrentChanged
            ?.Observe()
            ?.OnEach(
                args => UpdateSourceContent(
                    view: args.View,
                    isNew: args.IsNew
                )
            );

        UpdateSourceContent(
            view: Source?.Current,
            isNew: true
        );
    }

    /// <summary>
    /// Stop observing changes to the current view of the presenter.
    /// </summary>
    internal void UnsubscribeFromPresenter()
    {
        _sourceViewObserver?.Dispose();
        _sourceViewObserver = null;
    }

    /// <summary>
    /// Update the content for the view being displayed.
    /// </summary>
    /// <param name="view">The view being displayed.</param>
    /// <param name="isNew">Indicates if the view was newly added.</param>
    private void UpdateSourceContent(IView? view, bool isNew)
    {
        if (_templateControls is null)
            return;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            List<Task> tasks = [];

            Dialog? dialog = null;
            if (view is not null)
            {
                dialog = new Dialog
                {
                    DialogBackgroundColor = DialogBackgroundColor,
                    DialogShape = DialogShape,
                    DialogShadow = DialogShadow,
                    DialogContent = view
                };
            }

            if (dialog is not null && !_templateControls.Root.IsVisible)
            {
                ScrimEnterAnimation.Initialize(_templateControls.Scrim);

                _templateControls.Root.IsVisible = true;
                IsDialogOpen = true;

                tasks.Add(ScrimEnterAnimation.RunAsync(_templateControls.Scrim));
                tasks.Add(
                    _templateControls.ContentPresenter.SetContentAsync(
                        view: dialog,
                        enterAnimation: FirstDialogEnterAnimation ?? DialogEnterForwardAnimation,
                        exitAnimation: DialogExitForwardAnimation
                    )
                );

                await Task.WhenAll(tasks);
            }
            else if (dialog is null && _templateControls.Root.IsVisible)
            {
                ScrimExitAnimation.Initialize(_templateControls.Scrim);

                tasks.Add(ScrimExitAnimation.RunAsync(_templateControls.Scrim));
                tasks.Add(
                    _templateControls.ContentPresenter.SetContentAsync(
                        view: dialog,
                        enterAnimation: DialogEnterBackwardAnimation,
                        exitAnimation: FirstDialogExitAnimation ?? DialogExitBackwardAnimation
                    )
                );

                await Task.WhenAll(tasks);

                _templateControls.Root.IsVisible = false;
                IsDialogOpen = false;
            }
            else if (dialog is not null)
            {
                await _templateControls.ContentPresenter.SetContentAsync(
                    view: dialog,
                    enterAnimation: isNew ? DialogEnterForwardAnimation : DialogEnterBackwardAnimation,
                    exitAnimation: isNew ? DialogExitForwardAnimation : DialogExitBackwardAnimation
                );
            }
        });
    }

    #endregion

    #region Events

    /// <summary>
    /// Event raised when the user has tapped on the are outside of the dialog.
    /// If the DismissOnTouchOutsideDialog property is false this event will not
    /// be raised.
    /// </summary>
    public event EventHandler? DismissRequested;

    /// <summary>
    /// Event raised when the IsDialogOpen property changes.
    /// </summary>
    public event EventHandler<bool>? IsDialogOpenChanged;

    #endregion

    #region Types

    /// <summary>
    /// The controls read in from the template.
    /// </summary>
    /// <param name="Root">The root level control.</param>
    /// <param name="Scrim">The scrim control.</param>
    /// <param name="ContentPresenter">The content presenter.</param>
    private record TemplateControls(
        View Root,
        View Scrim,
        AnimatedContent ContentPresenter
    );

    #endregion
}
