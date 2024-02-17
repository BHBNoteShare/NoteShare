namespace NoteShare.Models
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int RecordNumber { get; set; }
        public int PageCount { get; set; }
        public List<T> Items { get; set; }
    }
}
