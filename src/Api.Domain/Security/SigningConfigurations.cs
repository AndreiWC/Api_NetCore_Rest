using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Api.Domain.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        // IMPLEMENTAÇÃO DA CLASSE PARA GERAR UMA CREDENCIAL QUE POSSIBILITA O USO DA API
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true)); // GERA UMA KEY DE SEGURANÇA
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature); // COM A CHAVE KEY GERA UMA CREDENCIAL COM UM ALGORITMO DE SEGURANÇA
        }
    }
}