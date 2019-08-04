using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    public class Car : Vehicle
    {
        public Car()
        {
            // bos constructor method
        }
        public Car(string brand, string modelName, int tires, double price, bool hasSunroof)
        {
            // constructor method
            Brand = brand;
            ModelName = modelName;
            Tires = tires;
            Price = price;
            HasSunroof = hasSunroof;
        }
        public bool HasSunroof { get; set; }
    }
}
