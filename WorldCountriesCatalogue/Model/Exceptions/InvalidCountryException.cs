namespace WorldCountriesCatalogue.Model.Exceptions
{
    public class InvalidCountryException : Exception
    {
        public InvalidCountryException() { }
        public InvalidCountryException(string message) : base(message) { }
    }
}
