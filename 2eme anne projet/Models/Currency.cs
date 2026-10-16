using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Models
{
    /// <summary>
    /// Represents a currency with its conversion rate.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Gets or sets the unique identifier of the currency.
        /// </summary>
        public int Id { get; set; }

        private string? currencyName;

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        [Required]
        public string? CurrencyName
        {
            get { return currencyName; }
            set { currencyName = value; }
        }

        /// <summary>
        /// Gets or sets the conversion rate relative to Euro.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        public Currency() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class with specified values.
        /// </summary>
        /// <param name="id">The currency ID</param>
        /// <param name="currencyName">The currency name</param>
        /// <param name="rate">The conversion rate</param>
        public Currency(int id, string? currencyName, double rate)
        {
            Id = id;
            CurrencyName = currencyName;
            Rate = rate;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CurrencyName, Rate);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Currency other = (Currency)obj;
            return Id == other.Id && CurrencyName == other.CurrencyName && Rate == other.Rate;
        }
    }
}