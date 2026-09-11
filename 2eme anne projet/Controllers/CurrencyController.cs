using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private List<Currency> currencies;

        public CurrenciesController()
        {
            currencies = new List<Currency>();
            currencies.Add(new Currency(1, "Dollar", 1.08));
            currencies.Add(new Currency(2, "Swiss Franc", 1.07));
            currencies.Add(new Currency(3, "Yen", 120));
        }

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>Http response with a list of currencies</returns>
        /// <response code="200">Returns the list of currencies</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Currency>> GetAll()
        {
            return Ok(currencies); // Putem returna doar "currencies", am adăugat Ok() ca Best Practice
        }

        /// <summary>
        /// Get a single currency by ID.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        [HttpGet("{id}", Name = "GetCurrency")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Currency> GetById(int id)
        {
            Currency? currency = currencies.FirstOrDefault((d) => d.Id == id);

            if (currency == null)
            {
                return NotFound();
            }
            return currency;
        }

        /// <summary>
        /// Add a new currency.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Currency> Post([FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            currencies.Add(currency);

            return CreatedAtRoute("GetCurrency", new { id = currency.Id }, currency);
        }

        /// <summary>
        /// Update an existing currency.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id, [FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currency.Id)
            {
                return BadRequest();
            }

            int index = currencies.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }

            currencies[index] = currency;

            return NoContent();
        }

        /// <summary>
        /// Delete a currency by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Currency> Delete(int id)
        {
            Currency? currency = currencies.FirstOrDefault((d) => d.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            currencies.Remove(currency);
            return currency;
        }
    }
}