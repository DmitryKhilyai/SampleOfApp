using System;
using Microsoft.SqlServer.Types;

namespace DataAccessLayer.Models.AdventureWorks2017
{
    public partial class ProductDocument
    {
        public int ProductId { get; set; }
        public SqlHierarchyId DocumentNode { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Document Document { get; set; }
    }
}
