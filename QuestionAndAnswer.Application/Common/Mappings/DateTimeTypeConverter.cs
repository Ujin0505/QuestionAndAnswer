using System;
using AutoMapper;

namespace QuestionAndAnswer.Application.Common.Mappings
{
    public class DateTimeTypeConverter: ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}