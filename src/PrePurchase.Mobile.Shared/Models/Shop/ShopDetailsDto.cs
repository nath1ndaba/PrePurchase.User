namespace PrePurchase.Mobile.Shared.Models.Shop;

// Shop Details Model
public class ShopDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double ServiceRadiusKm { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public string? LogoUrl { get; set; }
    public string? BannerUrl { get; set; }
    public FullAddress FullAddress { get; set; }
    public Location Location { get; set; }
    public BusinessHours BusinessHours { get; set; }
    public ShopOwner? Owner { get; set; }
    public double? DistanceKm { get; set; } // Added - distance from user location
}

// Shop Owner Model
public class ShopOwner
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

// Full Address Model
public class FullAddress
{
    public string Street { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}

// Location Model
public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

// Business Hours Model
public class BusinessHours
{
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public List<int> ClosedDays { get; set; } // 0 = Sunday, 6 = Saturday
}