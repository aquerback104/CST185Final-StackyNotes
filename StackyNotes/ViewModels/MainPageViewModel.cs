using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace StackyNotes.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _welcomeTitle = "Welcome to StackyNotes";
        private string _introMessage = "Create a quick reminder, preview it, or jump into your saved stack cards.";
        private string _noteText = string.Empty;
        private string _resultMessage = "Add a short note to get started.";
        private Color _resultColor = Color.FromArgb("#6A6A6A");
        private string _tipMessage = "Use clear card titles so search works better once your stack grows.";
        private bool _showIdeasShortcut = true;
        private bool _showHelperTip = true;

        public string WelcomeTitle
        {
            get => _welcomeTitle;
            set
            {
                if (_welcomeTitle == value) return;
                _welcomeTitle = value;
                OnPropertyChanged();
            }
        }

        public string IntroMessage
        {
            get => _introMessage;
            set
            {
                if (_introMessage == value) return;
                _introMessage = value;
                OnPropertyChanged();
            }
        }

        public string NoteText
        {
            get => _noteText;
            set
            {
                if (_noteText == value) return;
                _noteText = value;
                OnPropertyChanged();
            }
        }

        public string ResultMessage
        {
            get => _resultMessage;
            set
            {
                if (_resultMessage == value) return;
                _resultMessage = value;
                OnPropertyChanged();
            }
        }

        public Color ResultColor
        {
            get => _resultColor;
            set
            {
                if (_resultColor == value) return;
                _resultColor = value;
                OnPropertyChanged();
            }
        }

        public string TipMessage
        {
            get => _tipMessage;
            set
            {
                if (_tipMessage == value) return;
                _tipMessage = value;
                OnPropertyChanged();
            }
        }

        public bool ShowIdeasShortcut
        {
            get => _showIdeasShortcut;
            set
            {
                if (_showIdeasShortcut == value) return;
                _showIdeasShortcut = value;
                OnPropertyChanged();
            }
        }

        public bool ShowHelperTip
        {
            get => _showHelperTip;
            set
            {
                if (_showHelperTip == value) return;
                _showHelperTip = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddQuickNoteCommand { get; }
        public ICommand PreviewNoteCommand { get; }
        public ICommand OpenStacksCommand { get; }
        public ICommand OpenIdeasCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public MainPageViewModel()
        {
            AddQuickNoteCommand = new Command(AddQuickNote);
            PreviewNoteCommand = new Command(async () => await PreviewNoteAsync());
            OpenStacksCommand = new Command(async () => await Shell.Current.GoToAsync("stacks"));
            OpenIdeasCommand = new Command(async () => await Shell.Current.GoToAsync("noteideas"));
            OpenSettingsCommand = new Command(async () => await Shell.Current.GoToAsync("settings"));
            LoadSettings();
        }

        private void AddQuickNote()
        {
            var trimmedNote = (NoteText ?? string.Empty).Trim();

            if (!string.IsNullOrWhiteSpace(trimmedNote))
            {
                NoteText = trimmedNote;
                ResultColor = Color.FromArgb("#2E7D32");
                ResultMessage = $"Ready to save: {trimmedNote}";
            }
            else
            {
                ResultColor = Color.FromArgb("#C62828");
                ResultMessage = "Please enter a note before continuing.";
            }
        }

        public void LoadSettings()
        {
            ShowIdeasShortcut = Services.AppSettings.ShowIdeasShortcut;
            ShowHelperTip = Services.AppSettings.ShowHelperTip;

            IntroMessage = ShowIdeasShortcut
                ? "Create a quick reminder, preview it, open your saved cards, or jump into note ideas."
                : "Create a quick reminder, preview it, or jump into your saved stack cards.";

            TipMessage = ShowHelperTip
                ? "Use clear card titles so search works better once your stack grows."
                : string.Empty;
        }

        private async Task PreviewNoteAsync()
        {
            var trimmedNote = (NoteText ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(trimmedNote))
            {
                ResultColor = Color.FromArgb("#C62828");
                ResultMessage = "Add a note first so the preview page has something to show.";
                return;
            }

            ResultColor = Color.FromArgb("#2E7D32");
            ResultMessage = $"Opening preview for: {trimmedNote}";
            await Shell.Current.GoToAsync($"preview?noteText={Uri.EscapeDataString(trimmedNote)}");
        }
    }
}
