using System.Collections.ObjectModel;

namespace StackyNotes.Models
{
    public class NoteStack
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ObservableCollection<NoteCard> Cards { get; set; }
            = new ObservableCollection<NoteCard>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}