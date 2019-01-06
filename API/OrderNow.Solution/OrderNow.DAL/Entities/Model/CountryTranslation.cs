using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class CountryTranslation:Entity
    {
        public long CountryTranslationId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        [ForeignKey("Country")]
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
