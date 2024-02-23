namespace NoteShare.Models.StudentPreferences
{
    public class StudentPreferenceDto
    {
        public string SubjectId { get; set; }
        public SubjectLevel SubjectLevel { get; set; }
    }

    public enum SubjectLevel
    {
        Mid,
        High
    }
}
