using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessAccessLayer.Contract;
using CommonLayer.Models;
using DataAccessLayer;

namespace BusinessAccessLayer.Services
{
    public class CustomerService : ICustomer
    {
        public CustomerDbOperation custdb;
        public CustomerService()
        {
            custdb = new CustomerDbOperation();
        }
        public List<Customer> getallCustomers()
        {
            return custdb.GetCustomers();
        }
        public Customer getsingleCustomer(int cid)
        {
            return custdb.getCustomer(cid);
        }
        public bool AddCustomer(Customer cusObj,string filename)
        {
            return custdb.addCustomer(cusObj,filename);
        }
        public bool UpdateCustomer(Customer cusObj,string filename)
        {
            return custdb.updateCustomer(cusObj,filename);
        }
        public bool DeleteCustomer(int cid)
        {
            return custdb.deleteCustomer(cid);
        }
        public List<SelectListItem> bindCity()
        {
            return custdb.bindCity();
        }
    }
}
