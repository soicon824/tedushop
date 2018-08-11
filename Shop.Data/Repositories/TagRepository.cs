using Shop.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Model.Models;
using System.Linq.Expressions;

namespace Shop.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
    }
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

    }
}
