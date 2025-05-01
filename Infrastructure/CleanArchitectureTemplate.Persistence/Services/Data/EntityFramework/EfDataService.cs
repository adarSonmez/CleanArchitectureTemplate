using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Constants.StringConstants;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Files;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.ValueObjects;
using CleanArchitectureTemplate.Persistence.Contexts;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArchitectureTemplate.Persistence.Services.Data.EntityFramework;

/// <summary>
/// Represents the data service for seeding and updating the database based on Entity Framework.
/// </summary>
public class EfDataService : IDataService
{
    private readonly EfDbContext _context;
    public readonly UserManager<AppUser> _userManager;
    public readonly RoleManager<AppRole> _roleManager;

    public EfDataService(EfDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <inheritdoc/>
    public async Task SeedAsync()
    {
        if (_context.Roles.Any())
            return;

        #region Seed Roles

        var roles = new[] { UserRoles.Admin, UserRoles.Customer, UserRoles.Store };

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new AppRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
        }

        #endregion Seed Roles

        #region Seed Role Claims

        var adminRole = await _roleManager.FindByNameAsync(UserRoles.Admin)!;
        var customerRole = await _roleManager.FindByNameAsync(UserRoles.Customer)!;
        var storeRole = await _roleManager.FindByNameAsync(UserRoles.Store)!;

        var adminClaims = new[]
        {
            new Claim(ClaimNames.Permission, Permissions.UserCreate),
            new Claim(ClaimNames.Permission, Permissions.UserRead),
            new Claim(ClaimNames.Permission, Permissions.UserUpdate),
            new Claim(ClaimNames.Permission, Permissions.UserDelete),
            new Claim(ClaimNames.Permission, Permissions.ProductCreate),
            new Claim(ClaimNames.Permission, Permissions.ProductRead),
            new Claim(ClaimNames.Permission, Permissions.ProductUpdate),
            new Claim(ClaimNames.Permission, Permissions.ProductDelete),
            new Claim(ClaimNames.Permission, Permissions.OrderCreate),
            new Claim(ClaimNames.Permission, Permissions.OrderRead),
            new Claim(ClaimNames.Permission, Permissions.OrderUpdate),
            new Claim(ClaimNames.Permission, Permissions.OrderDelete),
            new Claim(ClaimNames.Permission, Permissions.FileUpload),
            new Claim(ClaimNames.Permission, Permissions.FileRead),
            new Claim(ClaimNames.Permission, Permissions.FileDelete),
            new Claim(ClaimNames.Permission, Permissions.BasketCreate),
            new Claim(ClaimNames.Permission, Permissions.BasketRead),
            new Claim(ClaimNames.Permission, Permissions.BasketUpdate),
            new Claim(ClaimNames.Permission, Permissions.BasketDelete),
            new Claim(ClaimNames.Permission, Permissions.BasketItemCreate),
            new Claim(ClaimNames.Permission, Permissions.BasketItemRead),
            new Claim(ClaimNames.Permission, Permissions.BasketItemUpdate),
            new Claim(ClaimNames.Permission, Permissions.BasketItemDelete),
        };

        var customerClaims = new[]
        {
            new Claim(ClaimNames.Permission, Permissions.UserRead),
            new Claim(ClaimNames.Permission, Permissions.ProductRead),
            new Claim(ClaimNames.Permission, Permissions.FileRead),
            new Claim(ClaimNames.Permission, Permissions.OrderCreate),
            new Claim(ClaimNames.Permission, Permissions.OrderRead),
            new Claim(ClaimNames.Permission, Permissions.BasketCreate),
            new Claim(ClaimNames.Permission, Permissions.BasketRead),
            new Claim(ClaimNames.Permission, Permissions.BasketUpdate),
            new Claim(ClaimNames.Permission, Permissions.BasketDelete),
            new Claim(ClaimNames.Permission, Permissions.BasketItemCreate),
            new Claim(ClaimNames.Permission, Permissions.BasketItemRead),
            new Claim(ClaimNames.Permission, Permissions.BasketItemUpdate),
            new Claim(ClaimNames.Permission, Permissions.BasketItemDelete),
        };

        var storeClaims = new[]
        {
            new Claim(ClaimNames.Permission, Permissions.UserRead),
            new Claim(ClaimNames.Permission, Permissions.ProductCreate),
            new Claim(ClaimNames.Permission, Permissions.ProductRead),
            new Claim(ClaimNames.Permission, Permissions.ProductUpdate),
            new Claim(ClaimNames.Permission, Permissions.ProductDelete),
            new Claim(ClaimNames.Permission, Permissions.OrderRead),
            new Claim(ClaimNames.Permission, Permissions.OrderDelete),
            new Claim(ClaimNames.Permission, Permissions.FileUpload),
            new Claim(ClaimNames.Permission, Permissions.FileRead),
            new Claim(ClaimNames.Permission, Permissions.FileDelete),
        };

        var currentAdminClaims = await _roleManager.GetClaimsAsync(adminRole!);
        foreach (var item in adminClaims)
        {
            if (!currentAdminClaims.Any(c => c.Type == item.Type && c.Value == item.Value))
                await _roleManager.AddClaimAsync(adminRole!, item);
        }

        var currentCustomerClaims = await _roleManager.GetClaimsAsync(customerRole!);
        foreach (var item in customerClaims)
        {
            if (!currentCustomerClaims.Any(c => c.Type == item.Type && c.Value == item.Value))
                await _roleManager.AddClaimAsync(customerRole!, item);
        }

        var currentStoreClaims = await _roleManager.GetClaimsAsync(storeRole!);
        foreach (var item in storeClaims)
        {
            if (!currentStoreClaims.Any(c => c.Type == item.Type && c.Value == item.Value))
                await _roleManager.AddClaimAsync(storeRole!, item);
        }

        #endregion Seed Role Claims

        #region Seed Users

        var adminAppUser = new AppUser
        {
            FullName = "Admin User",
            UserName = "admin",
            Email = "admin@localhost.com",
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(adminAppUser, "Admin@123");
        await _userManager.AddToRoleAsync(adminAppUser, "Admin");

        var storeAppUser = new AppUser
        {
            FullName = "Store User",
            UserName = "store",
            Email = "store@localhost.com",
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(storeAppUser, "Store@123");
        await _userManager.AddToRoleAsync(storeAppUser, "Store");

        var customerAppUser = new AppUser
        {
            FullName = "Customer User",
            UserName = "customer",
            Email = "customer@localhost.com",
            EmailConfirmed = false
        };

        await _userManager.CreateAsync(customerAppUser, "Customer@123");
        await _userManager.AddToRoleAsync(customerAppUser, "Customer");

        #endregion Seed Users

        #region Seed User Claims

        var customerUserClaims = new[]
        {
            new Claim(ClaimNames.SubscriptionLevel, "Free"),
            new Claim(ClaimNames.LoyaltyLevel, "Gold")
        };

        var storeUserClaims = new[]
        {
            new Claim(ClaimNames.TrustLevel, "High"),
            new Claim(ClaimNames.SubscriptionLevel, "Premium")
        };

        var currentCustomerUserClaims = await _userManager.GetClaimsAsync(customerAppUser);
        foreach (var item in customerUserClaims)
        {
            if (!currentCustomerUserClaims.Any(c => c.Type == item.Type && c.Value == item.Value))
                await _userManager.AddClaimAsync(customerAppUser, item);
        }

        var currentStoreUserClaims = await _userManager.GetClaimsAsync(storeAppUser);
        foreach (var item in storeUserClaims)
        {
            if (!currentStoreUserClaims.Any(c => c.Type == item.Type && c.Value == item.Value))
                await _userManager.AddClaimAsync(storeAppUser, item);
        }

        #endregion Seed User Claims

        #region Seed Customers

        var customerMember = new Customer
        {
            Id = customerAppUser.Id,
            DateOfBirth = new DateOnly(1996, 1, 1),
            Gender = Gender.Male,
        };

        await _context.Customers.AddAsync(customerMember);

        #endregion Seed Customers

        #region Seed Stores

        var storeMember = new Store
        {
            Id = storeAppUser.Id,
            Website = "www.store.com",
            Description = "Store Description",
        };

        await _context.Stores.AddAsync(storeMember);

        #endregion Seed Stores

        #region Seed Categories

        var electronicsCategory = new Category { Name = "Electronics", Description = "Electronic devices and accessories" };
        var clothingCategory = new Category { Name = "Clothing", Description = "Clothing and accessories" };
        var hatsCategory = new Category { Name = "Hats", Description = "Hats and caps", ParentCategoryId = clothingCategory.Id };
        var booksCategory = new Category { Name = "Books", Description = "Books and magazines" };

        await _context.Categories.AddRangeAsync(electronicsCategory, clothingCategory, booksCategory);

        #endregion Seed Categories

        #region Seed Products

        var laptopProduct = new Product
        {
            Name = "Laptop",
            Description = "A high-performance laptop",
            Categories = [electronicsCategory],
            Stock = 100,
            StandardPrice = new Money(1000, Currency.Usd),
            DiscountRate = 0.1m,
            StoreId = storeMember.Id,
        };

        var phoneProduct = new Product
        {
            Name = "Phone",
            Description = "Latest model phone",
            Categories = [electronicsCategory],
            Stock = 200,
            StandardPrice = new Money(0.01m, Currency.Btc),
            DiscountRate = 0.2m,
            StoreId = storeMember.Id,
        };

        var shirtProduct = new Product
        {
            Name = "Shirt",
            Description = "A casual shirt",
            Categories = [clothingCategory],
            Stock = 1000,
            StandardPrice = new Money(50, Currency.Try),
            DiscountRate = 0.1m,
            StoreId = storeMember.Id,
        };

        var bookProduct = new Product
        {
            Name = "Book",
            Description = "A best-selling book",
            Categories = [booksCategory],
            Stock = 2000,
            StandardPrice = new Money(20, Currency.Eur),
            DiscountRate = 0.5m,
            StoreId = storeMember.Id,
        };

        var hatProduct = new Product
        {
            Name = "Hat",
            Description = "A stylish hat",
            Categories = [hatsCategory, clothingCategory],
            Stock = 500,
            StandardPrice = new Money(10, Currency.Try),
            DiscountRate = 0.1m,
            StoreId = storeMember.Id,
        };

        await _context.Products.AddRangeAsync(laptopProduct, phoneProduct, shirtProduct, bookProduct, hatProduct);

        #endregion Seed Products

        #region Seed Baskets

        var basket1 = new Basket { CustomerId = customerMember.Id, Ordered = true };

        var basket2 = new Basket { CustomerId = customerMember.Id, Ordered = true };

        var basket3 = new Basket { CustomerId = customerMember.Id, Ordered = true };

        var basket4 = new Basket { CustomerId = customerMember.Id };

        await _context.Baskets.AddRangeAsync(basket1, basket2, basket3, basket4);

        #endregion Seed Baskets

        #region Seed Basket Items

        var laptopBasketItem = new BasketItem
        {
            Product = laptopProduct,
            Quantity = 2,
            Basket = basket1,
        };

        var phoneBasketItem = new BasketItem
        {
            Product = phoneProduct,
            Quantity = 1,
            Basket = basket1,
        };

        var shirtBasketItem = new BasketItem
        {
            Product = shirtProduct,
            Quantity = 3,
            Basket = basket2,
        };

        var bookBasketItem1 = new BasketItem
        {
            Product = bookProduct,
            Quantity = 4,
            Basket = basket3,
        };

        var bookBasketItem2 = new BasketItem
        {
            Product = bookProduct,
            Quantity = 2,
            Basket = basket4,
        };

        var bookBasketItem3 = new BasketItem
        {
            Product = bookProduct,
            Quantity = 10,
            Basket = basket1,
        };

        await _context.BasketItems.AddRangeAsync(laptopBasketItem, phoneBasketItem, shirtBasketItem, bookBasketItem1, bookBasketItem2, bookBasketItem3);

        #endregion Seed Basket Items

        #region Seed Orders

        var shippedOrder = new Order
        {
            Basket = basket1,
            ShippingAddress = new Address("12345", "Istanbul", Country.Turkey),
            Status = OrderStatus.Shipped,
        };

        var deliveredOrder = new Order
        {
            Basket = basket2,
            ShippingAddress = new Address("54321", "Izmir", Country.Turkey),
            Status = OrderStatus.Delivered,
        };

        var pendingOrder = new Order
        {
            Basket = basket3,
            ShippingAddress = new Address("67890", "Ankara", Country.Turkey),
            Status = OrderStatus.Pending,
        };

        await _context.Orders.AddRangeAsync(shippedOrder, deliveredOrder, pendingOrder);

        #endregion Seed Orders

        #region Seed Invoices

        var shippedInvoice = new Invoice
        {
            Order = shippedOrder,
            BillingAddress = new Address("12345", "Istanbul", Country.Turkey),
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = InvoiceStatus.Paid,
            PaymentMethod = PaymentMethod.CreditCard,
            TransactionId = "0001",
            Notes = "Paid by credit card",
            PaidAt = DateTime.UtcNow.AddDays(-1),
            IssuedAt = DateTime.UtcNow.AddDays(-2),
        };

        var deliveredInvoice = new Invoice
        {
            Order = deliveredOrder,
            BillingAddress = new Address("54321", "Izmir", Country.Turkey),
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = InvoiceStatus.Paid,
            PaymentMethod = PaymentMethod.CreditCard,
            TransactionId = "0002",
            Notes = "Paid by credit card",
            PaidAt = DateTime.UtcNow.AddDays(-1),
            IssuedAt = DateTime.UtcNow.AddDays(-2),
        };

        var pendingInvoice = new Invoice
        {
            Order = pendingOrder,
            BillingAddress = new Address("67890", "Ankara", Country.Turkey),
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = InvoiceStatus.Pending,
            IssuedAt = DateTime.UtcNow.AddDays(0),
        };

        await _context.Invoices.AddRangeAsync(shippedInvoice, deliveredInvoice, pendingInvoice);

        #endregion Seed Invoices

        #region Seed File Details

        var jpg1 = new FileDetails
        {
            Name = "jpg1.jpg",
            Extension = FileExtension.Jpg,
            Size = 1024,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var jpg2 = new FileDetails
        {
            Name = "jpg2.jpg",
            Extension = FileExtension.Jpg,
            Size = 2048,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var jpg3 = new FileDetails
        {
            Name = "jpg3.jpg",
            Extension = FileExtension.Jpg,
            Size = 4096,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var jpg4 = new FileDetails
        {
            Name = "jpg4.jpg",
            Extension = FileExtension.Jpg,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png1 = new FileDetails
        {
            Name = "png1.png",
            Extension = FileExtension.Png,
            Size = 1024,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png2 = new FileDetails
        {
            Name = "png2.png",
            Extension = FileExtension.Png,
            Size = 2048,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png3 = new FileDetails
        {
            Name = "png3.png",
            Extension = FileExtension.Png,
            Size = 4096,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png4 = new FileDetails
        {
            Name = "png4.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png5 = new FileDetails
        {
            Name = "png5.png",
            Extension = FileExtension.Png,
            Size = 1024,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png6 = new FileDetails
        {
            Name = "png6.png",
            Extension = FileExtension.Png,
            Size = 2048,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png7 = new FileDetails
        {
            Name = "png7.png",
            Extension = FileExtension.Png,
            Size = 4096,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png8 = new FileDetails
        {
            Name = "png8.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png9 = new FileDetails
        {
            Name = "png9.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png10 = new FileDetails
        {
            Name = "png10.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png11 = new FileDetails
        {
            Name = "png11.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png12 = new FileDetails
        {
            Name = "png12.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png13 = new FileDetails
        {
            Name = "png13.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png14 = new FileDetails
        {
            Name = "png14.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        var png15 = new FileDetails
        {
            Name = "png15.png",
            Extension = FileExtension.Png,
            Size = 8192,
            Folder = "images",
            Storage = StorageType.Local,
        };

        await _context.FileDetails.AddRangeAsync(
            jpg1, jpg2, jpg3, jpg4,
            png1, png2, png3, png4,
            png5, png6, png7, png8,
            png9, png10, png11, png12,
            png13, png14, png15
        );

        #endregion Seed File Details

        #region Seed Category Image Files

        var categoryImageFile1 = new CategoryImageFile
        {
            FileDetails = jpg1,
            Category = electronicsCategory,
        };

        var categoryImageFile2 = new CategoryImageFile
        {
            FileDetails = jpg2,
            Category = clothingCategory,
        };

        var categoryImageFile3 = new CategoryImageFile
        {
            FileDetails = jpg3,
            Category = booksCategory,
        };

        var categoryImageFile4 = new CategoryImageFile
        {
            FileDetails = jpg4,
            Category = hatsCategory,
        };

        await _context.CategoryImageFiles.AddRangeAsync(categoryImageFile1, categoryImageFile2, categoryImageFile3, categoryImageFile4);

        #endregion Seed Category Image Files

        #region Seed Product Image Files

        var productImageFile1 = new ProductImageFile
        {
            FileDetails = png1,
            Product = laptopProduct,
            IsPrimary = true,
        };

        var productImageFile2 = new ProductImageFile
        {
            FileDetails = png2,
            Product = laptopProduct,
            IsPrimary = false,
        };

        var productImageFile3 = new ProductImageFile
        {
            FileDetails = png3,
            Product = laptopProduct,
            IsPrimary = false,
        };

        var productImageFile4 = new ProductImageFile
        {
            FileDetails = png4,
            Product = phoneProduct,
            IsPrimary = true,
        };

        var productImageFile5 = new ProductImageFile
        {
            FileDetails = png5,
            Product = phoneProduct,
            IsPrimary = false,
        };

        var productImageFile6 = new ProductImageFile
        {
            FileDetails = png6,
            Product = phoneProduct,
            IsPrimary = false,
        };

        var productImageFile7 = new ProductImageFile
        {
            FileDetails = png7,
            Product = shirtProduct,
            IsPrimary = true,
        };

        var productImageFile8 = new ProductImageFile
        {
            FileDetails = png8,
            Product = shirtProduct,
            IsPrimary = false,
        };

        var productImageFile9 = new ProductImageFile
        {
            FileDetails = png9,
            Product = shirtProduct,
            IsPrimary = false,
        };

        var productImageFile10 = new ProductImageFile
        {
            FileDetails = png10,
            Product = bookProduct,
            IsPrimary = true,
        };

        var productImageFile11 = new ProductImageFile
        {
            FileDetails = png11,
            Product = bookProduct,
            IsPrimary = false,
        };

        var productImageFile12 = new ProductImageFile
        {
            FileDetails = png12,
            Product = bookProduct,
            IsPrimary = false,
        };

        var productImageFile13 = new ProductImageFile
        {
            FileDetails = png13,
            Product = hatProduct,
            IsPrimary = true,
        };

        var productImageFile14 = new ProductImageFile
        {
            FileDetails = png14,
            Product = hatProduct,
            IsPrimary = false,
        };

        var productImageFile15 = new ProductImageFile
        {
            FileDetails = png15,
            Product = hatProduct,
            IsPrimary = false,
        };

        await _context.ProductImageFiles.AddRangeAsync(
            productImageFile1, productImageFile2, productImageFile3,
            productImageFile4, productImageFile5, productImageFile6,
            productImageFile7, productImageFile8, productImageFile9,
            productImageFile10, productImageFile11, productImageFile12,
            productImageFile13, productImageFile14, productImageFile15
        );

        #endregion Seed Product Image Files

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task MigrateAsync()
    {
        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
            await _context.Database.MigrateAsync();
    }
}