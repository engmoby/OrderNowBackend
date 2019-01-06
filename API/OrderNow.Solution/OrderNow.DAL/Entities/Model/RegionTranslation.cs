using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class RegionTranslation:Entity
    {
        public long RegionTranslationId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        [ForeignKey("Region")]
        public long RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}
