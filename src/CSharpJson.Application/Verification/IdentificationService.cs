using System.Data;
using System.Text.Json;
using CSharpJson.Domain;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpJson.Application.Verification
{
    public sealed class IdentificationService : IIdentificationService
    {
        public async Task<TypeMessage> CheckType(string message)
        {

            var typeMessage = TypeMessage.Invalid;
            if (CheckFromJson(message))
                typeMessage = TypeMessage.Json;
            if (await CheckFromCode(message))
                typeMessage = TypeMessage.Code;
            return typeMessage;
        }

        private static bool CheckFromJson(string message)
        {
            try
            {
                var jsonDocument =JsonDocument.Parse(message);
                return jsonDocument.RootElement.EnumerateObject().Any(_ => !string.IsNullOrEmpty(_.Name) && !string.IsNullOrEmpty(_.Value.ToString()) || !string.IsNullOrEmpty(_.Name));
            }
            catch(Exception)
            {
                return false;
            }
        }

        private static async Task<bool> CheckFromCode(string message)
        {
            try
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(message);
                var rootTree = await syntaxTree.GetRootAsync();
                var result = rootTree.DescendantNodes().OfType<PropertyDeclarationSyntax>().Any(_ =>_.Modifiers.Any(syntaxToken => syntaxToken.Kind() == SyntaxKind.PublicKeyword));
                return result;
            }
            catch (SyntaxErrorException)
            {
                return false;
            }
        }
    }
}