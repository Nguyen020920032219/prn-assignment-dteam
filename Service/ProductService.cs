using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        ProductRepository _productRepo;
        public ProductService()
        {
            _productRepo = new ProductRepository();
        }

        public List<Product> GetProducts()
        {
            return _productRepo.GetAll();
        }

        public List<Product> GetProductsContainString(string txtSearch)
        {
            List<Product> products = _productRepo.GetAll();
            List<Product> result = new List<Product>();
            foreach (Product product in products)
            {
                if (product.Name.ToLower().Contains(txtSearch.ToLower()))
                {
                    result.Add(product);
                }
            }
            return result;
        }

        public void DeleteProduct(Product product)
        {
            product.IsDiscontinued = true;
            _productRepo.Update(product);
        }
        public void UpdateProduct(Product product)
        {
            _productRepo.Update(product);
        }

        public void CreateProduct(Product product)
        {
            _productRepo.Add(product);
        }
    }
}
