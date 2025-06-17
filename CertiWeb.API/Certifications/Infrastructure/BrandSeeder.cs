using CertiWeb.API.Certifications.Domain.Model.Aggregates;

namespace CertiWeb.API.Certifications.Infrastructure;

/// <summary>
/// Seeder for pre-populating brand data.
/// </summary>
public static class BrandSeeder
{
    /// <summary>
    /// Gets the pre-defined brands.
    /// </summary>
    /// <returns>A collection of pre-defined brands.</returns>
    public static IEnumerable<Brand> GetPredefinedBrands()
    {
        return new List<Brand>
        {
            new Brand("Toyota") { Id = 1 },
            new Brand("Nissan") { Id = 2 },
            new Brand("Hyundai") { Id = 3 },
            new Brand("Kia") { Id = 4 },
            new Brand("Chevrolet") { Id = 5 },
            new Brand("Suzuki") { Id = 6 },
            new Brand("Mitsubishi") { Id = 7 },
            new Brand("Honda") { Id = 8 },
            new Brand("Volkswagen") { Id = 9 },
            new Brand("Ford") { Id = 10 },
            new Brand("Mercedes-Benz") { Id = 11 },
            new Brand("Audi") { Id = 12 },
            new Brand("BMW") { Id = 13 }
        };
    }
}