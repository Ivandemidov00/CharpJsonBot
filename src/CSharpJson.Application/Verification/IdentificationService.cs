using System.Data;
using CSharpJson.Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;

namespace CSharpJson.Application.Verification
{
    public class IdentificationService : IIdentificationService
    {
        public async Task<TypeMessage> CheckType(string? message)
        {

            var typeMessage = TypeMessage.Invalid;
            if (CheckFromJson(message))
                typeMessage = TypeMessage.Json;
            if (await CheckFromCode(message))
                typeMessage = TypeMessage.Code;
            return typeMessage;
        }

        private bool CheckFromJson(string? message)
        {
            try
            {
                JToken.Parse(message);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private async Task<bool> CheckFromCode(string? message)
        {
            try
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(message);
                var rootTree = await syntaxTree.GetRootAsync();
                var result = rootTree.DescendantNodes().OfType<PropertyDeclarationSyntax>().Any(_ =>
                    _.Modifiers.Any(syntaxToken => syntaxToken.Kind() == SyntaxKind.PublicKeyword));
                return result;
            }
            catch (SyntaxErrorException)
            {
                return false;
            }
        }
        
    }
}