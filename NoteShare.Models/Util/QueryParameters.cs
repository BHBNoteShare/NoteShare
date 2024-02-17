namespace NoteShare.Models
{
    public class QueryParameters
    {
        private int pageSize = 10;
        private int pageNumber = 1;
        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = value <= 1 ? 1 : value;
        }
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > 50 ? 50 : value < 10 ? 10 : value;
        }
    }
}