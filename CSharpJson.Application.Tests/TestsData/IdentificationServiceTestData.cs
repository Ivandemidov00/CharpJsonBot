using System.Collections;
using CSharpJson.Domain;

namespace CSharpJson.Application.Tests.TestsData;

public class IdentificationServiceTestData : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
            {"public record Command { public string SetWebHook { get; init; } public string SendMessage { get; init; }}", TypeMessage.Code};
        yield return new object[]
            {"public class Command { private string SetWebHook { get; init; } public string SendMessage { get; init; }}", TypeMessage.Code};
        yield return new object[]
            { "", TypeMessage.Invalid };
        yield return new object[]
            {"public class Command { private string SetWebHook { get; init; } private string SendMessage { get; init; }}", TypeMessage.Invalid};
        yield return new object[]
            {"public class Command { }", TypeMessage.Invalid};
        yield return new object[]
            {"{}:{}", TypeMessage.Invalid};
        yield return new object[]
            {"{{}:{}}", TypeMessage.Invalid};
        yield return new object[]
            {"{\"test\":\"test\"}", TypeMessage.Json};
        yield return new object[]
            {"{\"\":\"\"}", TypeMessage.Invalid};
        yield return new object[]
            {"{\"one\":\"test\"," +
             "\"two\":\"test\"}", TypeMessage.Json};
        yield return new object[]
            {"{\"one\":\"test1\"," +
             "\"two\":\"test2\"," +
             "\"three\":\"test3\"}", TypeMessage.Json};
        yield return new object[]
            {"{\"oneClass\":[{\"one\":\"test\"}]}", TypeMessage.Json};
    }
}