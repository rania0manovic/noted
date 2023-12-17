using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class NotesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NotesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Note> AddAsync(NoteAddVM noteVM, CancellationToken cancellationToken = default)
        {
            var note = new Note()
            {
                Name = noteVM.Name,
                RepositoryId = noteVM.RepositoryId,
                Title = "Note Title",
                Data = "Write your note here!"
            };
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return note;
        }

        public async Task<List<NoteGetVM>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var notes = await _dbContext.Notes.Where(n => n.RepositoryId == id).ToListAsync(cancellationToken);
            var list = new List<NoteGetVM>();
            foreach (var note in notes)
            {
                list.Add(new NoteGetVM()
                {
                    Id = note.Id,
                    Name = note.Name,
                });
            }
            return list;
        }

        public async Task<Note?> GetFullAsync(int id, CancellationToken cancellationToken = default)
        {
            var note = await _dbContext.Notes.FindAsync(new object?[] { id });
            return note;
        }

        public async Task<Note?> EditAsync(NoteEditVM noteVM, CancellationToken cancellationToken = default)
        {

            var note = await _dbContext.Notes.FindAsync(new object?[] { noteVM.Id }, cancellationToken: cancellationToken);
            if (note != null)
            {
                note.Title = noteVM.Title;
                note.Data = noteVM.Data;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            return note;

        }
    }
}
