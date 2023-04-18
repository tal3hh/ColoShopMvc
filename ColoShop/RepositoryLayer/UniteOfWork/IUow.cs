using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;
using RepositoryLayer.Repositories.Interface;

namespace RepositoryLayer.UniteOfWork
{
    public interface IUow
    {
        Task SaveChangesAsync();
        IRepository<T> GetRepository<T>() where T : BaseEntity;
    }
}
