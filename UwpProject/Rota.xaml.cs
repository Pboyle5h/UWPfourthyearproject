using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Rota : Page
    {
        public class RootObject
        {
            public string Username { get; set; }
          
        }
        public Rota()
        {
            this.InitializeComponent();
            getUsernames();
        }
        private async void addRota_Click(object sender, RoutedEventArgs e)
        {

            string uri = "https://javaapiuwp.herokuapp.com/rota/" + Username.SelectionBoxItem + "/"
                                                       + StartDate.Date.DayOfWeek + "/"
                                                       + StartDate.Date.Day + "-"
                                                       + StartDate.Date.Month + "-"
                                                       + StartDate.Date.Year + "/"
                                                       + StartTime.Time.Hours + ":"
                                                       + StartTime.Time.Minutes + "/"
                                                       + Details.Text + "/"
                                                       + Duration.SelectionBoxItem;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);

                dynamic javaResponse = (objReader.ReadToEnd());
                if (javaResponse== "success")
                {
                    this.Frame.Navigate(typeof(ManagerPage));
                }
                response.Dispose();
            }
            catch (WebException ex)
            {
                //if connection failed, output message to user
               // errorMessage.Visibility = Visibility.Visible;
               // errorMessage.Text = "Failed to connect to server\nPlease check your internet connection";

            }

        }

        private async void getUsernames()
        {

            string uri = "https://javaapiuwp.herokuapp.com/getusername/";
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);

                dynamic javaResponse = (objReader.ReadToEnd());               
                var list = JsonConvert.DeserializeObject<List<RootObject>>(javaResponse);
                foreach (RootObject rt in list)
                {
                    Username.Items.Add(rt.Username);
                }

                response.Dispose();
            }
            catch (WebException ex)
            {
                //if connection failed, output message to user
                // errorMessage.Visibility = Visibility.Visible;
                // errorMessage.Text = "Failed to connect to server\nPlease check your internet connection";

            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
