using CSharpJson.Domain;

namespace CSharpJson.Application.Verification
{
    public interface IIdentificationService
    {
        public Task<TypeMessage> CheckType(string? message);
    }
}