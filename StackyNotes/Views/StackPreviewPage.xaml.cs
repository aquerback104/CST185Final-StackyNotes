using Microsoft.Maui.Controls;

namespace StackyNotes.Views;

[QueryProperty(nameof(NoteText), "noteText")]
public partial class StackPreviewPage : ContentPage
{
    private string noteText = string.Empty;

    public string NoteText
    {
        get => noteText;
        set
        {
            noteText = Uri.UnescapeDataString(value ?? string.Empty);
            ReceivedNoteLabel.Text = $"You sent: {noteText}";
        }
    }

    public StackPreviewPage()
    {
        InitializeComponent();
    }

    private async void OnOpenStacksClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("stacks");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
