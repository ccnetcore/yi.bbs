﻿// <auto-generated />
using System;
using CC.Yi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CC.Yi.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210506062121_jift1")]
    partial class jift1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("CC.Yi.Model.action", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("action_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("icon")
                        .HasColumnType("TEXT");

                    b.Property<int>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("router")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("action");
                });

            modelBuilder.Entity("CC.Yi.Model.comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("content")
                        .HasColumnType("TEXT");

                    b.Property<int?>("discussid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("time")
                        .HasColumnType("TEXT");

                    b.Property<int?>("usersid")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("discussid");

                    b.HasIndex("usersid");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("CC.Yi.Model.discuss", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("content")
                        .HasColumnType("TEXT");

                    b.Property<string>("introduction")
                        .HasColumnType("TEXT");

                    b.Property<int>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("plateid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("time")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .HasColumnType("TEXT");

                    b.Property<string>("type")
                        .HasColumnType("TEXT");

                    b.Property<int?>("usersid")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("plateid");

                    b.HasIndex("usersid");

                    b.ToTable("discuss");
                });

            modelBuilder.Entity("CC.Yi.Model.plate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("introduction")
                        .HasColumnType("TEXT");

                    b.Property<int>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("logo")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("userid")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("plate");
                });

            modelBuilder.Entity("CC.Yi.Model.role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("role_name")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("CC.Yi.Model.user", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("is_delete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("password")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("time")
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("actionrole", b =>
                {
                    b.Property<int>("actionsid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("rolesid")
                        .HasColumnType("INTEGER");

                    b.HasKey("actionsid", "rolesid");

                    b.HasIndex("rolesid");

                    b.ToTable("actionrole");
                });

            modelBuilder.Entity("actionuser", b =>
                {
                    b.Property<int>("actionsid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("usersid")
                        .HasColumnType("INTEGER");

                    b.HasKey("actionsid", "usersid");

                    b.HasIndex("usersid");

                    b.ToTable("actionuser");
                });

            modelBuilder.Entity("roleuser", b =>
                {
                    b.Property<int>("rolesid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("usersid")
                        .HasColumnType("INTEGER");

                    b.HasKey("rolesid", "usersid");

                    b.HasIndex("usersid");

                    b.ToTable("roleuser");
                });

            modelBuilder.Entity("CC.Yi.Model.comment", b =>
                {
                    b.HasOne("CC.Yi.Model.discuss", null)
                        .WithMany("comments")
                        .HasForeignKey("discussid");

                    b.HasOne("CC.Yi.Model.user", "users")
                        .WithMany("comments")
                        .HasForeignKey("usersid");

                    b.Navigation("users");
                });

            modelBuilder.Entity("CC.Yi.Model.discuss", b =>
                {
                    b.HasOne("CC.Yi.Model.plate", null)
                        .WithMany("discusses")
                        .HasForeignKey("plateid");

                    b.HasOne("CC.Yi.Model.user", "users")
                        .WithMany("discusses")
                        .HasForeignKey("usersid");

                    b.Navigation("users");
                });

            modelBuilder.Entity("CC.Yi.Model.plate", b =>
                {
                    b.HasOne("CC.Yi.Model.user", null)
                        .WithMany("plates")
                        .HasForeignKey("userid");
                });

            modelBuilder.Entity("actionrole", b =>
                {
                    b.HasOne("CC.Yi.Model.action", null)
                        .WithMany()
                        .HasForeignKey("actionsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CC.Yi.Model.role", null)
                        .WithMany()
                        .HasForeignKey("rolesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("actionuser", b =>
                {
                    b.HasOne("CC.Yi.Model.action", null)
                        .WithMany()
                        .HasForeignKey("actionsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CC.Yi.Model.user", null)
                        .WithMany()
                        .HasForeignKey("usersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("roleuser", b =>
                {
                    b.HasOne("CC.Yi.Model.role", null)
                        .WithMany()
                        .HasForeignKey("rolesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CC.Yi.Model.user", null)
                        .WithMany()
                        .HasForeignKey("usersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CC.Yi.Model.discuss", b =>
                {
                    b.Navigation("comments");
                });

            modelBuilder.Entity("CC.Yi.Model.plate", b =>
                {
                    b.Navigation("discusses");
                });

            modelBuilder.Entity("CC.Yi.Model.user", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("discusses");

                    b.Navigation("plates");
                });
#pragma warning restore 612, 618
        }
    }
}
