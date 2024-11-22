using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Domain.Entities
{
    /// <summary>
    /// Represents a customer entity.
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the orders of the customer.
        /// </summary>
        public ICollection<Order> Orders { get; set; } = [];
    }
}