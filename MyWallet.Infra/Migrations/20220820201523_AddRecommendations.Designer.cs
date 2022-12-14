// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWallet.Infra.Data.Context;

#nullable disable

namespace MyWallet.Infra.Migrations
{
    [DbContext(typeof(MyWalletContext))]
    [Migration("20220820201523_AddRecommendations")]
    partial class AddRecommendations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyWallet.Domain.Entities.Administrator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cnpj");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("AdministratorId")
                        .HasColumnType("bigint");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cnpj");

                    b.HasIndex("AdministratorId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Earning", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCom")
                        .HasColumnType("datetime2");

                    b.Property<int>("DividendType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("TickerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TotalValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TickerId");

                    b.ToTable("Earning");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Negociation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOperation")
                        .HasColumnType("datetime2");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("TickerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TickerId");

                    b.ToTable("Negociation");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Patrimony", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("TickerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TickerId");

                    b.ToTable("Patrimony");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Recommendation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("LimitePrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("RecommendedWalletId")
                        .HasColumnType("bigint");

                    b.Property<long>("TickerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Weight")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("RecommendedWalletId");

                    b.HasIndex("TickerId");

                    b.ToTable("Recommendation");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.RecommendedWallet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("RecommendedWallet");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Ticker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.HasIndex("CompanyId");

                    b.ToTable("Ticker");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Company", b =>
                {
                    b.HasOne("MyWallet.Domain.Entities.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorId");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Earning", b =>
                {
                    b.HasOne("MyWallet.Domain.Entities.Ticker", "Ticker")
                        .WithMany("Earnings")
                        .HasForeignKey("TickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticker");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Negociation", b =>
                {
                    b.HasOne("MyWallet.Domain.Entities.Ticker", "Ticker")
                        .WithMany()
                        .HasForeignKey("TickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticker");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Patrimony", b =>
                {
                    b.HasOne("MyWallet.Domain.Entities.Ticker", "Ticker")
                        .WithMany()
                        .HasForeignKey("TickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticker");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Recommendation", b =>
                {
                    b.HasOne("MyWallet.Domain.Entities.RecommendedWallet", "RecommendedWallet")
                        .WithMany("Recommendations")
                        .HasForeignKey("RecommendedWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWallet.Domain.Entities.Ticker", "Ticker")
                        .WithMany()
                        .HasForeignKey("TickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecommendedWallet");

                    b.Navigation("Ticker");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Ticker", b =>
                {
                    b.HasOne("MyWallet.Domain.Entities.Company", "Company")
                        .WithMany("Tickers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Company", b =>
                {
                    b.Navigation("Tickers");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.RecommendedWallet", b =>
                {
                    b.Navigation("Recommendations");
                });

            modelBuilder.Entity("MyWallet.Domain.Entities.Ticker", b =>
                {
                    b.Navigation("Earnings");
                });
#pragma warning restore 612, 618
        }
    }
}
