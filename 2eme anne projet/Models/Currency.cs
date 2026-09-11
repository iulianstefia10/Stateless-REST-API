using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        public int Id { get; set; }

        private string? currencyName;

        [Required]
        public string? CurrencyName
        {
            get { return currencyName; }
            set { currencyName = value; }
        }

        public double Rate { get; set; }

        public Currency() { }

        public Currency(int id, string? currencyName, double rate)
        {
            Id = id;
            CurrencyName = currencyName;
            Rate = rate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CurrencyName, Rate);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Currency other = (Currency)obj;
            return Id == other.Id && CurrencyName == other.CurrencyName && Rate == other.Rate;
        }
    }
}