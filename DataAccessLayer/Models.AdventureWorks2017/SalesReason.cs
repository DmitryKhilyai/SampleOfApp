using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models.AdventureWorks2017
{
    public partial class SalesReason
    {
        public SalesReason()
        {
            SalesOrderHeaderSalesReason = new HashSet<SalesOrderHeaderSalesReason>();
        }

        public int SalesReasonId { get; set; }
        public string Name { get; set; }
        public string ReasonType { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
    }
}
