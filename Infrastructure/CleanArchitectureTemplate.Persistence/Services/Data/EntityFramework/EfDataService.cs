using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Constants.StringConstants;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
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

        _context.Customers.Add(customerMember);

        #endregion Seed Customers

        #region Seed Stores

        var storeMember = new Store
        {
            Id = storeAppUser.Id,
            Website = "www.store.com",
            Description = "Store Description",
        };

        _context.Stores.Add(storeMember);

        #endregion Seed Stores

        #region Seed Categories

        var electronicsCategory = new Category { Name = "Electronics", Description = "Electronic devices and accessories" };
        var clothingCategory = new Category { Name = "Clothing", Description = "Clothing and accessories" };
        var hatsCategory = new Category { Name = "Hats", Description = "Hats and caps", ParentCategoryId = clothingCategory.Id };
        var booksCategory = new Category { Name = "Books", Description = "Books and magazines" };

        _context.Categories.AddRange(electronicsCategory, clothingCategory, booksCategory);

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

        _context.Products.AddRange(laptopProduct, phoneProduct, shirtProduct, bookProduct, hatProduct);

        #endregion Seed Products

        #region Seed Baskets

        var basket1 = new Basket { CustomerId = customerMember.Id, Ordered = true };

        var basket2 = new Basket { CustomerId = customerMember.Id, Ordered = true };

        var basket3 = new Basket { CustomerId = customerMember.Id, Ordered = true };

        var basket4 = new Basket { CustomerId = customerMember.Id };

        _context.Baskets.AddRange(basket1, basket2, basket3, basket4);

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

        _context.BasketItems.AddRange(laptopBasketItem, phoneBasketItem, shirtBasketItem, bookBasketItem1, bookBasketItem2, bookBasketItem3);

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

        _context.Orders.AddRange(shippedOrder, deliveredOrder, pendingOrder);

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

        _context.Invoices.AddRange(shippedInvoice, deliveredInvoice, pendingInvoice);

        #endregion Seed Invoices

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