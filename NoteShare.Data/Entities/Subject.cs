using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("Subjects")]
    public class Subject : AbstractEntity
    {
        public string Name { get; set; }
        public SubjectLevel Level { get; set; }
    }

    public enum SubjectLevel
    {
        Mid,
        High
    }
}
