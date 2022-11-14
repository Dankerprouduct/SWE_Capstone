using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Crawlers
{
    public interface IWebCrawler
    {
        public string BaseUrl { get; set; }

        public  Task<List<Product>> SearchProduct(string productName);

        public bool Enabled { get; set; }
    }
}
