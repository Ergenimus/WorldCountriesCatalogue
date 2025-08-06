namespace WorldCountriesCatalogue.Model.Exceptions
{
    public class DuplicateCountryException : Exception
    {
        public DuplicateCountryException() { }
        public DuplicateCountryException(string message) : base(message) { }
    }
}
