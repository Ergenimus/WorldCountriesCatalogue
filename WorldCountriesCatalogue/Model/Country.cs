namespace WorldCountriesCatalogue.Model
{
    public class Country
    {
        // Короткое наименование страны
        public string ShortName { get; set; } = string.Empty;

        // Полное наименование страны
        public string FullName { get; set; } = string.Empty;

        // ISO Alpha-2 код страны
        public string IsoAlpha2 { get; set; } = string.Empty;

        // Конструктор по умолчанию
        public Country() { }

        public Country(string shortName, string fullName, string isoAlpha2)
        {
            ShortName = shortName;
            FullName = fullName;
            IsoAlpha2 = isoAlpha2;
        }

    }
}
