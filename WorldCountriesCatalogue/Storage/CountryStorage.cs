using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using WorldCountriesCatalogue.Model;
using WorldCountriesCatalogue.Model.Exceptions;

namespace WorldCountriesCatalogue.Storage
{
    public class CountryStorage : ICountryStorage
    {
        private readonly ApplicationDbContext _context;

        public CountryStorage(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            // Получаем все страны из базы данных
            return await _context.Countries
                .Select(c => new Country
                {
                    ShortName = c.ShortName,
                    FullName = c.FullName,
                    IsoAlpha2 = c.IsoAlpha2
                })
                .ToListAsync();
        }

        public async Task<Country> GetCountryByIsoCode(string isoAlpha2)
        {
            // Ищем страну по IsoAlpha2
            var dbCountry = await _context.Countries
                .FirstOrDefaultAsync(c => c.IsoAlpha2 == isoAlpha2);

            if (dbCountry == null)
                throw new CountryNotFoundException($"Country with ISO code {isoAlpha2} not found");

            return new Country
            {
                ShortName = dbCountry.ShortName,
                FullName = dbCountry.FullName,
                IsoAlpha2 = dbCountry.IsoAlpha2
            };
        }

        public async Task<Country> StoreCountry(Country country)
        {
            // Проверка уникальности ISO кода
            if (await _context.Countries.AnyAsync(c => c.IsoAlpha2 == country.IsoAlpha2))
                throw new DuplicateCountryException($"Country with ISO code {country.IsoAlpha2} already exists");

            var dbCountry = new DBCountry
            {
                ShortName = country.ShortName,
                FullName = country.FullName,
                IsoAlpha2 = country.IsoAlpha2
            };

            _context.Countries.Add(dbCountry);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<Country> UpdateCountry(string isoAlpha2, Country country)
        {
            var dbCountry = await _context.Countries
                .FirstOrDefaultAsync(c => c.IsoAlpha2 == isoAlpha2);

            if (dbCountry == null)
                throw new CountryNotFoundException($"Country with ISO code {isoAlpha2} not found");

            if (dbCountry.ShortName == country.ShortName || dbCountry.FullName == country.FullName)
                throw new DuplicateCountryException($"Country already has these parameters.");

            // Обновляем поля
            dbCountry.ShortName = country.ShortName;
            dbCountry.FullName = country.FullName;


            await _context.SaveChangesAsync();

            return country;
        }

        public async Task DeleteCountry(string isoAlpha2)
        {
            var dbCountry = await _context.Countries
                .FirstOrDefaultAsync(c => c.IsoAlpha2 == isoAlpha2);

            if (dbCountry == null)
                throw new CountryNotFoundException($"Country with ISO code {isoAlpha2} not found");

            _context.Countries.Remove(dbCountry);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateCountry(string isoAlpha2)
        {
            return await _context.Countries.AnyAsync(c => c.IsoAlpha2 == isoAlpha2);
        }
    }
}
