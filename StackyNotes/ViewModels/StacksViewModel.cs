using System.Collections.ObjectModel;
using System.Windows.Input;
using StackyNotes.Data;
using StackyNotes.Models;
using StackyNotes.Services;

namespace StackyNotes.ViewModels
{
    public class StacksViewModel : BaseViewModel
    {
        private readonly AppDatabase _database;
        private readonly List<NoteCard> _allCards = new();
        private string _pageTitle = "Saved Stack Cards";
        private string _stackDescription = "Use search and sorting to quickly find the right note in your stack.";
        private string _newCardTitle = string.Empty;
        private string _newCardContent = string.Empty;
        private string _newStackName = "My Saved Stack";
        private string _statusMessage = "Loading saved cards...";
        private string _searchText = string.Empty;
        private string _selectedSortOption = "Newest First";
        private string _emptyStateMessage = "No saved cards yet. Add your first one above!";
        private bool _isLoading;
        private string _formHintMessage = "Required field: card title. Details are optional but helpful.";

        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                if (_pageTitle == value) return;
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

        public string StackDescription
        {
            get => _stackDescription;
            set
            {
                if (_stackDescription == value) return;
                _stackDescription = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NoteCard> Cards { get; } = new();

        public ObservableCollection<string> SortOptions { get; } = new()
        {
            "Newest First",
            "Oldest First",
            "A-Z"
        };

        public string CurrentStackName => string.IsNullOrWhiteSpace(NewStackName) ? "My Saved Stack" : NewStackName.Trim();

        public string NewCardTitle
        {
            get => _newCardTitle;
            set
            {
                if (_newCardTitle == value) return;
                _newCardTitle = value;
                OnPropertyChanged();
            }
        }

        public string NewCardContent
        {
            get => _newCardContent;
            set
            {
                if (_newCardContent == value) return;
                _newCardContent = value;
                OnPropertyChanged();
            }
        }

        public string NewStackName
        {
            get => _newStackName;
            set
            {
                if (_newStackName == value) return;
                _newStackName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentStackName));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                OnPropertyChanged();
                ApplySearchAndSort();
            }
        }

        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                if (_selectedSortOption == value || string.IsNullOrWhiteSpace(value)) return;
                _selectedSortOption = value;
                OnPropertyChanged();
                ApplySearchAndSort();
            }
        }

        public string EmptyStateMessage
        {
            get => _emptyStateMessage;
            set
            {
                if (_emptyStateMessage == value) return;
                _emptyStateMessage = value;
                OnPropertyChanged();
            }
        }


        public string FormHintMessage
        {
            get => _formHintMessage;
            set
            {
                if (_formHintMessage == value) return;
                _formHintMessage = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage == value) return;
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCardCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public StacksViewModel(AppDatabase database)
        {
            _database = database;
            AddCardCommand = new Command(async () => await AddCardAsync());
            RefreshCommand = new Command(async () => await LoadCardsAsync());
            ClearSearchCommand = new Command(() => SearchText = string.Empty);

            ReloadPreferences();
            _ = LoadCardsAsync();
        }

        public void ReloadPreferences()
        {
            NewStackName = AppSettings.DefaultStackName;
            SelectedSortOption = AppSettings.SanitizeSortOption(AppSettings.DefaultSortOption);
        }

        public async Task LoadCardsAsync()
        {
            if (IsLoading)
            {
                return;
            }

            try
            {
                IsLoading = true;
                var savedCards = await _database.GetCardsAsync();

                _allCards.Clear();
                _allCards.AddRange(savedCards);
                ApplySearchAndSort();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AddCardAsync()
        {
            var titleText = (NewCardTitle ?? string.Empty).Trim();
            var contentText = (NewCardContent ?? string.Empty).Trim();
            var stackNameText = string.IsNullOrWhiteSpace(NewStackName) ? "My Saved Stack" : NewStackName.Trim();

            if (string.IsNullOrWhiteSpace(titleText))
            {
                FormHintMessage = "Card title is required before you can save this note.";
                StatusMessage = "Enter a card title before saving it.";
                return;
            }

            if (string.IsNullOrWhiteSpace(contentText))
            {
                contentText = "Quick reminder";
            }

            FormHintMessage = "Required field: card title. Details are optional but helpful.";

            var newCard = new NoteCard
            {
                Title = titleText,
                Content = contentText,
                StackName = stackNameText,
                Order = _allCards.Any() ? _allCards.Max(card => card.Order) + 1 : 1,
                CreatedAt = DateTime.Now
            };

            await _database.SaveCardAsync(newCard);
            _allCards.Add(newCard);

            NewCardTitle = string.Empty;
            NewCardContent = string.Empty;
            StatusMessage = $"Saved '{titleText}' to {stackNameText}.";
            ApplySearchAndSort();
        }

        private void ApplySearchAndSort()
        {
            IEnumerable<NoteCard> workingCards = _allCards;
            var query = (SearchText ?? string.Empty).Trim();

            if (!string.IsNullOrWhiteSpace(query))
            {
                workingCards = workingCards.Where(card =>
                    card.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    card.Content.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    card.StackName.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            workingCards = SelectedSortOption switch
            {
                "Oldest First" => workingCards.OrderBy(card => card.CreatedAt).ThenBy(card => card.Title),
                "A-Z" => workingCards.OrderBy(card => card.Title).ThenBy(card => card.CreatedAt),
                _ => workingCards.OrderByDescending(card => card.CreatedAt).ThenBy(card => card.Title)
            };

            Cards.Clear();
            foreach (var card in workingCards)
            {
                Cards.Add(card);
            }

            UpdateMessages();
        }

        private void UpdateMessages()
        {
            if (_allCards.Count == 0)
            {
                EmptyStateMessage = "No saved cards yet. Add your first one above!";
                StatusMessage = "0 cards saved. Create your first StackyNotes card to get started.";
            }
            else if (Cards.Count == 0)
            {
                EmptyStateMessage = "No matches found. Try a different search.";
                StatusMessage = $"No results for '{SearchText}'. Clear search or try another keyword.";
            }
            else
            {
                EmptyStateMessage = string.Empty;
                var filteredText = Cards.Count == _allCards.Count ? "" : $" ({Cards.Count} shown)";
                StatusMessage = $"{_allCards.Count} saved card(s){filteredText} · Sorted: {SelectedSortOption}";
            }

            OnPropertyChanged(nameof(CurrentStackName));
            OnPropertyChanged(nameof(Cards));
        }
    }
}
