using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UwpProject
{
   public class CallRota
    {
        public void Show(string content, string title)
        {
            IAsyncOperation<IUICommand> command = new MessageDialog(content, title).ShowAsync();
        }

        public async void Add(object sender, List<RootObject>test)
        {
            foreach (RootObject rt in test)
            {
                if (rt != null)
                {                                  
                    DateTime oDate = Convert.ToDateTime(rt.Date);
                    DateTime iDate = Convert.ToDateTime(rt.Time);
                    FrameworkElement element = (FrameworkElement)sender;
                    GeneralTransform transform = element.TransformToVisual(null);
                    Point point = transform.TransformPoint(new Point());
                    Rect rect = new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
                    DateTimeOffset date = oDate.Date;
                    TimeSpan time = iDate.TimeOfDay;
                    Appointment appointment = new Appointment()
                    {
                        StartTime = new DateTimeOffset(date.Year, date.Month, date.Day,
                        time.Hours, time.Minutes, 0, TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)),
                        Subject = "Working Today",
                        Location = "Flannary's Hotel",
                        Details = rt.Details,
                        Duration = TimeSpan.FromHours(Int32.Parse(rt.Hours)),
                    };
                    string id = await AppointmentManager.ShowAddAppointmentAsync(appointment, rect, Placement.Default);
                    if (string.IsNullOrEmpty(id))
                        Show("Date Added", "Rota ");
                    else
                        Show(string.Format("Date added", id), "Rota");
                }
            }
        }
        
    }
}
