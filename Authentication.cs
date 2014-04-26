using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.UI.ApplicationSettings;

namespace vkapp
{
    class Authentication
    {
        public Authentication()
        {
            authDataStorage = new Storage<AuthData>();
        }

        private Storage<AuthData> authDataStorage;
        private AuthData authData;

        public async Task<AuthData> GetAuthentecationStatus()
        {
            await authDataStorage.CreateStorageFile();

            authData = authDataStorage.GetActiveUser();

            if (authData != null)
            {
                SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
            }
            else
            {
                SettingsPane.GetForCurrentView().CommandsRequested -= App_CommandsRequested;

                await AuthenticateAsync();
            }

            return authData;
        }

        public async Task AuthenticateAsync()
        {
            WebAuthenticationResult WebAuthenticationResult;
            
            try
            {
                WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, AuthorizationParameters.requestUri, AuthorizationParameters.callbackUri);

                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    String tokenUri = WebAuthenticationResult.ResponseData;

                    String token = tokenUri.Split('=')[1].Split('&')[0];
                    String user_id = tokenUri.Split('=')[3].Split('&')[0];

                    authData = new AuthData { user_id = Int32.Parse(user_id), token = token, isActive = true };
                    authDataStorage.UpdateData(authData);

                    SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
                }
                else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.UserCancel)
                {
                    await AuthenticateAsync();
                    //throw new AuthenticationException("User has canceled authentication");
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
            catch(AuthenticationException e)
            {
                SettingsPane.GetForCurrentView().CommandsRequested -= App_CommandsRequested;
                Log.Logger.Log.Error("Authentication failed : " + e.InnerException);
            }
        }

        private static Settings settings;
        private void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand settingsCommand = new SettingsCommand("settings", "Settings",
                (handler) =>
                {
                    if (settings == null)
                    {
                        settings = new Settings();
                    }

                    settings.Show();
                });

            args.Request.ApplicationCommands.Add(settingsCommand);

            SettingsCommand privacyCommand = new SettingsCommand("privacy", "Privacy policy",
                (handler) =>
                {
                    Windows.System.Launcher.LaunchUriAsync(new Uri("https://vk.com/privacy")).Cancel();
                });

            args.Request.ApplicationCommands.Add(privacyCommand);
        }
    }
}
