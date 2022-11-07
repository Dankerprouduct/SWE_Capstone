using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Product
    {
        public string Name { get; set; }        //The name of each product
        public string Description { get; set; } //unused?
        public string Price { get; set; }       //The price of each product (USD, don't worry about other currencies)
        public string Retailer { get; set; }    //The name of the retailer where each of the products was scraped from
        public byte[] Image { get; set; }       //The image that show up next to each of the products in search results
        public string ExternalLink { get; set; }//The link to the retailer's listing page where each item is sold
    }
}
