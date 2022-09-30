using Follow_Up.Application.Enums;
using Follow_Up.Application.Helpers.Security.Encryption;
using Follow_Up.Application.Models.LoginDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Helpers.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenoptions;
        DateTime _accessTokenExp;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenoptions = Configuration.GetSection("Jwt").Get<TokenOptions>();

        }
        public AccessToken CreateToken(RolesEnum rolesEnum, int id)
        {
            _accessTokenExp = DateTime.Now.AddMinutes(_tokenoptions.AccessTokenExpretion);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenoptions.Key);
            var signingCredentials = SigningCreditianalsHelper.CreateSigningCreditianals(securityKey);
            var jwt = CreateJwtSecurityWebToken(_tokenoptions, signingCredentials, rolesEnum, id);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                ExpirationDate = _accessTokenExp
            };

        }
        private JwtSecurityToken CreateJwtSecurityWebToken(TokenOptions tokenOptions, SigningCredentials signingCredentials, RolesEnum rolesEnum, int id)
        {
            var jwt = new JwtSecurityToken
            (
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExp,
                notBefore: DateTime.Now,
                claims: SetClaims(rolesEnum, id),
                signingCredentials: signingCredentials
                );
            return jwt;
        }
        private IEnumerable<Claim> SetClaims(RolesEnum rolesEnum, int id)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("AccountRole", rolesEnum.ToString()));
            claims.Add(new Claim("Id", id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, rolesEnum.ToString()));
            return claims;
        }
    }
}
