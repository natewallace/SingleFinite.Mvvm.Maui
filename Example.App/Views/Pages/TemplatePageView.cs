using CommunityToolkit.Maui.Markup;
using SingleFinite.Essentials;

namespace Example.App.Views.Pages;

/// <summary>
/// The common template for page views.
/// </summary>
public class TemplatePageView : ContentView
{
    #region Fields

    /// <summary>
    /// The title.
    /// </summary>
    private readonly Label _titleLabel = new();

    /// <summary>
    /// The content.
    /// </summary>
    private readonly ContentPresenter _contentPresenter = new();

    /// <summary>
    /// The back button.
    /// </summary>
    private readonly Button _backButton = new();

    /// <summary>
    /// The next button.
    /// </summary>
    private readonly Button _nextButton = new();

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplatePageView()
    {
        base.Content = new Border
        {
            Stroke = SolidColorBrush.DarkGray,
            Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto }
                },
                Children =
                {
                    _titleLabel.Row(0),
                    _contentPresenter.Row(1),
                    new Grid
                    {
                        Padding = 24,
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = GridLength.Auto }
                        },
                        Children =
                        {
                            _backButton
                                .Row(0)
                                .IsVisible(false)
                                .Text("Back")
                                .Also(it => it.Clicked += (_, _) => OnBack?.Invoke()),

                            _nextButton
                                .Row(0)
                                .Column(2)
                                .IsVisible(false)
                                .Text("Next")
                                .Also(it => it.Clicked += (_, _) => OnNext?.Invoke())
                        }
                    }.Row(2)
                }
            }
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// The title.
    /// </summary>
    public string Title
    {
        get => _titleLabel.Text;
        set => _titleLabel.Text = value;
    }

    /// <summary>
    /// The content.
    /// </summary>
    public new View Content
    {
        get => _contentPresenter.Content;
        set => _contentPresenter.Content = value;
    }

    /// <summary>
    /// Optional action to invoke when the next button is tapped.  If this is
    /// null the next button will be hidden.
    /// </summary>
    public Action? OnNext
    {
        get;
        set
        {
            field = value;
            _nextButton.IsVisible = value != null;
        }
    }

    /// <summary>
    /// Optional action to invoke when the back button is tapped.  If this is
    /// null the back button will be hidden.
    /// </summary>
    public Action? OnBack
    {
        get;
        set
        {
            field = value;
            _backButton.IsVisible = value != null;
        }
    }

    #endregion
}
