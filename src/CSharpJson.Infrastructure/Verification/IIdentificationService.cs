using CSharpJson.Domain;

namespace CSharpJson.Infrastructure.Verification
{
    public interface IIdentificationService
    {
        public Task<TypeMessage> CheckType(string? message);
    }
}