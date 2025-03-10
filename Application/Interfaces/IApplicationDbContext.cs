﻿using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Account> Accounts { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}