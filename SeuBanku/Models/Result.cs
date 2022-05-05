namespace SeuBanku.Models
{
    public class Result
    {
        public object? Content { get; set; }
        public Dictionary<object, object>? Errors { get; set; }
        public bool Successful { get; set; }
        public DateTime TimeStamp { get; } = DateTime.Now;
    }

    public static class ResultProvider
    {
        public static Result Set(object? content, bool sucessful, Dictionary<object, object>? errors = null)
        {
            return new Result
            {
                Content = content,
                Errors = errors,
                Successful = sucessful
            };
        }

        public static Result Set(bool sucessful = true)
        {
            return new Result
            {
                Successful = sucessful
            };
        }
    }
}
