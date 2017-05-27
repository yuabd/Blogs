using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;

namespace Studio.Services
{
	public class CustomerService
	{
		SiteDataContext db = new SiteDataContext();
		public void InsertCustomer(Customer customer)
		{
			db.Customers.Add(customer);
		}

		public Customer GetCustomer(string company)
		{
			return db.Customers.Find(company);
		}

		public void UpdateCustomer(Customer customer)
		{
			var c = GetCustomer(customer.CompanyName);

			c.Address = customer.Address;
			c.ContactName = customer.ContactName;
			c.DateCreated = customer.DateCreated;
			c.Email = customer.Email;
			c.Gender = customer.Gender;
			c.IndustryID = customer.IndustryID;
			c.LeadSource = customer.LeadSource;
			c.Phone = customer.Phone;
			c.Status = customer.Status;
			c.Type = customer.Type;
		}

		public void DeleteCustomer(string companyName)
		{
			var customer = GetCustomer(companyName);

			db.Customers.Remove(customer);
		}

		public IQueryable<Customer> GetCustomers()
		{
			return db.Customers.Where(m => m.UserID == GlobalHelper.UserID).OrderByDescending(m => m.DateCreated);
		}

		public IQueryable<Customer> GetCustomers(string type)
		{
			return db.Customers.Where(m => m.Type == type && m.UserID == GlobalHelper.UserID).OrderByDescending(m => m.DateCreated);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		~CustomerService()
		{
			db.Dispose();
		}
	}
}