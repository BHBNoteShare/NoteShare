using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("Subjects")]
    public class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SubjectLevel Level { get; set; }
    }

    public enum SubjectLevel
    {
        Mid,
        High
    }
}
