using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafePointSecurity.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        List<T> FindAll(string connectionString = null);

        bool Save(T entity);

        bool Delete(T entity);
    }
}
