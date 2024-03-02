using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using BaliFramework.Database;

namespace NoteShare.Data.Entities
{
    [Table("Subjects")]
    public class Subject : AbstractEntity
    {
        public string Name { get; set; }

        public IList<StudentPreference> Preferences { get; set; } = new List<StudentPreference>();
        public IList<TeacherSubject> Teachers { get; set; } = new List<TeacherSubject>();
    }

    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
        }
    }
}
