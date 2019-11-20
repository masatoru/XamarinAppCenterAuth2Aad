using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Auth;
using Xamarin.Forms;

namespace XamarinAppCenterAuth2Aad
{


    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        UserInformation _userInfo { get; set; }
        const string _baseUrl = "https://{Your Web Site URL}.azurewebsites.net/";
        //const string _apiUrl = _baseUrl + "api/hello";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnCallApi_OnClicked(object sender, EventArgs e)
        {
                var jsonText = await CallWebApi("api/hello",false);
                await DisplayAlert("API", jsonText, "OK");
        }
        private async void BtnCallApiAuth_OnClicked(object sender, EventArgs e)
        {
            var jsonText = await CallWebApi("api/Admin",true);
            await DisplayAlert("API", jsonText, "OK");
        }

        private async void ClickSignIn(object sender, EventArgs e)
        {
            if(await SignIn())
            {
                await DisplayAlert("SignIn", $"AccessToken={_userInfo.AccessToken}", "OK");
            }
        }
        private void ClickSignOut(object sender, EventArgs e)
        {
            Auth.SignOut();
        }

        async Task<bool> SignIn()
        {
            try
            {
                // Sign-in succeeded.
                _userInfo = await Auth.SignInAsync();
                Console.WriteLine($"ID Token={_userInfo.IdToken}");
                Console.WriteLine($"Access Token={_userInfo.AccessToken}");
            }
            catch (Exception e)
                {
                await DisplayAlert("Error", e.ToString(), "OK");
                return false;
            }
            return true;
        }

        private async Task<string> CallWebApi(string apiName,bool isAuth)
        {
            using (var client = new HttpClient())
            {
                if(isAuth)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _userInfo.AccessToken);
                    client.DefaultRequestHeaders.ExpectContinue = false;
                }

                var res = await client.GetAsync($"{_baseUrl}{apiName}");

                // OK(200)でなくてエラーの場合の処理
                if (!res.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error... {res.ToString()}");
                    return res.ToString();
                    //throw new Exception(res.ToString());
                }

                // 結果のJSONを受け取る
                var resultText = await res.Content.ReadAsStringAsync();

                return resultText;
            }
        }
    }
}
