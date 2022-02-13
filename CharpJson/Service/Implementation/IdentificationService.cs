using System.Linq;
using CharpJson.Service.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CharpJson.Service.Implementation
{
    public class IdentificationService : IIdentificationService
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

        private bool CheckFromJson(string message)
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

        private async Task<bool> CheckFromCode(string message)
        {
            try
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(message);
                var rootTree = await syntaxTree.GetRootAsync();
                var propertyDeclarationSyntaxes = rootTree.DescendantNodes().OfType<PropertyDeclarationSyntax>()
                    .First(_ => _.Modifiers.First() == SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
    }
}