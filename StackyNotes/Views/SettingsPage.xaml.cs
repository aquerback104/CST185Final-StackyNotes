using StackyNotes.ViewModels;

namespace StackyNotes.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel _viewModel;

        public SettingsPage()
        {
            InitializeComponent();
            _viewModel = new SettingsViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadSettings();
        }

        private async void OnOpenAboutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("about");
        }
    }
}
