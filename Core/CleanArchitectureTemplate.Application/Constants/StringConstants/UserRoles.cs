namespace CleanArchitectureTemplate.Domain.Constants.StringConstants;

/// <summary>
/// Defines the user role constants.
/// </summary>
public static class UserRoles
{
    #region Base Roles

    public const string Admin = nameof(Admin);
    public const string Customer = nameof(Customer);
    public const string Store = nameof(Store);

    #endregion Base Roles

    #region Combined Roles

    public const string CustomerOrAdmin = Customer + "," + Admin;
    public const string StoreOrAdmin = Store + "," + Admin;
    public const string StoreOrCustomer = Store + "," + Customer;

    #endregion Combined Roles
}