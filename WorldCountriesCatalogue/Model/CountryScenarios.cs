using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorldCountriesCatalogue.Model.Exceptions;

namespace WorldCountriesCatalogue.Model
{
    public class CountryScenarios
    {
        private readonly ICountryStorage _storage;
        public CountryScenarios(ICountryStorage storage)
        {
            _storage = storage;
        }

        public Task<List<Country>> GetAllCountriesAsync() => _storage.GetAllCountries();

        public async Task<Country> GetCountryByIsoCodeAsync(string isoAlpha2) {

            // Валидация кода страны
            ValidateIsoCode(isoAlpha2);

            return await _storage.GetCountryByIsoCode(isoAlpha2);
        }

        public async Task<Country> StoreCountryAsync(Country country) {

            // Валидация данных страны
            ValidateCountryData(country);

            // Проверка уникальности кода страны
            if (await _storage.ValidateCountry(country.IsoAlpha2))
            {
                throw new DuplicateCountryException($"Country with ISO code {country.IsoAlpha2} already exists");
            }

            return await _storage.StoreCountry(country);
        }

        public async Task<Country> UpdateCountryAsync(string isoAlpha2, Country updatedCountry) {

            ValidateIsoCode(isoAlpha2);

            if (!await _storage.ValidateCountry(isoAlpha2))
            {
                throw new CountryNotFoundException($"Country with ISO code {isoAlpha2} not found");
            }

            if (updatedCountry.IsoAlpha2 != isoAlpha2)
            {
                throw new InvalidCountryException("Changing ISO country code is not allowed");
            }

            ValidateCountryData(updatedCountry);

            return await _storage.UpdateCountry(isoAlpha2, updatedCountry);
        }

        public async Task DeleteCountryAsync(string isoAlpha2) {

            ValidateIsoCode(isoAlpha2);

            if (!await _storage.ValidateCountry(isoAlpha2))
            {
                throw new CountryNotFoundException($"Country with ISO code {isoAlpha2} not found");
            }

            await _storage.DeleteCountry(isoAlpha2);
        }

        // Валидация кода страны
        private void ValidateIsoCode(string isoCode)
        {
            if (string.IsNullOrWhiteSpace(isoCode))
            {
                throw new InvalidCountryCodeException("ISO code cannot be empty");
            }

            if (isoCode.Length != 2)
            {
                throw new InvalidCountryCodeException("ISO code must be exactly 2 characters");
            }

            if (!Regex.IsMatch(isoCode, "^[a-zA-Z]{2}$"))
            {
                throw new InvalidCountryCodeException("ISO code must contain only letters");
            }
        }

        // Валидация данных страны
        private void ValidateCountryData(Country country)
        {
            if (country == null)
            {
                throw new InvalidCountryException("Country data cannot be null");
            }

            if (string.IsNullOrWhiteSpace(country.ShortName))
            {
                throw new InvalidCountryException("Short name is required");
            }

            if (string.IsNullOrWhiteSpace(country.FullName))
            {
                throw new InvalidCountryException("Full name is required");
            }

            if (string.IsNullOrWhiteSpace(country.IsoAlpha2))
            {
                throw new InvalidCountryException("ISO code is required");
            }

            ValidateIsoCode(country.IsoAlpha2);

            if (country.ShortName.Length > 15)
            {
                throw new InvalidCountryException("Short name cannot exceed 15 characters");
            }

            if (country.FullName.Length > 50)
            {
                throw new InvalidCountryException("Full name cannot exceed 50 characters");
            }
        }
    }
}
