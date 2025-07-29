using FlashGameCofe.DataBase.Context;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.DataBase.Implementations.EFImplementations
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly FlashGameCofeDbContext _dbContext;

        public NoteRepository(FlashGameCofeDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _dbContext.Notes.ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Note entity)
        {
            _dbContext.Notes.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note entity)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (note != null)
            {
                _dbContext.Entry(note).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Note entity)
        {
            _dbContext.Notes.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
