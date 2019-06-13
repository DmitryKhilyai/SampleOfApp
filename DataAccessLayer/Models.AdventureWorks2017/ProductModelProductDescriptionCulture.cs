using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models.AdventureWorks2017
{
    public partial class ProductModelProductDescriptionCulture
    {
        public int ProductModelId { get; set; }
        public int ProductDescriptionId { get; set; }
        public string CultureId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Culture Culture { get; set; }
        public virtual ProductDescription ProductDescription { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}
