using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("TeacherSubjects")]
    public class TeacherSubject
    {
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }

    public class TeacherSubjectConfiguration : IEntityTypeConfiguration<TeacherSubject>
    {
        public void Configure(EntityTypeBuilder<TeacherSubject> builder)
        {
            builder.HasKey(ts => new { ts.SubjectId, ts.TeacherId });
        }
    }
}
