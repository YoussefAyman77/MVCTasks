using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Task02MVC.Models;

namespace Task02MVC.Data.Configrations
{
    public class StudCourseResultConfiguration : IEntityTypeConfiguration<StuCrsRes>
    {
        public void Configure(EntityTypeBuilder<StuCrsRes> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Property(sc => sc.Grade)
                   .IsRequired();

            builder.HasOne(sc => sc.Student)
                   .WithMany(s => s.StudCourseResults)
                   .HasForeignKey(sc => sc.StudentId);

            builder.HasOne(sc => sc.Course)
                   .WithMany(c => c.StudCourseResults)
                   .HasForeignKey(sc => sc.CourseId);
        }
    }
}
