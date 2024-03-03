using BaliFramework.Models;
using Microsoft.AspNetCore.Http;
using NoteShare.Models.StudentPreferences;
using System.ComponentModel.DataAnnotations;

namespace NoteShare.Models.Note
{
    public class NoteDto
    {
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public string SubjectId { get; set; }
        public SubjectLevel SubjectLevel { get; set; }
    }

    public class CreateNoteDto : NoteDto
    {
        public List<IFormFile> NoteFiles { get; set; }
    }

    public class UpdateNoteDto : CreateNoteDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public List<IFormFile> NoteFiles { get; set; }
    }

    public class ListNoteDto : NoteDto
    {
        public string Id { get; set; }
        public string SubjectName { get; set; }
        public int Rate { get; set; }
        public string CreatedUserName { get; set; }
    }

    public class ReadNoteDto : ListNoteDto
    {
        public List<NoteFileDto> NoteFiles { get; set; }
    }

    public class NoteSearchDto : QueryParameters
    {
        public string? Title { get; set; }
        public string? SubjectId { get; set; }
        public SubjectLevel? SubjectLevel { get; set; }
    }

    public class NoteFileDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }

    public class NoteCommentDto
    {
        public string NoteId { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; }
    }

    public class ListNoteCommentDto
    {
        public string? Id { get; set; } //ha a sajátja akkor kell, hogy tudja törölni
        [MaxLength(1000)]
        public string Comment { get; set; }
        public string CreatedUserName { get; set; }
    }
}