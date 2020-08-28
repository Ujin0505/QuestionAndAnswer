using System;
using QuestionAndAnswer.Application.Common.Interfaces;

namespace QuestionAndAnswer.Infrastracture.Services
{
    public class DateTimeService: IDateTimeService
    {
        public DateTime DateTimeNow => DateTime.UtcNow;
        
        public string ToResponceFormat (DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}