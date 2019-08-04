using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    public class Bike : Vehicle
    {
        public Bike()
        {
            // bos constructor
        }
        public Bike(string brand, string modelName,  double price, bool hasBasket)
        {
            Brand = brand;
            ModelName = modelName;
            Tires = 2;
            Price = price;
            HasBasket = hasBasket;
        }
        public bool HasBasket { get; set; }
    }
}
