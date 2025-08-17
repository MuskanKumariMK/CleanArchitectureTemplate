namespace BuildingBlock.Tests.Base
{
     /// <summary>
     /// Base class for all unit tests.
     /// 
     /// Provides common setup and utility methods for test classes.
     /// Inheriting from this class allows easy creation of objects under test (SUT) 
     /// and access to shared test helpers.
     /// </summary>
     public abstract class TestBase
     {
          /// <summary>
          /// Constructor for base test class.
          /// Can be extended to include common initialization logic for all tests.
          /// </summary>
          protected TestBase()
          {
               // Optional: add shared setup code here
          }

          /// <summary>
          /// Creates an instance of the class under test (SUT) using the default constructor.
          /// 
          /// This helps avoid repetitive object creation code in individual test methods.
          /// </summary>
          /// <typeparam name="T">The type of the object to create.</typeparam>
          /// <returns>A new instance of type T.</returns>
          protected T CreateSut<T>() where T : new()
          {
               return new T();
          }
     }
}

/* Example Usage:

public class SampleTests : TestBase
{
    [Fact]
    public void Can_Create_Instance()
    {
        // Use CreateSut to quickly instantiate the class under test
        var service = CreateSut<SampleService>();

        // Verify that the object was created
        Assert.NotNull(service);
    }
}

*/