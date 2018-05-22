using DevCommerce.Entities;
using System.Collections.Generic;

namespace DevCommerce.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        //Product Insert(Product category);
        //int Update(Product category);
    }
}
