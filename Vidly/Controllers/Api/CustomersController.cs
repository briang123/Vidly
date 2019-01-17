using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = Context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var dtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(dtos);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateCustomer(CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(dto);
            Context.Customers.Add(customer);
            Context.SaveChanges();

            dto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), dto);
        }

        // PUT /api/customers/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            Mapper.Map(dto, customer);
            Context.SaveChanges();

            return Ok(dto);
        }

        // DELETE /api/customers/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            Context.Customers.Remove(customer);
            Context.SaveChanges();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));

        }
    }
}