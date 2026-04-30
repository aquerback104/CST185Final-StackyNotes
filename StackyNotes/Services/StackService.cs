using System.Collections.ObjectModel;
using StackyNotes.Models;

namespace StackyNotes.Services
{
    public class StackService
    {
        private readonly ObservableCollection<NoteStack> _stacks;

        public StackService()
        {
            _stacks = new ObservableCollection<NoteStack>
            {
                new NoteStack
                {
                    Id = 1,
                    Name = "Chores",
                    Cards =
                    {
                        new NoteCard
                        {
                            Id = 1,
                            Title = "Dishes",
                            Content = "Clean before 8pm",
                            Order = 1
                        },
                        new NoteCard
                        {
                            Id = 2,
                            Title = "Laundry",
                            Content = "Start load",
                            Order = 2
                        }
                    }
                },
                new NoteStack
                {
                    Id = 2,
                    Name = "Homework",
                    Cards =
                    {
                        new NoteCard
                        {
                            Id = 3,
                            Title = "Web App Lab",
                            Content = "Finish Express routing",
                            Order = 1
                        }
                    }
                }
            };
        }

        public ObservableCollection<NoteStack> GetStacks()
        {
            return _stacks;
        }
    }
}