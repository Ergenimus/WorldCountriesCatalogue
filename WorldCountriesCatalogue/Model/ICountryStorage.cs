using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldCountriesCatalogue.Model
{
    public interface ICountryStorage
    {
        Task<List<Country>> GetAllCountries();

        Task<Country> GetCountryByIsoCode(string isoAlpha2);

        Task<Country> StoreCountry(Country country);

        Task<Country> UpdateCountry(string isoAlpha2, Country country);

        Task DeleteCountry(string isoAlpha2);

        Task<bool> ValidateCountry(string isoAlpha2);
    }
}
