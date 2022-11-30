using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.IO;

namespace Capstone.Crawlers
{
    public class TargetCrawler: IWebCrawler
    {
        public string BaseUrl { get; set; }



        public bool Enabled { get; set; } = false;

        public TargetCrawler()
        {
            BaseUrl = @"https://www.target.com/";
        }
        public async Task<List<Product>> SearchProduct(string productName)
        {
            List<Product> result = new List<Product>();
            var query = productName.Replace(' ', '+');
            var url = BaseUrl + $"s?searchTerm={query}";

            var httpClient = new HttpClient();
            var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.UserAgent.Add(productValue);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsByteArrayAsync();

                var s = Decompress(content.Result);
                var pageHtml = Encoding.Default.GetString(s);

                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();

                htmlDocument.LoadHtml(pageHtml);
                var productList = htmlDocument.DocumentNode.SelectNodes("//div[contains(@data-component-type, 's-search-result')]");


                foreach (var product in productList)
                {

                    Debug.WriteLine(product.InnerText + "\n\n");


                    var split = product.InnerText.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                    var title = split[0];
                    var priceRegex = "\\$(\\d{1,}.\\d{1,2})";

                    var productObj = new Product()
                    {
                        Name = title,
                        Price = new Regex(priceRegex).Match(product.InnerText).Value
                    };

                   
                    Debug.WriteLine(productObj.Name);
                    result.Add(productObj);
                }


            }
            else
            {
                Console.WriteLine("here");
                Debug.WriteLine($"search for {productName} failed!");
            }

            return result;

        }


        static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        
    }
    }
}
