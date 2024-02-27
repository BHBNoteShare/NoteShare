using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
            set => pageSize = value > 20 ? 20 : value < 10 ? 10 : value;
        }
    }

    public class SearchQueryParameters : QueryParameters
    {
        [MinLength(3)]
        [AllowNull]
        public string? SearchText { get; set; }
    }
}