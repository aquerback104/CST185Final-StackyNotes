using StackyNotes.Data;
using StackyNotes.Models;

namespace StackyNotes.Views;

[QueryProperty(nameof(CardId), "id")]
public partial class EditNotePage : ContentPage
{
    private readonly AppDatabase _database;
    private NoteCard? _currentCard;
    private int _cardId;

    public string CardId
    {
        get => _cardId.ToString();
        set
        {
            if (int.TryParse(value, out var parsedId))
            {
                _cardId = parsedId;
                _ = LoadCardAsync();
            }
        }
    }

    public EditNotePage()
    {
        InitializeComponent();
        _database = IPlatformApplication.Current?.Services.GetService<AppDatabase>() ?? new AppDatabase();
    }

    public EditNotePage(AppDatabase database)
    {
        InitializeComponent();
        _database = database;
    }

    private async Task LoadCardAsync()
    {
        if (_cardId <= 0)
        {
            StatusLabel.Text = "No card was selected.";
            return;
        }

        _currentCard = await _database.GetCardAsync(_cardId);

        if (_currentCard is null)
        {
            StatusLabel.Text = "This card could not be found. It may have already been deleted.";
            return;
        }

        TitleEntry.Text = _currentCard.Title;
        ContentEditor.Text = _currentCard.Content;
        StackNameEntry.Text = _currentCard.StackName;
        CompletedCheckBox.IsChecked = _currentCard.IsCompleted;
        StatusLabel.Text = $"Editing card #{_currentCard.Id}.";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_currentCard is null)
        {
            StatusLabel.Text = "Load a card before saving changes.";
            return;
        }

        var title = (TitleEntry.Text ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            StatusLabel.Text = "Card title is required before saving.";
            return;
        }

        _currentCard.Title = title;
        _currentCard.Content = string.IsNullOrWhiteSpace(ContentEditor.Text) ? "Quick reminder" : ContentEditor.Text.Trim();
        _currentCard.StackName = string.IsNullOrWhiteSpace(StackNameEntry.Text) ? "My Saved Stack" : StackNameEntry.Text.Trim();
        _currentCard.IsCompleted = CompletedCheckBox.IsChecked;

        await _database.SaveCardAsync(_currentCard);
        StatusLabel.Text = "Changes saved to SQLite.";
        await Shell.Current.GoToAsync("..");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_currentCard is null)
        {
            StatusLabel.Text = "Load a card before deleting.";
            return;
        }

        await _database.DeleteCardAsync(_currentCard);
        StatusLabel.Text = "Card deleted from SQLite.";
        await Shell.Current.GoToAsync("..");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
