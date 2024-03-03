using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("NoteComments")]
    public class NoteComment : AuditedEntity
    {
        public string NoteId { get; set; }
        public Note Note { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; }
    }

    public class NoteCommentConfiguration : IEntityTypeConfiguration<NoteComment>
    {
        public void Configure(EntityTypeBuilder<NoteComment> builder)
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
