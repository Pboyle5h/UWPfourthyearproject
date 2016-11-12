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
    
    public sealed partial class Reg : Page
    {
        string Username;
        string password;
        string email;
        string role;

        public Reg()
        {
            this.InitializeComponent();
        }

        private async void submitReg_Click(System.Object sender, RoutedEventArgs e)
        {
            Username = usernameReg.Text;
            password = passwordReg.Text;
            email = emailReg.Text;
            role = roleReg.Text;
            string uri = "http://localhost:4567/test/" + usernameReg.Text+"/"+passwordReg.Text+"/"+emailReg.Text+"/"+roleReg.Text;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);

                //dynamic movie = JsonConvert.DeserializeObject(objReader.ReadToEnd());

                response.Dispose();
            }
            catch (WebException ex)
            {
                //if connection failed, output message to user
                errorMessage.Visibility = Visibility.Visible;
                errorMessage.Text = "Failed to connect to server\nPlease check your internet connection";
            }


        }


    }
}
