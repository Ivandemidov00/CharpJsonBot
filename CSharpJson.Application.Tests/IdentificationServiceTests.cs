using CSharpJson.Application.Tests.TestsData;
using CSharpJson.Domain;
using NUnit.Framework;
using CSharpJson.Application.Verification;
using FluentAssertions;

namespace CSharpJson.Application.Tests;

public class IdentificationServiceTests
{
    private IIdentificationService _identificationService = null!;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _identificationService = new IdentificationService();
    }

    [TestCaseSource(typeof(IdentificationServiceTestData))]
    public async Task CheckTypeTests(string data, TypeMessage expectedTypeMessage)
    {
         var type = await _identificationService.CheckType(data);
         type.Should().Be(expectedTypeMessage);
    }
}