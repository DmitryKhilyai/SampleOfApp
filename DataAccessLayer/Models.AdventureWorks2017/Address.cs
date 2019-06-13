using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Types;

namespace DataAccessLayer.Models.AdventureWorks2017
{
    public partial class Address
    {
        public Address()
        {
            BusinessEntityAddress = new HashSet<BusinessEntityAddress>();
            SalesOrderHeaderBillToAddress = new HashSet<SalesOrderHeader>();
            SalesOrderHeaderShipToAddress = new HashSet<SalesOrderHeader>();
        }

        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int StateProvinceId { get; set; }
        public string PostalCode { get; set; }
        public SqlGeography SpatialLocation { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual StateProvince StateProvince { get; set; }
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderBillToAddress { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddress { get; set; }
    }
}
