using NoteShare.Data.Entities;

namespace NoteShare.Core
{
    public class NoteShareConfig
    {
        public NoteFileSetting NoteFileSetting { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<School> Schools { get; set; }
    }

    public class NoteFileSetting
    {
        public string Path { get; set; }
        public List<string> Extensions { get; set; }
        public string MaxSizeInMb { get; set; }
    }
}
