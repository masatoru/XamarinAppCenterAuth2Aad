using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinAppCenterAuth2Aad
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        const string _baseUrl = "http://localhost:5000";
        const string _apiUrl = _baseUrl + "/api/hello";
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCallApi_OnClicked(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                var jsonText = await CallWebApi();
                await DisplayAlert("API", jsonText, "OK");
            }).Wait();
        }

        private static async Task<string> CallWebApi()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_apiUrl);

                // OK(200)でなくてエラーの場合の処理
                if (!res.IsSuccessStatusCode)
                {
                    throw new Exception(res.ToString());
                }

                // 結果のJSONを受け取る
                var resultText = await res.Content.ReadAsStringAsync();

                return resultText;
            }
        }
    }
}
