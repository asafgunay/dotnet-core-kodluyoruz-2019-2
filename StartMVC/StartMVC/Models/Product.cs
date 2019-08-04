using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public static List<Product> GetSampleData()
        {
            var urun1 = new Product
            {
                Id = 1,
                ImageUrl = "https://productimages.hepsiburada.net/s/27/280-413/10182153306162.jpg",
                Name = "3'lü Bistro Set Balkon Bahçe Mobilya Takımı Masif Akasya Ağacı 2 Sandalye 1 Masa",
                Price = 399.90
            };
            var urun2 = new Product
            {
                Id = 2,
                ImageUrl = "https://productimages.hepsiburada.net/s/26/280-413/10147813195826.jpg",
                Name = "Sallanır 2 Pozisyonlu Şezlong",
                Price = 213.12
            };
            var urun3 = new Product
            {
                Id = 3,
                ImageUrl = "https://productimages.hepsiburada.net/s/1/280-413/9529689636914.jpg",
                Name = "Düz Krem Plaj Şemsiyesi",
                Price = 210.14
            };
            List<Product> sampleData = new List<Product>();
            sampleData.Add(urun1);
            sampleData.Add(urun2);
            sampleData.Add(urun3);
            return sampleData;
        }
    }
}
