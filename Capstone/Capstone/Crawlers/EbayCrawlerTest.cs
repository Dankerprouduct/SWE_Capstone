using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using HtmlAgilityPack;

namespace Capstone.Crawlers
{
    public class EbayCrawler : IWebCrawler
    {
        public string BaseUrl { get; set; }

        public bool Enabled { get; set; } = true;

		public EbayCrawler()
        {
            BaseUrl = @"https://www.ebay.com/";
        }
        public async Task<List<Product>> SearchProduct(string productName)
        {
            List<Product> result = new List<Product>();

            var query = productName.Replace(' ', '+');
            var url = BaseUrl + $"sch/i.html?_from=R40&_trksid=p2380057.m570.l1313&_nkw={query}";

            var httpClient = new HttpClient();
            var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.UserAgent.Add(productValue);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(response.Content.ReadAsStringAsync().Result);
                var productList = document.DocumentNode.SelectNodes("//div[contains(@class, 's-item__info')]");


                foreach (var product in productList)
                {
	                try
	                {
		                var price = product.ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText;
		                Debug.WriteLine(product.InnerText); //Remove debug later?
		                Debug.WriteLine(price);
		                var prod = new Product();
		                prod.Price = price;
		                prod.Name = product.InnerText.Substring(0, product.InnerText.IndexOf("Opens in a new window"));

		                string currentPrice = prod.Price.Substring(prod.Price.IndexOf('$') + 1);
		                string currentName = prod.Name;
		                if (currentName != "Shop on eBay" && Double.TryParse(currentPrice, out _))
		                {
			                result.Add(prod);
		                }
	                }
	                catch (Exception ex)
	                {
	                }
                }

                Debug.WriteLine(document.Text);     //Remove debug later?
            }

            return result; 
        }

    }
}
