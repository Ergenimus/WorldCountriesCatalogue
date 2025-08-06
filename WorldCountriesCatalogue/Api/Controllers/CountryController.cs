using Microsoft.AspNetCore.Mvc;
using WorldCountriesCatalogue.Model;
using WorldCountriesCatalogue.Model.Exceptions;

namespace WorldCountriesCatalogue.Api.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryScenarios _countryScenarios;

        public CountryController(CountryScenarios countryScenarios)
        {
            _countryScenarios = countryScenarios;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _countryScenarios.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{IsoAlpha2}")]
        public async Task<IActionResult> GetCountryByIsoCode(string isoAlpha2)
        {
            try
            {
                var country = await _countryScenarios.GetCountryByIsoCodeAsync(isoAlpha2);
                return Ok(country);
            }
            catch (InvalidCountryCodeException ex) 
            {
                // 400 Bad Request - невалидный код
                return BadRequest(new { error = ex.Message });
            }
            catch (CountryNotFoundException ex)
            {
                // 404 Not Found - валидный код, но страна не найдена
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> StoreCountry([FromBody] Country country)
        {
            try
            {
                var createdCountry = await _countryScenarios.StoreCountryAsync(country);
                return CreatedAtAction(nameof(GetCountryByIsoCode), new { isoAlpha2 = createdCountry.IsoAlpha2 }, createdCountry);
            }
            catch (DuplicateCountryException ex) 
            {   
                // 409 - конфликт с уже существующими параметрами в записях в таблице
                return Conflict(new { error = ex.Message });
            }
            catch (InvalidCountryCodeException ex)
            {
                // 400 Bad Request - невалидный код
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{IsoAlpha2}")]
        public async Task<IActionResult> UpdateCountry(string isoAlpha2, [FromBody] Country country)
        {
            try
            {
                var updatedCountry = await _countryScenarios.UpdateCountryAsync(isoAlpha2, country);
                return Ok(updatedCountry);
            }
            catch (DuplicateCountryException ex)
            {
                // 409 - конфликт с уже существующими параметрами в записях в таблице
                return Conflict(new { error = ex.Message });
            }
            catch (InvalidCountryCodeException ex)
            {
                // 400 Bad Request - невалидный код
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidCountryException ex)
            {
                // 400 Bad Request
                return BadRequest(new { error = ex.Message });
            }
            catch (CountryNotFoundException ex)
            {
                // 404 Not Found - валидный код, но страна не найдена
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{IsoAlpha2}")]
        public async Task<IActionResult> DeleteCountry(string isoAlpha2)
        {
            try
            {
                await _countryScenarios.DeleteCountryAsync(isoAlpha2);
                return NoContent();
            }
            catch (InvalidCountryCodeException ex)
            {
                // 400 Bad Request - невалидный код
                return BadRequest(new { error = ex.Message });
            }
            catch (CountryNotFoundException ex)
            {
                // 404 Not Found - валидный код, но страна не найдена
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
