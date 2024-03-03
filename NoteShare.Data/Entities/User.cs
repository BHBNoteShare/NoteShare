using BaliFramework.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteShare.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteShare.Data.Entities
{
    [Table("Users")]
    public class User : AbstractEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int NoteRate { get; set; }
        public string Discriminator { get; set; }

        public virtual UserType UserType { get; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.HasDiscriminator<string>("Discriminator")
                .HasValue<Teacher>("Teacher")
                .HasValue<Student>("Student")
                .HasValue<Parent>("Parent");

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
        }
    }

    public class Teacher : User
    {
        public IList<TeacherSubject> Subjects { get; set; }
        public IList<Note> Notes { get; set; }

        public string SchoolId { get; set; }
        public School School { get; set; }

        override public UserType UserType => UserType.Teacher;
    }

    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Users");
            builder.HasOne(s => s.School)
               .WithMany()
               .HasForeignKey(s => s.SchoolId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class Student : User
    {
        public string SchoolId { get; set; }
        public School School { get; set; }

        public IList<StudentPreference> Preferences { get; set; } = new List<StudentPreference>();
        public IList<Note> Notes { get; set; }

        override public UserType UserType => UserType.Student;
    }

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Users");
            builder.HasOne(s => s.School)
               .WithMany()
               .HasForeignKey(s => s.SchoolId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class Parent : User
    {
        public IList<Student> Children { get; set; } = new List<Student>();
        override public UserType UserType => UserType.Parent;
    }

    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.ToTable("Users");
        }
    }
}
