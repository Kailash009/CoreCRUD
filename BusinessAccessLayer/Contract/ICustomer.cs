using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessAccessLayer.Contract
{
    public interface ICustomer
    {
        List<Customer> getallCustomers();
        List<SelectListItem> bindCity();
        Customer getsingleCustomer(int cid);
        bool AddCustomer(Customer cusObj,string filename);
        bool UpdateCustomer(Customer cusObj,string filename);
        bool DeleteCustomer(int cid);
    }
}




