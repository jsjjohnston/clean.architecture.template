using Domain.Primitives;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

[TestClass]
public class DomainArchitectureTests
{
    [TestMethod]
    public void Entities_Should_Inherit_From_Entity()
    {
        // Act
        var testResult = Types
            .InNamespace("Domain.Entities")
            .Should()
            .Inherit(typeof(Entity))
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [TestMethod]
    public void Exceptions_Should_Inherit_From_DomainException()
    {
        // Act
        var testResult = Types
            .InNamespace("Domain.Exceptions")
            .Should()
            .Inherit(typeof(DomainException))
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}
