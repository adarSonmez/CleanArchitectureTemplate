using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}