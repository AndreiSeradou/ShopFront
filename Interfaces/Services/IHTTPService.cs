using ShopFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFront.Interfaces.Services
{
    public interface IHTTPService
    {
        List<Product> GetProducts();
        bool CreateProduct(string name, string imageUrl);
        bool UpdateProduct(string newName, string newImageUrl, string oldName, string oldImageUrl);
        bool DeleteProduct(string name, string imageUrl);
    }
}
