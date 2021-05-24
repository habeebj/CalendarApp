using System.Collections.Generic;

namespace CalendarApp.WebAPI.Models
{
    public class ErrorModel
    {
        public Dictionary<string, List<string>> Errors { get; private set; }

        public static ErrorModel Create(string message)
        {
            return new ErrorModel
            {
                Errors = new Dictionary<string, List<string>> {
                    { nameof(message), new List<string> { message }}
                }
            };
        }
    }
}