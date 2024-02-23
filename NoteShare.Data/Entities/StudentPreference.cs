using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteShare.Models.StudentPreferences;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("StudentPreferences")]
    public class StudentPreference
    {
        public string PreferenceId { get; set; }
        public Subject Preference { get; set; }
        public SubjectLevel Level { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }

    public class StudentPreferenceConfiguration : IEntityTypeConfiguration<StudentPreference>
    {
        public void Configure(EntityTypeBuilder<StudentPreference> builder)
        {
            builder.HasKey(sp => new { sp.PreferenceId, sp.StudentId });
        }
    }
}
