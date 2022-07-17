using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }

        [RegularExpression(@"(Active|InActive)", ErrorMessage = "Status Must be Active or InActive")]
        public string Status { get; set; }
        public virtual ProductType productType { get; set; }
        [ForeignKey("productType")]
        public  int ProductTypeId { get; set; }
        public virtual List<ProductLog> ProductLogs { get; set; }

        //product type table id , type 
        //Aproduct lOG
        //pRODUCTid / uSERiD / aCTION (aDD / aCTIVE / dEACTIVATE ) , DATETIME

    }
}
