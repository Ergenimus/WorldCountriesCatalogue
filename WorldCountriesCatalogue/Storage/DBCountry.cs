using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WorldCountriesCatalogue.Storage
{
    [Index(nameof(IsoAlpha2), IsUnique = true)]
    public class DBCountry
    {
        // Идентификатор
        [Key]
        public int Id { get; set; }

        // Короткое наименование страны
        [Required]
        [MaxLength(15)]
        public string ShortName { get; set; } = string.Empty;

        // Полное наименование страны
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = string.Empty;

        // ISO Alpha-2 код страны
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string IsoAlpha2 { get; set; } = string.Empty;

        // Конструктор по умолчанию
        public DBCountry() { }

        public DBCountry(int id, string shortName, string fullName, string isoAlpha2)
        {
            Id = id;
            ShortName = shortName;
            FullName = fullName;
            IsoAlpha2 = isoAlpha2;
        }

    }
}
