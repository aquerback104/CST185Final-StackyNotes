using SQLite;
using StackyNotes.Models;

namespace StackyNotes.Data
{
    public class AppDatabase
    {
        private SQLiteAsyncConnection? _database;
        private bool _isInitialized;

        private async Task InitAsync()
        {
            if (_isInitialized && _database is not null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "stackynotes.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<NoteCard>();

            var existingCount = await _database.Table<NoteCard>().CountAsync();
            if (existingCount == 0)
            {
                await _database.InsertAllAsync(new[]
                {
                    new NoteCard
                    {
                        Title = "Buy groceries",
                        Content = "Milk, eggs, and bread after class.",
                        StackName = "Daily Stack",
                        Order = 1,
                        CreatedAt = DateTime.Now
                    },
                    new NoteCard
                    {
                        Title = "Study for quiz",
                        Content = "Review MAUI binding notes for 30 minutes.",
                        StackName = "School Stack",
                        Order = 2,
                        CreatedAt = DateTime.Now
                    }
                });
            }

            _isInitialized = true;
        }

        public async Task<List<NoteCard>> GetCardsAsync()
        {
            await InitAsync();
            return await _database!
                .Table<NoteCard>()
                .OrderBy(card => card.Order)
                .ToListAsync();
        }

        public async Task<NoteCard?> GetCardAsync(int id)
        {
            await InitAsync();
            return await _database!
                .Table<NoteCard>()
                .Where(card => card.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveCardAsync(NoteCard card)
        {
            await InitAsync();

            if (card.Id != 0)
            {
                return await _database!.UpdateAsync(card);
            }

            return await _database!.InsertAsync(card);
        }

        public async Task<int> DeleteCardAsync(NoteCard card)
        {
            await InitAsync();
            return await _database!.DeleteAsync(card);
        }

        public async Task<int> DeleteCardAsync(int id)
        {
            await InitAsync();
            var card = await GetCardAsync(id);
            if (card is null)
            {
                return 0;
            }

            return await _database!.DeleteAsync(card);
        }
    }
}
