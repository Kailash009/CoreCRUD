using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonLayer.Models
{
    public class Customer
    {
        [Key]
        public int Cid { get; set; }

        [Required(ErrorMessage ="Customer Name is Required!!")]
        public string ?CName { get; set; }
        public int CAge { get; set; }
        public string ?City { get; set; }
        public decimal CSalary  { get; set; }
        public string ?custImg { get; set; }
        public IFormFile ?cust_img_File { get; set; }  // For File Upload
        public List<SelectListItem> ?Cities { get; set; }
    }
}


