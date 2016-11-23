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
    public class RootObject
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Details { get; set; }
        public string Hours { get; set; }
    }

    public class DeserializeJson
    {
        //Gets the class PicturePlaceObject and parses through the json responce Deserializing it into the class PicturePlaceObject
       

        public static RootObject FromJson(String jsonText)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject<RootObject>(jsonText);
            return jsonObject;
        }
    }

    public sealed partial class viewRota : Page
    {      
    public viewRota()
        {
            this.InitializeComponent();
            rotaView();      
        }

    

    private async void rotaView()
        {

            string uri = "http://localhost:4567/viewRota/" + App.user;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);

                dynamic javaResponse = (objReader.ReadToEnd());   
               // RootObject rt = JsonConvert.DeserializeObject<RootObject>(javaResponse);
                var list = JsonConvert.DeserializeObject<List<RootObject>>(javaResponse);
                //RootObject rt = new RootObject();
                foreach (RootObject rt in list)
                {

                    textBlockRota.Text += "Date: " + rt.Date +
                                     "\r\nTime: " + rt.Time +
                                     "\r\nDetails: " + rt.Details +
                                     "\r\nHours: " + rt.Hours+
                                     "\r\n\r\n";
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(viewRota));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    
}

