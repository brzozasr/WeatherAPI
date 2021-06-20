using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.DbContext;
using WeatherAPI.Models;

namespace WeatherAPI.Repositories
{
    public class CityRepository : IRepository<City>
    {
        private readonly WeatherDbContext _context;
        private readonly DbSet<City> _dbSet;
            
        public CityRepository(WeatherDbContext weatherDbContext)
        {
            _context = weatherDbContext;
            _dbSet = _context.Set<City>();
        }

        public async Task<IEnumerable<City>> GetByName(string name)
        {
            name = name.ToLower();
            return await _dbSet.Where(x => x.Name.ToLower() == name).ToListAsync();
        }

        public async Task<IEnumerable<City>> GetByPartialMatch(string partialName)
        {
            int quantityReturnedCities = 50;
            partialName = partialName.ToLower();

            var startsWith = await _dbSet.Where(x => x.Name.ToLower().StartsWith(partialName))
                .Take(quantityReturnedCities).ToListAsync();

             if (!startsWith.Any() || startsWith.Count < quantityReturnedCities)
             {
                 int citiesCounter = quantityReturnedCities - startsWith.Count;
                 var contains = await _dbSet.Where(x => x.Name.ToLower().Contains(partialName))
                     .Take(citiesCounter).ToListAsync();
                 startsWith.AddRange(contains);
             }

             return startsWith;
        }

        public async Task<City> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}