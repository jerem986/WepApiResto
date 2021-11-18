using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Security.Configuration;
using ToolBox.Security.Models;
using ToolBox.Utils.Extensions;

namespace ToolBox.Security.Services
{
    public class JwtService
    {
        private readonly JwtConfiguration _config;
        private readonly JwtSecurityTokenHandler _handler;
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _credentials;
        private TokenValidationParameters _validationParameters;
        //private string _signature; -> furfooz

        public JwtService(JwtConfiguration config, JwtSecurityTokenHandler handler) //, IConfiguration configSignature)
        {
            _config = config;
            _handler = handler;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
            _validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = _config.ValidateIssuer,
                ValidIssuer = _config.Issuer,
                ValidateAudience = _config.ValidateAudience,
                ValidAudience = _config.Audience,
                ValidateLifetime = _config.ValidateLifetime,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey
            };
            //furfooz import
            //_signature = configSignature.GetSection("Jwt").GetValue<string>("Signature");
        }
        
        /// <summary>
        /// Creates token based on payload and configuration
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string CreateToken(IPayload payload)
        {
            JwtSecurityToken JwtToken = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                GetClaims(payload),
                _config.ValidateLifetime ? DateTime.Now : null,
                _config.ValidateLifetime ? DateTime.Now.AddSeconds(_config.ExpirationDuration) : null,
                _credentials
            );
            return _handler.WriteToken(JwtToken);
        }

        /// <summary>
        /// Token validation based on configuration
        /// Create a ClaimsPricipal or null if not valid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public bool TryGetClaims(string token, out ClaimsPrincipal claims)
        {
            try
            {
                claims = _handler.ValidateToken(token, _validationParameters, out SecurityToken secutityToken);
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                claims = null;
                return false;
            }
        }

        /// <summary>
        /// Token validation based on Google API
        /// Create a GoogleJsonWebSignature.Payload or null if not valid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _config.GoogleAuthSettings.ClientId }
                };
                return await GoogleJsonWebSignature.ValidateAsync(token, settings);
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }

        /// <summary>
        /// Get a list of claims based on payload
        /// If full is false then it will only return the basic claims
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="full"></param>
        /// <returns></returns>
        private IEnumerable<Claim> GetClaims(IPayload payload, bool full = true)
        { 
            // yield return basic claims
            yield return new Claim(ClaimTypes.Email, payload.Email);
            yield return new Claim(ClaimTypes.NameIdentifier, payload.Identifier);
            foreach (string role in payload.Roles) 
            {
                yield return new Claim(ClaimTypes.Role, role);
            }
            if(full)
            {
                // yield return other claims
                foreach (PropertyInfo prop in payload.GetType().GetProperties())
                {
                    if(prop.GetValue(payload) != null)
                        yield return CreatePropertyClaim(prop, payload);
                }
            }
        }

        /// <summary>
        /// Get Claim based on property and instance
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        private Claim CreatePropertyClaim(PropertyInfo prop, object instance)
        {
            if (prop.PropertyType.IsAssignableTo(typeof(IEnumerable)) && prop.PropertyType != typeof(string))
            {
                return new Claim(
                    prop.Name.ToLowerCamelCase(),
                    JsonConvert.SerializeObject(prop.GetValue(instance)),
                    JsonClaimValueTypes.JsonArray
                );
            }
            else
            {
                string type
                    = prop.PropertyType.IsNumericType() ? ClaimValueTypes.Integer64
                    : prop.PropertyType == typeof(DateTime) ? ClaimValueTypes.Date
                    : prop.PropertyType == typeof(bool) ? ClaimValueTypes.Boolean
                    : ClaimValueTypes.String;

                string value = prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)
                    ? prop.GetValue(instance)?.ToString()
                    : JsonConvert.SerializeObject(prop.GetValue(instance));

                return new Claim(
                    prop.Name.ToLowerCamelCase(),
                    value,
                    type
                );
            }
        }

        //ADD FUFROOZ METHOD

        //public ClaimsPrincipal Decode(string token)
        //{
        //    TokenValidationParameters validationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        ValidateLifetime = false,
        //        RequireSignedTokens = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signature))
        //    };
        //    try
        //    {
        //        ClaimsPrincipal claims = _handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
        //        return claims;
        //    }

        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
