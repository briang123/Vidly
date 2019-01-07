using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ViewResult Index()
        {
            IEnumerable<Customer> customers = GetCustomers();
            return View(customers);
        }

        public ActionResult Details(int Id)
        {
            Customer customer = GetCustomers().SingleOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound($"A customer with the ID of {Id} was not found.");
            }

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Id=1, Name = "John Smith"},
                new Customer {Id=2, Name = "Mary Williams"}
            };
        }
    }
}