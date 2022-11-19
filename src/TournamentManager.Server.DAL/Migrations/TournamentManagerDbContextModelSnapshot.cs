﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentManager.Server.DAL;

#nullable disable

namespace TournamentManager.Server.DAL.Migrations
{
    [DbContext(typeof(TournamentManagerDbContext))]
    partial class TournamentManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.MatchEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Round")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score2")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("Team1Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Team2Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TournamentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Team1Id");

                    b.HasIndex("Team2Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.SportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TeamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LeaderId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TeamIsParticipatingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Approved")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TournamentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Participatings");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TournamentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxAttendees")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SportId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("WinnerTeamOverrideId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("SportId");

                    b.HasIndex("WinnerTeamOverrideId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.UserIsInTeamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersIsInTeam");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.MatchEntity", b =>
                {
                    b.HasOne("TournamentManager.Server.DAL.Entities.TeamEntity", "Team1")
                        .WithMany("Matches")
                        .HasForeignKey("Team1Id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TournamentManager.Server.DAL.Entities.TeamEntity", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TournamentManager.Server.DAL.Entities.TournamentEntity", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Team1");

                    b.Navigation("Team2");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TeamEntity", b =>
                {
                    b.HasOne("TournamentManager.Server.DAL.Entities.UserEntity", "Leader")
                        .WithMany("TeamsAsLeader")
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TeamIsParticipatingEntity", b =>
                {
                    b.HasOne("TournamentManager.Server.DAL.Entities.TeamEntity", "Team")
                        .WithMany("Participatings")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentManager.Server.DAL.Entities.TournamentEntity", "Tournament")
                        .WithMany("Participatings")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TournamentEntity", b =>
                {
                    b.HasOne("TournamentManager.Server.DAL.Entities.UserEntity", "Creator")
                        .WithMany("Tournaments")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentManager.Server.DAL.Entities.SportEntity", "Sport")
                        .WithMany("Tournaments")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TournamentManager.Server.DAL.Entities.TeamEntity", "WinnerTeamOverride")
                        .WithMany()
                        .HasForeignKey("WinnerTeamOverrideId");

                    b.Navigation("Creator");

                    b.Navigation("Sport");

                    b.Navigation("WinnerTeamOverride");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.UserIsInTeamEntity", b =>
                {
                    b.HasOne("TournamentManager.Server.DAL.Entities.TeamEntity", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentManager.Server.DAL.Entities.UserEntity", "User")
                        .WithMany("TeamsAsMember")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.SportEntity", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TeamEntity", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Members");

                    b.Navigation("Participatings");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.TournamentEntity", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Participatings");
                });

            modelBuilder.Entity("TournamentManager.Server.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("TeamsAsLeader");

                    b.Navigation("TeamsAsMember");

                    b.Navigation("Tournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
