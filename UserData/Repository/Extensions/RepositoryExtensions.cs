using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using UserData.Entitys;
using UserData.Repositories;

namespace UserData.Repositories.Extensions;

public static class RepositoryExensions
{
    public static void AddBatch<T>(this IRepository<T> userRepository, T[] items)
        where T : class, IEntity
    {
        foreach (var item in items)
        {
            userRepository.Add(item);
        }
        userRepository.Save();
    }
}

