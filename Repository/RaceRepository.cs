﻿using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i=> i.Address).FirstOrDefaultAsync(i=> i.Id == id); //add include so it doesnt do lazy loading
        }
		public async Task<Race> GetByIdAsyncNoTracking(int id)
		{
			return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id); //add include so it doesnt do lazy loading
		}
		public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
