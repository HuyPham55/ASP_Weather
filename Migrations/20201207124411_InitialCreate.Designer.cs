// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weather.Contexts;

namespace Weather.Migrations
{
    [DbContext(typeof(CityContext))]
    [Migration("20201207124411_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Weather.Models.City", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<float>("lat")
                        .HasColumnType("REAL");

                    b.Property<float>("lon")
                        .HasColumnType("REAL");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("state")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
