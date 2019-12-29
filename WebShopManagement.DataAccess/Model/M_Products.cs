using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebShopManagement.DataAccess.Model
{
    public class M_Products
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
    }

    public enum Products
    {
        ID,
        ProductName,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Status
    }
    public enum ProductsProcude
    {
        P_InsertOrUpdate_M_Products,
        P_M_Products_GetByID,
        P_GetAll_M_Products
    }
}
