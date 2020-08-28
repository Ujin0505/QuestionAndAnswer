using System;

namespace QuestionAndAnswer.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        public DateTime DateTimeNow { get; }

        public string ToResponceFormat(DateTime dateTime);

    }
}