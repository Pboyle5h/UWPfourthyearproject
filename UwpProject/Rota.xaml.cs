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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Rota : Page
    {
        public Rota()
        {
            this.InitializeComponent();
        }

        private async void addRota_Click(object sender, RoutedEventArgs e)
        {
         
            string uri = "http://localhost:4567/rota/" +App.user+"/"
                                                       + StartDate.Date.DayOfWeek + "/"
                                                       + StartDate.Date.Day+"-" 
                                                       + StartDate.Date.Month+"-"
                                                       + StartDate.Date.Year+"/" 
                                                       + StartTime.Time.Hours +":"
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
                if (javaResponse == "Duplicate")
                {
                    //toast(javaResponse);
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
    }
}
