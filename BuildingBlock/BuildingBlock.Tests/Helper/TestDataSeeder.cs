namespace BuildingBlock.Tests.Helpers
{
     /// <summary>
     /// Provides generic test data generation utilities for unit tests.
     /// 
     /// This helper allows creating lists of any type T, 
     /// using a factory function to define how each item is created.
     /// This improves test readability and reduces repetitive boilerplate code.
     /// </summary>
     public static class TestDataSeeder
     {
          /// <summary>
          /// Generates a list of test data for any type using a factory function.
          /// 
          /// This is fully generic and can be used for any entity or DTO in your tests.
          /// </summary>
          /// <typeparam name="T">The type of entity to generate.</typeparam>
          /// <param name="count">Number of items to generate in the list.</param>
          /// <param name="factory">A function that takes an index (1-based) and returns an instance of T.</param>
          /// <returns>A list of <typeparamref name="T"/> containing <paramref name="count"/> items.</returns>
          /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is null.</exception>
          public static List<T> Seed<T>(int count, Func<int, T> factory)
          {
               // Ensure the factory function is provided
               if (factory == null) throw new ArgumentNullException(nameof(factory));

               // Generate a sequence from 1 to count and create an instance of T for each index using the factory
               return Enumerable.Range(1, count).Select(factory).ToList();
          }
     }
}

/* Example Usage:

// Generate a list of 5 users with unique properties
var users = TestDataSeeder.Seed(5, i => new User
{
    Id = i,
    Name = $"User {i}",
    Email = $"user{i}@example.com"
});

// Generate a list of 3 products
var products = TestDataSeeder.Seed(3, i => new Product
{
    Id = i,
    Name = $"Product {i}",
    Price = 10.0m * i
});

// Generate a list of integers
var numbers = TestDataSeeder.Seed(5, i => i * 10); // [10, 20, 30, 40, 50]

*/
