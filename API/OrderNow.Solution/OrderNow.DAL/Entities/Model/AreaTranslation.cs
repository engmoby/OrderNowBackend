using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class AreaTranslation : Entity
    {
        public long AreaTranslationId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; } 
        public long AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int TenantId { get; set; }
    }
}
