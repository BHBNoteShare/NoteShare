using NoteShare.Data.Entities;

namespace NoteShare.Data
{
    public class AuditedEntity : AbstractEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User CreatedUser { get; set; }
        public string CreatedBy { get; set; }
        public User LastModifiedUser { get; set; }
        public string ModifiedBy { get; set; }
    }
}
