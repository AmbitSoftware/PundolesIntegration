using CMSIntegration.CMSModels;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CMSIntegration.SuiteAPI;

namespace CMSIntegration.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //public ApplicationOAuthProvider(string publicClientId)
        //{
        //    if (publicClientId == null)
        //    {
        //        throw new ArgumentNullException("publicClientId");
        //    }

        //    _publicClientId = publicClientId;
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            UserModel userModel = new UserModel();
            var userName = context.UserName;
            var password = context.Password;
            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(password))
            {
                context.SetError("InvalidCredentials", "User Name and Password is mandatory.");
                return;
            }
            // Decrypty the password here
            var encryptPassword = SuiteWrapper.CreateMD5(password);
            var userManagement = userModel.GetUser(userName, encryptPassword);
            if (userManagement != null)
            {
                if(userManagement.user_status== "Inactive")
                {
                    context.SetError("deactivate", "Account is inactive");
                    return;
                }

                var identityNewUser = new ClaimsIdentity(context.Options.AuthenticationType);
                identityNewUser.AddClaim(new Claim("id", Convert.ToString(userManagement.id)));
                var propsInvalid = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {
                                "id", Convert.ToString(userManagement.id)
                            },
                            {
                                "user_name", Convert.ToString(userManagement.user_name)
                            },
                            {
                                "fullname", userManagement.first_name + " " + userManagement.last_name
                            },
                            {
                                "status", userManagement.user_status
                            },
                            {
                                "user_type", userManagement.user_type
                            }
                        });
                var ticketInvalid = new AuthenticationTicket(identityNewUser, propsInvalid);
                context.Validated(ticketInvalid);
                return;
            }
            else
            {
                context.SetError("InvalidCredentials", "Invalid Username and Password.");
                return;
            }

        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}