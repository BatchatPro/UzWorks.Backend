﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.SMS;
using UzWorks.Identity.Models;

namespace UzWorks.Identity;

public class UzWorksIdentityDbContext : IdentityDbContext<User, Role, string>
{
    public UzWorksIdentityDbContext(DbContextOptions<UzWorksIdentityDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.Migrate();
    }

    public DbSet<SmsToken> SmsTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        base.OnModelCreating(modelBuilder);


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}
