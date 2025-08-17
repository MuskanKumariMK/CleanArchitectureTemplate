namespace BuildingBlock.Tests.Helper
{
     /// <summary>
     /// Provides reusable assertion helpers for unit tests.
     /// 
     /// This class wraps common xUnit assertions with descriptive method names
     /// to improve readability and maintainability of unit tests.
     /// </summary>
     public static class TestAssertions
     {
          /// <summary>
          /// Asserts that the provided collection is empty.
          /// </summary>
          /// <typeparam name="T">Type of collection elements.</typeparam>
          /// <param name="collection">The collection to test.</param>
          public static void ShouldBeEmpty<T>(IEnumerable<T> collection)
          {
               Assert.Empty(collection);
          }

          /// <summary>
          /// Asserts that the provided collection contains the specified item.
          /// </summary>
          /// <typeparam name="T">Type of collection elements.</typeparam>
          /// <param name="collection">The collection to test.</param>
          /// <param name="item">The expected item.</param>
          public static void ShouldContain<T>(IEnumerable<T> collection, T item)
          {
               Assert.Contains(item, collection);
          }

          /// <summary>
          /// Asserts that the object is not null.
          /// </summary>
          /// <param name="obj">Object to check.</param>
          public static void ShouldNotBeNull(object obj)
          {
               Assert.NotNull(obj);
          }

          /// <summary>
          /// Asserts that the object is null.
          /// </summary>
          /// <param name="obj">Object to check.</param>
          public static void ShouldBeNull(object obj)
          {
               Assert.Null(obj);
          }

          /// <summary>
          /// Asserts that two objects are equal.
          /// </summary>
          /// <typeparam name="T">Type of objects.</typeparam>
          /// <param name="expected">Expected value.</param>
          /// <param name="actual">Actual value.</param>
          public static void ShouldBeEqual<T>(T expected, T actual)
          {
               Assert.Equal(expected, actual);
          }

          /// <summary>
          /// Asserts that a boolean condition is true.
          /// </summary>
          /// <param name="condition">The condition to check.</param>
          public static void ShouldBeTrue(bool condition)
          {
               Assert.True(condition);
          }

          /// <summary>
          /// Asserts that a boolean condition is false.
          /// </summary>
          /// <param name="condition">The condition to check.</param>
          public static void ShouldBeFalse(bool condition)
          {
               Assert.False(condition);
          }
     }
}

/* Example Usage:

var users = TestDataSeeder.Seed(5, i => new User { Id = i, Name = $"User {i}" });

// Collection assertions
TestAssertions.ShouldContain(users, users[0]);
TestAssertions.ShouldBeEmpty(new List<int>());

// Null checks
var service = CreateSut<MyService>();
TestAssertions.ShouldNotBeNull(service);

// Equality checks
TestAssertions.ShouldBeEqual(5, 2 + 3);

// Boolean checks
TestAssertions.ShouldBeTrue(users.Count > 0);
TestAssertions.ShouldBeFalse(users.Count == 0);

*/