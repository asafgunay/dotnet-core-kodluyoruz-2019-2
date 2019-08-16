using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartMVC.Models
{
    public class Staff
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool Gender { get; set; }
        [Required]
        public string JobTitle { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public static List<Staff> GetSampleData()
        {
            List<Staff> sampleData = new List<Staff>();
            sampleData.Add(new Staff
            {
                Id = 1,
                Name = "Thierry",
                LastName = "Bollore",
                Gender = false,
                JobTitle = "CEO",
                Address = "Nilüfer/Bursa",
                StartDate = Convert.ToDateTime("01.08.2019"),
                BirthDate = Convert.ToDateTime("01.07.1970"),
                Salary = 15000,
                ImageUrl = "https://media.marianne.net/sites/default/files/thierry_bollore.jpg"
            });

            sampleData.Add(new Staff
            {
                Id = 3,
                Name = "Deniz",
                LastName = "Satar",
                Gender = true,
                JobTitle = "Halkla İlişkiler Sorumlusu",
                Address = "ABC cd. EFG ap. 2/27 Yıldırım/Bursa",
                StartDate = Convert.ToDateTime("15.07.2018"),
                BirthDate = Convert.ToDateTime("11.03.1990"),
                Salary = 7000,
                ImageUrl = "https://www.biyografiler.kim/wp-content/uploads/2018/04/Deniz-Satar-sevgilisi-kim.jpg"
            });

            sampleData.Add(new Staff
            {
                Id = 2,
                Name = "Berk",
                LastName = "Çağdaş",
                Gender = false,
                JobTitle = "Satış Pazarlama Müdürü",
                Address = "ABC cd. EFG ap. 2/27 Osmangazi/Bursa",
                StartDate = Convert.ToDateTime("21.05.2015"),
                BirthDate = Convert.ToDateTime("08.01.1980"),
                Salary = 8000,
                ImageUrl = "https://automobilemagazine.com.tr/wp-content/uploads/2019/01/1547189271_1546410314_Berk_Cagdas_Renault_CEO.jpg"
            });
            
            return sampleData;
        }
    }
}
