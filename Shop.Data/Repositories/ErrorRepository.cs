using Shop.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }
    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

    }
}
