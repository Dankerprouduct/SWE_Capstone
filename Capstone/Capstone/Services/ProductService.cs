using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.Services
{
    public class ProductService
    {
        public static ProductService Instance { get; set; }
	    
        private BuggyDbContext _dbContext;

        public ProductService(BuggyDbContext context)
        {
	        Instance = this;
	        _dbContext = context; 
        }

        public async Task<Product> AddProductAsync(Product product)
        {
	        try
	        {
		        product.Id = _dbContext.Products.Count() + 1;
				
		        _dbContext.Products.Add(product);
		        await _dbContext.SaveChangesAsync();
	        }
	        catch (Exception ex)
	        {
		        throw;
	        }

	        return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
	        try
	        {
		        var productExists = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
		        if (productExists != null)
		        {
			        _dbContext.Update(product);
			        await _dbContext.SaveChangesAsync();
		        }
	        }
	        catch (Exception ex)
	        {
		        throw;
	        }

			return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
	        try
	        {
		        _dbContext.Products.Remove(product);
		        await _dbContext.SaveChangesAsync();
	        }
	        catch (Exception ex)
	        {
		        throw;
	        }
        }

    }
}
