namespace Application.Common.Models
{
    public class ListResult<T> where T : class, new()
    {
        public ListResult(bool success, IList<T> values = null, IEnumerable<string> errors = null)
        {
            Errors = errors.ToArray();
            Succeeded = success;
            Items = values;
        }

        public static ListResult<T> SuccessListDataResult(IList<T> values)
        {
            return new ListResult<T>(true, values);
        }
        public static ListResult<T> SuccessListErrorResult(string[] errorMessages)
        {
            return new ListResult<T>(true, errors: errorMessages);
        }
        public IList<T> Items { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}
