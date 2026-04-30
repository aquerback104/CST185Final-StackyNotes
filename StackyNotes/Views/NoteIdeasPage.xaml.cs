using StackyNotes.Services;

namespace StackyNotes.Views;

public partial class NoteIdeasPage : ContentPage
{
    private readonly NoteIdeasService _noteIdeasService = new();
    private bool _hasLoaded;

    public NoteIdeasPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_hasLoaded)
        {
            return;
        }

        _hasLoaded = true;
        await LoadIdeasAsync();
    }

    private async void OnReloadClicked(object sender, EventArgs e)
    {
        await LoadIdeasAsync();
    }

    private async Task LoadIdeasAsync()
    {
        try
        {
            SetLoadingState(true, "Loading note ideas...");

            var ideas = await _noteIdeasService.LoadIdeasAsync();
            IdeasCollectionView.ItemsSource = ideas;
            StatusLabel.Text = $"Loaded {ideas.Count} real note ideas from local JSON.";
        }
        catch
        {
            IdeasCollectionView.ItemsSource = null;
            StatusLabel.Text = "Could not load data right now. Please try again.";
        }
        finally
        {
            SetLoadingState(false, StatusLabel.Text);
        }
    }

    private void SetLoadingState(bool isLoading, string message)
    {
        LoadingIndicator.IsVisible = isLoading;
        LoadingIndicator.IsRunning = isLoading;
        StatusLabel.Text = message;
    }
}
