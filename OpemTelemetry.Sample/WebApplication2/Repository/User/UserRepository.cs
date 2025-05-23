﻿using Common.AOP;

namespace WebApplication2.Repository.User;

[Tracing]
public class UserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Common.Models.User?> GetAsync(int id, CancellationToken ct)
    {
        var result = await _dataContext.Users.FindAsync(id, ct);
        return result;
    }

    public async Task<int> CreateAsync(Common.Models.User user, CancellationToken ct)
    {
        await _dataContext.Users.AddAsync(user, ct);
        var result = await _dataContext.SaveChangesAsync(ct);
        return result;
    }

    //Delete
    public async Task<int> DeleteAsync(int id, CancellationToken ct)
    {
        var user = await _dataContext.Users.FindAsync(id, ct);
        _dataContext.Users.Remove(user);
        var result = await _dataContext.SaveChangesAsync(ct);
        return result;
    }
}
