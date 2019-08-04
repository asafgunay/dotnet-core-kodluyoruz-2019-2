using System;
using ConsoleApp.Models;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //// string metinsel veri tipi
            //string hi = "hello world!2";
            //var hi2 = "hello bursa";
            //decimal sayi = 5;
            //decimal sayi2 = 2;
            //double bolum = (double)(sayi / sayi2);
            //Console.WriteLine(bolum);


            // deger atama ile klasik yolla olusturuldu
            Bike bike = new Bike();
            bike.Brand = "Yamaha";
            bike.ModelName = "RZ255";
            bike.Tires = 2;
            bike.Price = 12.39;
            bike.HasBasket = true;

            // constructor method ile olusturuldu
            Car car = new Car("Fiat","Punto",4,500.55,false);

            // scope ile kisayolla olusturuldu
            Car car2 = new Car
            {
                Brand = "Honda",
                ModelName = "Civic",
                Price = 909.13,
                Tires = 4,
                HasSunroof = true
            };
            Bike bike2 = new Bike("Honda", "AZ7899", 50.33, true);

            string sunroofDurumu = "";
            if (car.HasSunroof == true)
            {
                sunroofDurumu = "Var";
            }
            else
            {
                sunroofDurumu = "Yok";
            }
            Console.WriteLine("Araba");
            Console.WriteLine("Marka: " + car.Brand);
            Console.WriteLine("Model: " + car.ModelName);
            Console.WriteLine("Teker Sayisi: " + car.Tires);
            Console.WriteLine("Fiyat: " + car.Price);
            Console.WriteLine("Sunroof Var mi? " + sunroofDurumu
                );
            Console.WriteLine("---");

            Console.WriteLine("Araba");
            Console.WriteLine("Marka: " + car2.Brand);
            Console.WriteLine("Model: " + car2.ModelName);
            Console.WriteLine("Teker Sayisi: " + car2.Tires);
            Console.WriteLine("Fiyat: " + car2.Price);
            Console.WriteLine("Sunroof Var mi? " + sunroofDurumu
                );
            Console.WriteLine("---");

            Console.WriteLine("Motorsiklet");
            Console.WriteLine("Marka: " + bike.Brand);
            Console.WriteLine("Model: " + bike.ModelName);
            Console.WriteLine("Teker Sayisi: " + bike.Tires);
            Console.WriteLine("Fiyat: " + bike.Price);
            Console.WriteLine("Sepet Var mi? " +
                (bike.HasBasket ? "Var" : "Yok"));

            Console.WriteLine("---");

            Console.WriteLine("Motorsiklet2");
            Console.WriteLine("Marka: " + bike2.Brand);
            Console.WriteLine("Model: " + bike2.ModelName);
            Console.WriteLine("Teker Sayisi: " + bike2.Tires);
            Console.WriteLine("Fiyat: " + bike2.Price);
            Console.WriteLine("Sepet Var mi? " +
                (bike2.HasBasket ? "Var" : "Yok"));

            Console.ReadLine();
        }
    }
}
