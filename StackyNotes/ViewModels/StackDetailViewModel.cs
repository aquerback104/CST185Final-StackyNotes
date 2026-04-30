using System.Collections.ObjectModel;
using StackyNotes.Models;

namespace StackyNotes.ViewModels
{
    public class StackDetailViewModel : BaseViewModel
    {
        private NoteStack _currentStack;

        public NoteStack CurrentStack
        {
            get => _currentStack;
            set
            {
                _currentStack = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Cards));
            }
        }

        public ObservableCollection<NoteCard> Cards
            => CurrentStack.Cards;

        public StackDetailViewModel(NoteStack stack)
        {
            _currentStack = stack;
        }
    }
}