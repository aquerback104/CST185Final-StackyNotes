using System.Collections.ObjectModel;
using System.Windows.Input;
using StackyNotes.Services;

namespace StackyNotes.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _pageTitle = "Project Settings";
        private string _pageDescription = "Customize how StackyNotes behaves when you open pages and browse your saved cards.";
        private string _defaultStackName = AppSettings.DefaultStackName;
        private string _selectedSortOption = AppSettings.DefaultSortOption;
        private bool _showIdeasShortcut = AppSettings.ShowIdeasShortcut;
        private bool _showHelperTip = AppSettings.ShowHelperTip;
        private string _statusMessage = "Settings save automatically.";

        public ObservableCollection<string> SortOptions { get; } = new()
        {
            AppSettings.DefaultSortNewest,
            AppSettings.DefaultSortOldest,
            AppSettings.DefaultSortAZ
        };

        public string PageTitle
        {
            get => _pageTitle;
            set { if (_pageTitle == value) return; _pageTitle = value; OnPropertyChanged(); }
        }

        public string PageDescription
        {
            get => _pageDescription;
            set { if (_pageDescription == value) return; _pageDescription = value; OnPropertyChanged(); }
        }

        public string DefaultStackName
        {
            get => _defaultStackName;
            set
            {
                var sanitized = string.IsNullOrWhiteSpace(value) ? "My Saved Stack" : value.Trim();
                if (_defaultStackName == sanitized) return;
                _defaultStackName = sanitized;
                AppSettings.DefaultStackName = sanitized;
                StatusMessage = $"Default stack name saved: {sanitized}";
                OnPropertyChanged();
            }
        }

        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                var sanitized = AppSettings.SanitizeSortOption(value);
                if (_selectedSortOption == sanitized) return;
                _selectedSortOption = sanitized;
                AppSettings.DefaultSortOption = sanitized;
                StatusMessage = $"Default sort saved: {sanitized}";
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
                AppSettings.ShowIdeasShortcut = value;
                StatusMessage = value
                    ? "The Browse Note Ideas shortcut will stay visible on the home screen."
                    : "The Browse Note Ideas shortcut will be hidden from the home screen.";
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
                AppSettings.ShowHelperTip = value;
                StatusMessage = value
                    ? "Home-screen helper tips are turned on."
                    : "Home-screen helper tips are turned off.";
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { if (_statusMessage == value) return; _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand ResetSettingsCommand { get; }

        public SettingsViewModel()
        {
            ResetSettingsCommand = new Command(ResetSettings);
        }

        public void LoadSettings()
        {
            _defaultStackName = AppSettings.DefaultStackName;
            _selectedSortOption = AppSettings.DefaultSortOption;
            _showIdeasShortcut = AppSettings.ShowIdeasShortcut;
            _showHelperTip = AppSettings.ShowHelperTip;
            StatusMessage = "Saved settings loaded.";
            OnPropertyChanged(nameof(DefaultStackName));
            OnPropertyChanged(nameof(SelectedSortOption));
            OnPropertyChanged(nameof(ShowIdeasShortcut));
            OnPropertyChanged(nameof(ShowHelperTip));
        }

        private void ResetSettings()
        {
            AppSettings.Reset();
            LoadSettings();
            StatusMessage = "Settings reset to their default values.";
        }
    }
}
