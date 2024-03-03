using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("NoteFiles")]
    public class NoteFile : AuditedEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }

        public string NoteId { get; set; }
        public Note Note { get; set; }
    }

    public class NoteFileConfiguration : IEntityTypeConfiguration<NoteFile>
    {
        public void Configure(EntityTypeBuilder<NoteFile> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.HasOne(n => n.CreatedUser)
                .WithMany()
                .HasForeignKey(n => n.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(n => n.LastModifiedUser)
                .WithMany()
                .HasForeignKey(n => n.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
