﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
      
        string Username;
        string Password;

        public MainPage()
        {
            this.InitializeComponent();
        }

       

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Reg));
        }

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Username = usernameTbox.Text;
            Password = PasswordTbox.Password.ToString();
            string uri = "https://javaapiuwp.herokuapp.com/login/" + usernameTbox.Text + "/" + Password;
   
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);
                dynamic javaResponse= (objReader.ReadToEnd());
                if (javaResponse=="Manager")
                {
                    this.Frame.Navigate(typeof(ManagerPage));
                    App.user = Username;
                        
                }

                else if (javaResponse == "Employee")
                {
                    this.Frame.Navigate(typeof(EmployeePage));
                    App.user = Username;

                }
                else
                {
                    errorMessage.Visibility = Visibility.Visible;
                    errorMessage.Text = "Invalid Username or Password";
                }
                response.Dispose();
            }
            catch (WebException ex)
            {
                // if connection failed, output message to user
               
                errorMessage.Visibility = Visibility.Visible;
                errorMessage.Text = "Failed to connect to server\nPlease check your internet connection";
            }

        }
    }
}
