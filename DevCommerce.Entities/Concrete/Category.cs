using DevCommerce.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class Category : IEntity
    {
        public Category()
        {
            this.Categories = new HashSet<Category>();
            this.Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Nullable<int> ParentId { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
