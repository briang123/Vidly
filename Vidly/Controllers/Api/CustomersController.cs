using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        public readonly ApplicationDbContext Context;

        public CustomersController()
        {
            Context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return Context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto dto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(dto);
            Context.Customers.Add(customer);
            Context.SaveChanges();

            dto.Id = customer.Id;

            return dto;
        }

        // PUT /api/customers/1
        [HttpPut]
        public CustomerDto UpdateCustomer(int id, CustomerDto dto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(dto, customer);
            Context.SaveChanges();

            return dto;
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public CustomerDto DeleteCustomer(int id)
        {
            var customer = Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Customers.Remove(customer);
            Context.SaveChanges();

            return Mapper.Map<Customer, CustomerDto>(customer);

        }
    }
}