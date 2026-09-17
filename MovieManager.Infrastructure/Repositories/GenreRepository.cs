using Microsoft.EntityFrameworkCore;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Infrastructure.Context;

namespace MJDVerse.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        //Private Fields
        private readonly AppDbContext _context;


        //Constructor
        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }






        //Action methods
        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }




        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }




        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
        }




        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}