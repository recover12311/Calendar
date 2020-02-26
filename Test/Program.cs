using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBuilder eventBuilder = new CalendarEventBuilder(Constants.Path);
            eventBuilder.SetStartDate("2020-02-25");
            eventBuilder.SetEndDate("2020-02-25");
            eventBuilder.SetSummary("test summary");
            eventBuilder.SetDescription("test decription");
            eventBuilder.SetCalendarId(Constants.CalendarIdPrimary);
            eventBuilder.AddReminder(new EventReminder { Method = Constants.Popup, Minutes = 10 });
            eventBuilder.InsertEventToCalendar();
            Console.WriteLine("All day event was added successfully");

            eventBuilder.SetStartDate(new DateTime(2020, 2, 23, 20, 18, 0));
            eventBuilder.SetEndDate(new DateTime(2020, 2, 23, 20, 40, 0));
            eventBuilder.SetSummary("test summary");
            eventBuilder.SetDescription("test decription");
            eventBuilder.SetCalendarId(Constants.CalendarIdPrimary);
            eventBuilder.AddReminder(new EventReminder { Method = Constants.Popup, Minutes = 10 });
            eventBuilder.InsertEventToCalendar();
            Console.WriteLine("Time event event was added successfully");
        }
    }
}
