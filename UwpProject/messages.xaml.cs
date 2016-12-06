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
    public class MessageBody
    {
        public string User { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
    }
    public sealed partial class messages : Page
    {
        public messages()
        {
            this.InitializeComponent();
            messageView();
        }

        public async void messageView()
        {

            string uri = "https://javaapiuwp.herokuapp.com/viewMessages";
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);

                dynamic javaResponse = (objReader.ReadToEnd());
                var list = JsonConvert.DeserializeObject<List<MessageBody>>(javaResponse);

                //loops through the list elements
                foreach (MessageBody rt in list)
                {
                    //if the list element is not equal to null it enters the if statement
                    //This makes sure that only accurate data is shown.

                    if (rt != null)
                    {
                        //appends the rota on to the screen for the employee 
                        textBlockMessages.Text += "Username: " + rt.User+
                                         "\r\nDate: " + rt.Date +
                                         "\r\nDetails: " + rt.Details +
                                         "\r\n\r\n";
                        //passes the list into a global list to be transfered on button click
                    }   
                    

                }

                response.Dispose();
            }
            catch (WebException ex)
            {
                //if connection failed, output message to user
                errorMessage.Visibility = Visibility.Visible;
                errorMessage.Text = "Failed to connect to server\nPlease check your internet connection";

            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManagerPage));
        }
    }
}
