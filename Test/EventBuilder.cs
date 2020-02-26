using Google.Apis.Calendar.v3.Data;
using System;

namespace Test
{
    public abstract class EventBuilder
    {
        public abstract void SetStartDate(DateTime dateTime);
        public abstract void SetStartDate(string date);
        public abstract void SetEndDate(DateTime dateTime);
        public abstract void SetEndDate(string date);
        public abstract void SetSummary(string summary);
        public abstract void SetDescription(string decription);
        public abstract void SetCalendarId(string calendarId);
        public abstract void AddReminder(EventReminder reminder);
        public abstract void RemoveReminder(EventReminder reminder);
        public abstract Event GetEvent();
        public abstract Event InsertEventToCalendar();
    }
}
