using Newtonsoft.Json;
using ShopFront.Interfaces.Services;
using ShopFront.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ShopFront.Services
{
    public class HTTPService : IHTTPService
    {
        private readonly string _baseUrl = " http://localhost:5044/Shop";

        public bool CreateProduct(string name, string imageUrl)
        {
            var request = WebRequest.Create(_baseUrl + $"?url={imageUrl}&name={name}");

            request.Method = "POST";
 
            var response = request.GetResponse();
            var respStream = response.GetResponseStream();
            var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            response.Close();
            respStream.Close();
            reader.Close();

            if (data == "true")
            {
                return true;
            }

            return false;
        }

        public bool DeleteProduct(string name, string imageUrl)
        {
            var request = WebRequest.Create(_baseUrl + $"?url={imageUrl}&name={name}");

            request.Method = "DELETE";

            var response = request.GetResponse();
            var respStream = response.GetResponseStream();
            var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            response.Close();
            respStream.Close();
            reader.Close();

            if (data == "true")
            {
                return true;
            }

            return false;
        }

        public List<Product> GetProducts()
        {
            var request = WebRequest.Create(_baseUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);

            var data = reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<List<Product>>(data);

            webResponse.Close();
            webStream.Close();
            reader.Close();

            return result;
        }

        public bool UpdateProduct(string newName, string newImageUrl, string oldName, string oldImageUrl)
        {
            var request = WebRequest.Create(_baseUrl + $"?newUrl={newImageUrl}&newName={newName}&oldUrl={oldImageUrl}&oldName={oldName}");
            request.Method = "PUT";

            var response = request.GetResponse();
            var respStream = response.GetResponseStream();
            var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            response.Close();
            respStream.Close();
            reader.Close();

            if (data == "true")
            {
                return true;
            }

            return false;
        }
    }
}
