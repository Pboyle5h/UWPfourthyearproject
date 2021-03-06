﻿using Newtonsoft.Json;
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



    public sealed partial class viewRota : Page
    {
        CallRota cr = new CallRota();
        List<RootObject> test = new List<RootObject>();
 
        public viewRota()
        {
            this.InitializeComponent();
            rotaView();      
        }

    

    public async void rotaView()
        {

            string uri = "https://javaapiuwp.herokuapp.com/viewRota/" + App.user;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;

            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);

                dynamic javaResponse = (objReader.ReadToEnd());   
                var list = JsonConvert.DeserializeObject<List<RootObject>>(javaResponse);

                //loops through the list elements
                foreach (RootObject rt in list)
                {
                    //if the list element is not equal to null it enters the if statement
                    //This makes sure that only accurate data is shown.
                    if (rt != null)
                    {
                        //appends the rota on to the screen for the employee 
                        textBlockRota.Text += "Date: " + rt.Date +
                                         "\r\nTime: " + rt.Time +
                                         "\r\nDetails: " + rt.Details +
                                         "\r\nHours: " + rt.Hours +
                                         "\r\n\r\n";
                        //passes the list into a global list to be transfered on button click
                        test = list;
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
        //button click to return to previous page. 
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeePage));
        }
        // on button click its pass the list into the call rota class to be processed.
        private void add_Click(object sender, RoutedEventArgs e)
        {
            cr.Add(sender, test);
        }
    }

    
}

