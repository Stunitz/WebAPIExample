using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIExample.BaseRepository;
using WebAPIExample.Models.Data;

namespace WebAPIExample.Controllers
{
    [RoutePrefix("WebAPIExample")]
    public class WebExampleAPIController : ApiController
    {
        #region GET's

        // GET WebAPIExample/Customers
        [Route("Customers")]
        public IEnumerable<Customer> GetCustomers() => BaseRepository<Customer>.ReturnList();

        // GET WebAPIExample/Products
        [Route("Products")]
        public IEnumerable<Product> GetProducts() => BaseRepository<Product>.ReturnList();


        // GET: WebAPIExample/CustomerId/1
        [Route("CustomerId/{customerId}")]
        public Customer GetCustomerId(int customerId) => BaseRepository<Customer>.Search(customerId);

        // GET: WebAPIExample/ProductId/1
        [Route("ProductId/{productId}")]
        public Product GetProductId(int productId) => BaseRepository<Product>.Search(productId);

        #endregion

        #region POST's

        // POST WebAPIExample/Customer
        [Route("Customer")]
        public void PostCustomer([FromBody]Customer customer) => BaseRepository<Customer>.Insert(customer);

        // POST WebAPIExample/Product
        [Route("Product")]
        public void PostProduct([FromBody]Product product) => BaseRepository<Product>.Insert(product);

        // POST WebAPIExample/Purchase
        [Route("Purchase")]
        public void PostPurchase([FromBody]Purchase purchase) => BaseRepository<Purchase>.Insert(purchase);

        #endregion

        #region PUT's

        // PUT WebAPIExample/Customer/5
        [Route("Customer/{customerId}")]
        public void PutCustomer(int customerId, [FromBody]Customer customer)
        {
            Customer returnedCustomer = BaseRepository<Customer>.Search(customerId);

            returnedCustomer.FirstName = customer.FirstName ?? returnedCustomer.FirstName;
            returnedCustomer.LastName = customer.LastName ?? returnedCustomer.LastName;
            returnedCustomer.Email = customer.Email ?? returnedCustomer.Email;
            returnedCustomer.Gender = customer.Gender;

            BaseRepository<Customer>.Update(returnedCustomer);
        }

        // PUT WebAPIExample/Product/5
        [Route("Product/{productId}")]
        public void PutProduct(int productId, [FromBody]Product product)
        {
            Product returnedProduct = BaseRepository<Product>.Search(productId);

            returnedProduct.Name = product.Name ?? returnedProduct.Name;
            returnedProduct.PricePerUnit = product.PricePerUnit;

            BaseRepository<Product>.Update(returnedProduct);
        }

        #endregion

        #region DELETE's

        // DELETE WebAPIExample/Customer/5
        [Route("Customer/{customerId}")]
        public void DeleteCustomer(int customerId)
        {
            Customer customer = BaseRepository<Customer>.Search(customerId);
            BaseRepository<Customer>.Delete(customer);
        }

        // DELETE WebAPIExample/5
        [Route("Product/{productId}")]
        public void DeleteProduct(int productId)
        {
            Product product = BaseRepository<Product>.Search(productId);
            BaseRepository<Product>.Delete(product);
        }

        #endregion
    }
}