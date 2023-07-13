using Microsoft.AspNetCore.Mvc;
using BusinessAccessLayer.Contract;
using CommonLayer.Models;
using System.IO;

namespace NTierDemoCore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _customer;
        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }
        private string UploadedFile(Customer cust)
        {
            //save image file into folder
            string uniqueFileName = null;
            if (cust.cust_img_File != null)
            {
                string uploadsFolder = Path.GetFullPath("wwwroot") + "\\cust_image";
                uniqueFileName = Guid.NewGuid().ToString() + "_" + cust.cust_img_File.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var FileStream = new FileStream(filePath, FileMode.Create))
                {
                    cust.cust_img_File.CopyTo(FileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Index()
        {
            var customers = _customer.getallCustomers();
            return View(customers);
        }
        public IActionResult Create()
        {
            Customer cus = new Customer();
            cus.Cities = _customer.bindCity();
            return View(cus);
        }

        [HttpPost]
        public IActionResult Create(Customer cusObj)
        {
            string filename=UploadedFile(cusObj);
            bool b = _customer.AddCustomer(cusObj,filename);
            if (b == true)
            {
                TempData["insert"] = "<script>alert('Customer Added SuccessFully!!');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["insert"] = "<script>alert('Customer Failed!!');</script>";
            }
            //ModelState.Clear();  // To Clear all Fields.
            return View();
        }
        public IActionResult Edit(int id)
        {
            var cust = _customer.getsingleCustomer(id);
            return View(cust);
        }

        [HttpPost]
        public IActionResult Edit(Customer cusObj)
        {
            string filename = UploadedFile(cusObj);
            bool b = _customer.UpdateCustomer(cusObj,filename);
            if (b == true)
            {
                TempData["update"] = "<script>alert('Customer Edit SuccessFully!!');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["update"] = "<script>alert('Customer Failed!!');</script>";
            }
            //ModelState.Clear();  // To Clear all Fields.
            return View();
        }

        public IActionResult Delete(int id)
        {
            bool b = _customer.DeleteCustomer(id);
            if (b == true)
            {
                TempData["delete"] = "<script>alert('Customer Deleted SuccessFully!!');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["delete"] = "<script>alert('Customer Failed!!');</script>";
            }
            //ModelState.Clear();  // To Clear all Fields.
            return View();
        }
        public IActionResult Details(int id)
        {
            var cust = _customer.getsingleCustomer(id);
            return View(cust);
        }
    }
}
