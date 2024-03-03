using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteShare.Models.StudentPreferences;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("Notes")]
    public class Note : AuditedEntity
    {
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        public List<NoteFile> NoteFiles { get; set; }

        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public SubjectLevel SubjectLevel { get; set; }

        public int Saves { get; set; } //collection
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public int GetRate()
        {
            return Likes - Dislikes;
        }
    }

    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.HasOne(n=>n.CreatedUser)
                .WithMany()
                .HasForeignKey(n=>n.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(n=>n.LastModifiedUser)
                .WithMany()
                .HasForeignKey(n=>n.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
