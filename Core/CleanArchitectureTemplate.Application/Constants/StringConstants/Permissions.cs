namespace CleanArchitectureTemplate.Application.Constants.StringConstants;

/// <summary>
/// Represents the permission claims constants.
/// </summary>
public static class Permissions
{
    #region User Permissions

    public const string UserCreate = "User.Create";
    public const string UserRead = "User.Read";
    public const string UserUpdate = "User.Update";
    public const string UserDelete = "User.Delete";

    #endregion User Permissions

    #region Product Permissions

    public const string ProductCreate = "Product.Create";
    public const string ProductRead = "Product.Read";
    public const string ProductUpdate = "Product.Update";
    public const string ProductDelete = "Product.Delete";

    #endregion Product Permissions

    #region Order Permissions

    public const string OrderCreate = "Order.Create";
    public const string OrderRead = "Order.Read";
    public const string OrderUpdate = "Order.Update";
    public const string OrderDelete = "Order.Delete";

    #endregion Order Permissions

    #region File Permissions

    public const string FileUpload = "File.Upload";
    public const string FileRead = "File.Read";
    public const string FileDelete = "File.Delete";

    #endregion File Permissions

    #region Basket Permissions

    public const string BasketCreate = "Basket.Create";
    public const string BasketRead = "Basket.Read";
    public const string BasketUpdate = "Basket.Update";
    public const string BasketDelete = "Basket.Delete";

    #endregion Basket Permissions

    #region Basket Item Permissions

    public const string BasketItemCreate = "BasketItem.Create";
    public const string BasketItemRead = "BasketItem.Read";
    public const string BasketItemUpdate = "BasketItem.Update";
    public const string BasketItemDelete = "BasketItem.Delete";

    #endregion Basket Item Permissions
}