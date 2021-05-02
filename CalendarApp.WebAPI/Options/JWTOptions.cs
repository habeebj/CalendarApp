namespace CalendarApp.WebAPI.Options
{
    public class JWTOptions
    {
        public const string Key = "JWTOptions";
        public string Secret { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}