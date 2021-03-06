﻿using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
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
      
              

        public Reg()
        {
            this.InitializeComponent();

            
        }
        private void toast(string response)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
        {
            new AdaptiveText()
            {
                Text = "error"
            },

            new AdaptiveText()
            {
                Text = response
            },
            
        },

                    
                }
            };


            // Now we can construct the final toast content
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,                
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
            toast.ExpirationTime = DateTime.Now.AddSeconds(3);
        }

        private async void submitReg_Click(System.Object sender, RoutedEventArgs e)
        {
          
            string role = "Employee";
            string uri = "https://javaapiuwp.herokuapp.com/test/" + usernameReg.Text+"/"+passwordReg.Password+"/"+emailReg.Text+"/"+role;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);
                dynamic javaResponse = (objReader.ReadToEnd());
                //if java response equals dupliate then it shows the error toast
                if (javaResponse == "Duplicate")
                {
                    toast(javaResponse);
                }
                //if response = success return to sign in page.
                else if (javaResponse == "success")
                {
                    this.Frame.Navigate(typeof(MainPage));
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


    }
}
