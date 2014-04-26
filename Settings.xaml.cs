using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

namespace vkapp
{
    public sealed partial class Settings : SettingsFlyout
    {
        public Settings()
        {
            this.InitializeComponent();

            if (storage == null)
            {
                authentication = new Authentication();
                storage = new Storage<AuthData>();
            }

            GetUserData();

            listView.Items.Add("");
            listView.Items.Add("");
            listView.Items.Add("");
        }

        private void GetUserData()
        {
            authData = storage.GetActiveUser();

            if (authData != null)
            {
                textBlockFirstname.Text = authData.firstname;
                textBlockLastname.Text = authData.lastname;
            }
        }

        private AuthData _authdata;
        private AuthData authData
        {
            get { return _authdata; }
            set
            {
                _authdata = value;
            }
        }

        private static Storage<AuthData> storage;
        Authentication authentication;

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            storage.Delete(authData.user_id);
            this.Hide();
            await authentication.GetAuthentecationStatus();
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
