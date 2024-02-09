﻿// <auto-generated />
using System;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiteRight.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240209220913_Update_product")]
    partial class Update_product
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BiteRight.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("photo_id");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("PhotoId")
                        .IsUnique()
                        .HasDatabaseName("ix_categories_photo_id");

                    b.ToTable("categories", "category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"),
                            PhotoId = new Guid("98eb4dc2-11b5-440b-bfc1-742fda8279b7")
                        },
                        new
                        {
                            Id = new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                            PhotoId = new Guid("5e4d81da-841b-493a-a47b-9f69791e1063")
                        },
                        new
                        {
                            Id = new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                            PhotoId = new Guid("2eaee2ac-3ebf-49f2-807b-1b0509f528ba")
                        },
                        new
                        {
                            Id = new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                            PhotoId = new Guid("4d4c96bc-6990-4b94-982e-d5e7860019a1")
                        },
                        new
                        {
                            Id = new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                            PhotoId = new Guid("2bfd1c0c-8882-44fa-b73d-8588ad8ec50b")
                        },
                        new
                        {
                            Id = new Guid("c82e0550-26cf-410d-8cec-5cf62bada757")
                        });
                });

            modelBuilder.Entity("BiteRight.Domain.Categories.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_photos");

                    b.ToTable("photos", "category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("98eb4dc2-11b5-440b-bfc1-742fda8279b7"),
                            Name = "dairy.webp"
                        },
                        new
                        {
                            Id = new Guid("5e4d81da-841b-493a-a47b-9f69791e1063"),
                            Name = "fruit.webp"
                        },
                        new
                        {
                            Id = new Guid("2eaee2ac-3ebf-49f2-807b-1b0509f528ba"),
                            Name = "vegetable.webp"
                        },
                        new
                        {
                            Id = new Guid("4d4c96bc-6990-4b94-982e-d5e7860019a1"),
                            Name = "meat.webp"
                        },
                        new
                        {
                            Id = new Guid("2bfd1c0c-8882-44fa-b73d-8588ad8ec50b"),
                            Name = "fish.webp"
                        });
                });

            modelBuilder.Entity("BiteRight.Domain.Categories.Translation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid")
                        .HasColumnName("language_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_category_translations");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_category_translations_category_id");

                    b.HasIndex("LanguageId")
                        .HasDatabaseName("ix_category_translations_language_id");

                    b.ToTable("category_translations", "category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("38097234-329c-4372-a54f-13e6c41004fa"),
                            CategoryId = new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"),
                            LanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Name = "Dairy"
                        },
                        new
                        {
                            Id = new Guid("96a2257a-f4c4-4c47-9b5b-50a7be92c5da"),
                            CategoryId = new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"),
                            LanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Name = "Nabiał"
                        },
                        new
                        {
                            Id = new Guid("c089f04f-fdfe-48e1-87b7-ff8028ecb67f"),
                            CategoryId = new Guid("e8c78317-70ac-4051-805e-ece2bb37656f"),
                            LanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Name = "Milchprodukte"
                        },
                        new
                        {
                            Id = new Guid("73127f14-9fe8-4dd7-b4bf-e11d98c6e505"),
                            CategoryId = new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                            LanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Name = "Fruit"
                        },
                        new
                        {
                            Id = new Guid("af5072c0-7282-481f-a623-f4e00feb2bf8"),
                            CategoryId = new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                            LanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Name = "Owoce"
                        },
                        new
                        {
                            Id = new Guid("532bfdc2-bd1a-40eb-8723-65ae4e76f242"),
                            CategoryId = new Guid("1fd7ed59-9e34-40ab-a03d-6282b5d9fd86"),
                            LanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Name = "Obst"
                        },
                        new
                        {
                            Id = new Guid("7ec1928d-1517-4ef1-a24f-cf20322f34a5"),
                            CategoryId = new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                            LanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Name = "Vegetable"
                        },
                        new
                        {
                            Id = new Guid("b9e41376-b606-42d3-b2af-8373c1905b87"),
                            CategoryId = new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                            LanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Name = "Warzywa"
                        },
                        new
                        {
                            Id = new Guid("115fbaa7-0bcc-4d41-b1f1-43fe1cdb28f9"),
                            CategoryId = new Guid("349774c7-3249-4245-a1e2-5b70c5725bbf"),
                            LanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Name = "Gemüse"
                        },
                        new
                        {
                            Id = new Guid("a2c2d7cb-e684-4c71-80e0-07aa1db6d5ba"),
                            CategoryId = new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                            LanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Name = "Meat"
                        },
                        new
                        {
                            Id = new Guid("f7f1c6e7-012f-449c-95a3-79459f9331fb"),
                            CategoryId = new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                            LanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Name = "Mięso"
                        },
                        new
                        {
                            Id = new Guid("04b42267-4c3e-4ac1-9972-90112ba7c952"),
                            CategoryId = new Guid("5e40ba93-d28c-4cf3-9e75-379040a18e52"),
                            LanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Name = "Fleisch"
                        },
                        new
                        {
                            Id = new Guid("bd8fefc6-70ce-4562-9730-274c589ca72b"),
                            CategoryId = new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                            LanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Name = "Fish"
                        },
                        new
                        {
                            Id = new Guid("15e9f981-3a22-4bb2-96f7-5cc559567794"),
                            CategoryId = new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                            LanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Name = "Ryby"
                        },
                        new
                        {
                            Id = new Guid("c605cdf4-8b17-4cf0-950a-5a9bee434145"),
                            CategoryId = new Guid("17c56168-c9ec-4ffb-a074-495a02ab0359"),
                            LanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Name = "Fisch"
                        },
                        new
                        {
                            Id = new Guid("f7b5e10f-0719-4731-831a-ffe0a1a1ed07"),
                            CategoryId = new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"),
                            LanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Name = "None"
                        },
                        new
                        {
                            Id = new Guid("abed62c9-41b4-462f-866b-06d714dec958"),
                            CategoryId = new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"),
                            LanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Name = "Brak"
                        },
                        new
                        {
                            Id = new Guid("206a3c95-fb6d-4127-a37b-9f328c021021"),
                            CategoryId = new Guid("c82e0550-26cf-410d-8cec-5cf62bada757"),
                            LanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Name = "Keine"
                        });
                });

            modelBuilder.Entity("BiteRight.Domain.Countries.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Alpha2Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("alpha2code");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid")
                        .HasColumnName("currency_id");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("english_name");

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("native_name");

                    b.Property<Guid>("OfficialLanguageId")
                        .HasColumnType("uuid")
                        .HasColumnName("official_language_id");

                    b.HasKey("Id")
                        .HasName("pk_countries");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_countries_currency_id");

                    b.HasIndex("OfficialLanguageId")
                        .HasDatabaseName("ix_countries_official_language_id");

                    b.ToTable("countries", "country");

                    b.HasData(
                        new
                        {
                            Id = new Guid("35d08361-f753-4db9-b88e-11c400d53eb7"),
                            Alpha2Code = "PL",
                            CurrencyId = new Guid("3b56a6de-3b41-4b10-934f-469ca12f4fe3"),
                            EnglishName = "Poland",
                            NativeName = "Polska",
                            OfficialLanguageId = new Guid("24d48691-7325-4703-b69f-8db933a6736d")
                        },
                        new
                        {
                            Id = new Guid("f3e4c5cb-229c-4b2d-90dc-f83cb4a45f75"),
                            Alpha2Code = "EN",
                            CurrencyId = new Guid("53dffab5-429d-4626-b1d9-f568119e069a"),
                            EnglishName = "England",
                            NativeName = "England",
                            OfficialLanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a")
                        },
                        new
                        {
                            Id = new Guid("1352de6e-c0bf-48c6-b703-fae0b254d642"),
                            Alpha2Code = "DE",
                            CurrencyId = new Guid("8b0a0882-3eb5-495a-a646-06d7e0e9fe99"),
                            EnglishName = "Germany",
                            NativeName = "Deutschland",
                            OfficialLanguageId = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3")
                        },
                        new
                        {
                            Id = new Guid("12e2937f-f04d-4150-a7ae-5ab1176a95d8"),
                            Alpha2Code = "US",
                            CurrencyId = new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                            EnglishName = "United States of America",
                            NativeName = "United States of America",
                            OfficialLanguageId = new Guid("454faf9a-644c-445c-89e3-b57203957c1a")
                        });
                });

            modelBuilder.Entity("BiteRight.Domain.Currencies.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ISO4217Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("iso4217code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.HasKey("Id")
                        .HasName("pk_currencies");

                    b.ToTable("currencies", "currency");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3b56a6de-3b41-4b10-934f-469ca12f4fe3"),
                            ISO4217Code = "PLN",
                            Name = "Polski złoty",
                            Symbol = "zł"
                        },
                        new
                        {
                            Id = new Guid("53dffab5-429d-4626-b1d9-f568119e069a"),
                            ISO4217Code = "GBP",
                            Name = "Pound sterling",
                            Symbol = "£"
                        },
                        new
                        {
                            Id = new Guid("8b0a0882-3eb5-495a-a646-06d7e0e9fe99"),
                            ISO4217Code = "EUR",
                            Name = "Euro",
                            Symbol = "€"
                        },
                        new
                        {
                            Id = new Guid("e862f33f-a04a-4b4e-a4bb-9542b1db3eeb"),
                            ISO4217Code = "USD",
                            Name = "United States dollar",
                            Symbol = "$"
                        });
                });

            modelBuilder.Entity("BiteRight.Domain.Languages.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("english_name");

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("native_name");

                    b.HasKey("Id")
                        .HasName("pk_languages");

                    b.ToTable("languages", "language");

                    b.HasData(
                        new
                        {
                            Id = new Guid("24d48691-7325-4703-b69f-8db933a6736d"),
                            Code = "pl",
                            EnglishName = "Polish",
                            NativeName = "Polski"
                        },
                        new
                        {
                            Id = new Guid("454faf9a-644c-445c-89e3-b57203957c1a"),
                            Code = "en",
                            EnglishName = "English",
                            NativeName = "English"
                        },
                        new
                        {
                            Id = new Guid("c1dd0a3b-70d3-4aa1-b53e-4c08a03b57c3"),
                            Code = "de",
                            EnglishName = "German",
                            NativeName = "Deutsch"
                        });
                });

            modelBuilder.Entity("BiteRight.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("AddedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_date_time");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("Usage")
                        .HasColumnType("double precision")
                        .HasColumnName("usage");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_products_category_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_products_user_id");

                    b.ToTable("products", "product");
                });

            modelBuilder.Entity("BiteRight.Domain.Users.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid")
                        .HasColumnName("currency_id");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("time_zone");

                    b.HasKey("Id")
                        .HasName("pk_profiles");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_profiles_currency_id");

                    b.ToTable("profiles", "user");
                });

            modelBuilder.Entity("BiteRight.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_id");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("joined_at");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid")
                        .HasColumnName("profile_id");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_identity_id");

                    b.HasIndex("ProfileId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_profile_id");

                    b.ToTable("users", "user");
                });

            modelBuilder.Entity("BiteRight.Domain.Categories.Category", b =>
                {
                    b.HasOne("BiteRight.Domain.Categories.Photo", "Photo")
                        .WithOne()
                        .HasForeignKey("BiteRight.Domain.Categories.Category", "PhotoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_categories_photo_photo_temp_id");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("BiteRight.Domain.Categories.Translation", b =>
                {
                    b.HasOne("BiteRight.Domain.Categories.Category", "Category")
                        .WithMany("Translations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_translations_categories_category_temp_id1");

                    b.HasOne("BiteRight.Domain.Languages.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_translations_languages_language_temp_id");

                    b.Navigation("Category");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("BiteRight.Domain.Countries.Country", b =>
                {
                    b.HasOne("BiteRight.Domain.Currencies.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_countries_currencies_currency_temp_id");

                    b.HasOne("BiteRight.Domain.Languages.Language", "OfficialLanguage")
                        .WithMany()
                        .HasForeignKey("OfficialLanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_countries_languages_official_language_temp_id1");

                    b.Navigation("Currency");

                    b.Navigation("OfficialLanguage");
                });

            modelBuilder.Entity("BiteRight.Domain.Products.Product", b =>
                {
                    b.HasOne("BiteRight.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_products_categories_category_id");

                    b.HasOne("BiteRight.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_products_users_user_temp_id1");

                    b.OwnsOne("BiteRight.Domain.Products.DisposedState", "DisposedState", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<bool>("Disposed")
                                .HasColumnType("boolean")
                                .HasColumnName("disposed_state_disposed");

                            b1.Property<DateTime?>("DisposedDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("disposed_state_disposed_date");

                            b1.HasKey("ProductId");

                            b1.ToTable("products", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId")
                                .HasConstraintName("fk_products_products_id");
                        });

                    b.OwnsOne("BiteRight.Domain.Products.ExpirationDate", "ExpirationDate", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<int>("Kind")
                                .HasColumnType("integer")
                                .HasColumnName("expiration_date_kind");

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("date")
                                .HasColumnName("expiration_date_value");

                            b1.HasKey("ProductId");

                            b1.ToTable("products", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId")
                                .HasConstraintName("fk_products_products_id");
                        });

                    b.OwnsOne("BiteRight.Domain.Products.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<Guid>("CurrencyId")
                                .HasColumnType("uuid")
                                .HasColumnName("price_currency_id");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("price_value");

                            b1.HasKey("ProductId");

                            b1.ToTable("products", "product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId")
                                .HasConstraintName("fk_products_products_id");
                        });

                    b.Navigation("Category");

                    b.Navigation("DisposedState")
                        .IsRequired();

                    b.Navigation("ExpirationDate")
                        .IsRequired();

                    b.Navigation("Price");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BiteRight.Domain.Users.Profile", b =>
                {
                    b.HasOne("BiteRight.Domain.Currencies.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_profiles_currencies_currency_temp_id2");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("BiteRight.Domain.Users.User", b =>
                {
                    b.HasOne("BiteRight.Domain.Users.Profile", "Profile")
                        .WithOne()
                        .HasForeignKey("BiteRight.Domain.Users.User", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_profiles_profile_temp_id");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("BiteRight.Domain.Categories.Category", b =>
                {
                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
