using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}