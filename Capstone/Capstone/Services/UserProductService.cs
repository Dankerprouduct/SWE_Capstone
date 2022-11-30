using Capstone.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capstone.Services
{
	public class UserProductService
	{
		public static UserProductService Instance;
		private List<SavedProduct> _savedProducts;

		private string _filePath = "savedProducts.json";
		
		public UserProductService()
		{
			_savedProducts = new List<SavedProduct>();
			LoadPath();
			Instance = this;
		}

		public List<SavedProduct> Products => _savedProducts;

		private void LoadPath()
		{
			string json = "";
			if (!File.Exists(_filePath))
			{
				File.Create(_filePath);
			}

			using (StreamReader reader = new StreamReader(_filePath))
			{
				json = reader.ReadToEnd();
				reader.Close();
			}

			var result = JsonConvert.DeserializeObject<List<SavedProduct>>(json);
			if (result == null)
				_savedProducts = new List<SavedProduct>();
			else
			{
				_savedProducts = result;
			}
		}


		public void AddItem(SavedProduct product)
		{
			_savedProducts.Add(product);
			Update();
		}

		public void RemoveItem(SavedProduct product)
		{
			_savedProducts.Remove(product);
			Update();
		}

		private void Update()
		{
			var json = JsonConvert.SerializeObject(_savedProducts);

			using (StreamWriter writer = new StreamWriter(_filePath))
			{
				writer.Write(json);
				writer.Close();
			}
		}
	}
}
