using Moq;

namespace BuildingBlock.Tests.Helper
{/// <summary>
/// Helper class to create Moq mocks easily
/// </summary>
     public static class MockFactory
     {  /// <summary>
        /// Creates a mock of any interface or class
        /// </summary>
          public static Mock<T> CreateMock<T>() where T : class
          {
               return new Mock<T>();
          }

          /// <summary>
          /// Creates multiple mocks at once
          /// </summary>
          public static Dictionary<string, Mock<T>> CreateMocks<T>(params string[] names) where T : class
          {
               return names.ToDictionary(n => n, n => new Mock<T>());
          }
     }
}
// Example :
/* 
 * var mockRepo = MockFactory.CreateMock<IRepository>();
mockRepo.Setup(x => x.GetAll()).Returns(new List<Entity>());
*/