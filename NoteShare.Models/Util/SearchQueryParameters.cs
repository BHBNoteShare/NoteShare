using BaliFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NoteShare.Models.Util
{
    public class SearchQueryParameters : QueryParameters
    {
        [MinLength(3)]
        [AllowNull]
        public string? SearchText { get; set; }
    }
}
