using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("Schools")]
    public class School : AbstractEntity
    {
        public string OM { get; set; }
        public string Name { get; set; }
        public string Tasks { get; set; }
        public string Address { get; set; }
    }

    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
        }
    }
}
