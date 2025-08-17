using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Template.Architecture.Tests.Layers
{
     /// <summary>
     /// Architecture tests to enforce clean layering in the project.
     /// Ensures dependencies between Domain, Application, Infrastructure, and Presentation layers are respected.
     /// </summary>
     public class LayerTests
     {
          // Layer namespaces - update these with your project namespaces
          private const string DomainNamespace = "Template.Domain";
          private const string ApplicationNamespace = "Template.Application";
          private const string InfrastructureNamespace = "Template.Infrastructure";
          private const string PresentationNamespace = "Template.API";

          /// <summary>
          /// Domain layer should not depend on any other layers.
          /// </summary>
          [Fact]
          public void Domain_Should_Not_DependOnOtherLayers()
          {
               var domainAssembly = Assembly.Load(DomainNamespace);
               var forbiddenDeps = new[] { ApplicationNamespace, InfrastructureNamespace, PresentationNamespace };

               var testResult = Types.InAssembly(domainAssembly)
                                     .Should()
                                     .NotHaveDependencyOnAny(forbiddenDeps)
                                     .GetResult();

               testResult.IsSuccessful.Should().BeTrue("Domain layer should not depend on other layers");
          }

          /// <summary>
          /// Application layer should not depend on Infrastructure or Presentation layers.
          /// </summary>
          [Fact]
          public void Application_Should_Not_DependOnOtherLayers()
          {
               var appAssembly = Assembly.Load(ApplicationNamespace);
               var forbiddenDeps = new[] { InfrastructureNamespace, PresentationNamespace };

               var testResult = Types.InAssembly(appAssembly)
                                     .Should()
                                     .NotHaveDependencyOnAny(forbiddenDeps)
                                     .GetResult();

               testResult.IsSuccessful.Should().BeTrue("Application layer should not depend on Infrastructure or Presentation layers");
          }

          /// <summary>
          /// Infrastructure layer should not depend on Presentation layer.
          /// </summary>
          [Fact]
          public void Infrastructure_Should_Not_DependOnOtherLayers()
          {
               var infraAssembly = Assembly.Load(InfrastructureNamespace);
               var forbiddenDeps = new[] { PresentationNamespace };

               var testResult = Types.InAssembly(infraAssembly)
                                     .Should()
                                     .NotHaveDependencyOnAny(forbiddenDeps)
                                     .GetResult();

               testResult.IsSuccessful.Should().BeTrue("Infrastructure layer should not depend on Presentation layer");
          }

          /// <summary>
          /// Application handlers should depend on Domain layer.
          /// </summary>
          [Fact]
          public void Handler_Should_Have_DependencyOnDomain()
          {
               var appAssembly = Assembly.Load(ApplicationNamespace);

               var testResult = Types.InAssembly(appAssembly)
                                     .That()
                                     .HaveNameEndingWith("Handler")
                                     .Should()
                                     .HaveDependencyOn(DomainNamespace)
                                     .GetResult();

               testResult.IsSuccessful.Should().BeTrue("Application layer handlers should depend on Domain layer");
          }

          /// <summary>
          /// Controllers should depend on MediatR for sending requests.
          /// </summary>
          [Fact]
          public void Controllers_Should_Have_DependencyOnMediatR()
          {
               var presentationAssembly = Assembly.Load(PresentationNamespace);

               var testResult = Types.InAssembly(presentationAssembly)
                                     .That()
                                     .HaveNameEndingWith("Controller")
                                     .Should()
                                     .HaveDependencyOn("MediatR")
                                     .GetResult();

               testResult.IsSuccessful.Should().BeTrue("Controllers should depend on MediatR");
          }

          /// <summary>
          /// Controllers should not depend directly on Infrastructure layer.
          /// </summary>
          [Fact]
          public void Controllers_Should_Not_DependOnInfrastructure()
          {
               var presentationAssembly = Assembly.Load(PresentationNamespace);

               var testResult = Types.InAssembly(presentationAssembly)
                                     .That()
                                     .HaveNameEndingWith("Controller")
                                     .Should()
                                     .NotHaveDependencyOn(InfrastructureNamespace)
                                     .GetResult();

               testResult.IsSuccessful.Should().BeTrue("Controllers should not depend on Infrastructure layer directly");
          }
     }
}
