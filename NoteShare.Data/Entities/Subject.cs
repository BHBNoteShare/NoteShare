using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("Subjects")]
    public class Subject : AbstractEntity
    {
        public string Name { get; set; }
        public SubjectLevel Level { get; set; }

        public IList<StudentPreference> Preferences { get; set; } = new List<StudentPreference>();
        public IList<TeacherSubject> Teachers { get; set; } = new List<TeacherSubject>();
    }

    public enum SubjectLevel
    {
        Mid,
        High
    }
}
