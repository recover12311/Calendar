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
    public class CalendarEventBuilder : EventBuilder
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "GoogleCalendar";

        private Event calendarEvent;
        private EventDateTime startDateTime;
        private EventDateTime endDateTime;
        public string calendarId { get; private set; }
        public UserCredential credential { get; private set; }
        public CalendarService service { get; private set; }

        public CalendarEventBuilder(string pathToCredentials)
        {
            calendarEvent = new Event();
            startDateTime = new EventDateTime();
            endDateTime = new EventDateTime();
            calendarEvent.Reminders = new Event.RemindersData { UseDefault = false, Overrides = new List<EventReminder>() };

            if (!string.IsNullOrEmpty(pathToCredentials))
            {
                SetCredentionalFromFile(pathToCredentials);
                service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            else throw new Exception("pathToCredentials is not valid");
        }

        public override Event GetEvent()
        {
            return calendarEvent;
        }

        public override void SetCalendarId(string calendarId)
        {
            this.calendarId = calendarId;
        }

        public override void SetDescription(string decription)
        {
            calendarEvent.Description = decription;
        }

        public override void SetEndDate(DateTime dateTime)
        {
            endDateTime.Date = null;
            endDateTime.DateTime = dateTime;
            calendarEvent.End = endDateTime;
        }

        public override void SetStartDate(DateTime dateTime)
        {
            startDateTime.Date = null;
            startDateTime.DateTime = dateTime;
            calendarEvent.Start = startDateTime;
        }

        public override void SetSummary(string summary)
        {
            calendarEvent.Summary = summary;
        }

        public override void AddReminder(EventReminder reminder)
        {
            calendarEvent.Reminders.Overrides.Add(reminder);
        }

        public override void RemoveReminder(EventReminder reminder)
        {
            calendarEvent.Reminders.Overrides.Remove(reminder);
        }

        public void SetCredentionalFromFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        null).Result;
                }
            }
        }

        public void SetCalendarService(CalendarService calendarService)
        {
            service = calendarService;
        }

        public override Event InsertEventToCalendar()
        {
            Event recurringEvent = new Event();
            if (calendarEvent.Start != null && calendarEvent.End != null && !string.IsNullOrEmpty(calendarId))
            {
                recurringEvent = service.Events.Insert(calendarEvent, calendarId).Execute();
            }
            return recurringEvent;
        }

        //Sets an event for a full day
        public override void SetStartDate(string date)
        {
            startDateTime.DateTime = null;
            startDateTime.Date = date;
            calendarEvent.Start = startDateTime;
        }

        public override void SetEndDate(string date)
        {
            endDateTime.DateTime = null;
            endDateTime.Date = date;
            calendarEvent.End = endDateTime;
        }
    }
}
