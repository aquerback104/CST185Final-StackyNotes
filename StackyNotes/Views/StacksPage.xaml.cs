using StackyNotes.Data;
using StackyNotes.Models;
using StackyNotes.ViewModels;

namespace StackyNotes.Views
{
    public partial class StacksPage : ContentPage
    {
        private readonly StacksViewModel _viewModel;

        public StacksPage()
        {
            InitializeComponent();
            _viewModel = IPlatformApplication.Current?.Services.GetService<StacksViewModel>()
                ?? new StacksViewModel(new AppDatabase());
            BindingContext = _viewModel;
        }

        public StacksPage(StacksViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.ReloadPreferences();
            await _viewModel.LoadCardsAsync();
        }

        private async void OnCardSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is not NoteCard selectedCard)
            {
                return;
            }

            if (sender is CollectionView collectionView)
            {
                collectionView.SelectedItem = null;
            }

            await Shell.Current.GoToAsync($"editnote?id={selectedCard.Id}");
        }
    }
}
