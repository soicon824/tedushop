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
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var products = from p in DbContext.Products
                           join
                            pt in DbContext.ProductTags on
                            p.ID equals pt.ProductID
                           where pt.TagID == tagId
                           orderby p.CreatedDate
                           select p;
            totalRow = products.Count();
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
