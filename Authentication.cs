using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace vkapp
{
    class Authentication
    {
        public Authentication()
        {
            authDataStorage = new Storage<AuthData>();
        }

        private Storage<AuthData> authDataStorage;

        public async Task<AuthData> CheckIfAuthenticatedAsync()
        {
            if (await authDataStorage.Delay())
            {
                Log.Logger.Log.Error("FUCK YEAH");
                AuthData authData = authDataStorage.GetActiveUser();
                return authData;
            }

            Log.Logger.Log.Error("FUCK NO");

            return null;
        }

        public async Task<AuthData> AuthenticateAsync()
        {
            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, AuthorizationParameters.requestUri, AuthorizationParameters.callbackUri);
            
            AuthData authData = null;

            try
            {
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    String tokenUri = WebAuthenticationResult.ResponseData;

                    String token = tokenUri.Split('=')[1].Split('&')[0];
                    String user_id = tokenUri.Split('=')[3].Split('&')[0];

                    authData = new AuthData { user_id = Int32.Parse(user_id), token = token, isActive = true };
                    authDataStorage.UpdateData(authData);
                }
                else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.UserCancel)
                {
                    throw new AuthenticationException("User has canceled authentication");
                }
                else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    throw new AuthenticationException("WebAuthentication ErrorHttp");
                }
                else
                {
                    throw new AuthenticationException("Unknown WebAuthenticationResult ResponseStatus : " + WebAuthenticationResult.ResponseStatus);
                }
            }
            catch(VKAppException)
            {

            }

            return authData;
        }
    }
}
