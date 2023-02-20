using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests
{
    [TestClass]
    public class CleanArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebNamespace = "Web";

        [TestMethod]
        [Description("In clean Clean Architecture the presentation layer should not have any dependencies on The Application, Presentation, Infrastructure or Web Projects")]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace,
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        [Description("In clean Clean Architecture the presentation layer should not have any dependencies on The Presentation, Infrastructure or Web Projects")]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace,
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        public void Handlers_Should_Have_DependencyOnDomain()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        [Description("In clean Clean Architecture the presentation layer should not have any dependencies on The Presentation or Web Projects")]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace,
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        [Description("In clean Clean Architecture the presentation layer should not have any dependencies on The Infustruture or Web Projects")]
        public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                WebNamespace,
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        [Description("In clean Clean Architecture the presentation layer should not have any dependencies on The Infustruture or Web Projects")]
        public void Controllers_Should_HaveDependencyOnMediatR()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;


            // Act
            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}