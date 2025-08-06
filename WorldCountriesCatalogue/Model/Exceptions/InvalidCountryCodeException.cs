namespace WorldCountriesCatalogue.Model.Exceptions
{
    public class InvalidCountryCodeException : Exception
    {
        public InvalidCountryCodeException() { }
        public InvalidCountryCodeException(string message) : base(message) { }
    }
}
