﻿using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; }
        DbSet<TodoItem> TodoItems { get; }
        DbSet<Blog> Blogs { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
