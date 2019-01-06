using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class CityTranslation:Entity
    {
        public long CityTranslationId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        [ForeignKey("City")]
        public long CityId { get; set; }
        public virtual City City { get; set; }
    }
}
