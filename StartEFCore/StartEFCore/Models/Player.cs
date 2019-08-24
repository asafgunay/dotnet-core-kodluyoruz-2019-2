using StartEFCore.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.Models
{
    public class Player : Entity
    {
        [Required]
        public string LongName { get; set; }
        public int? Age { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public virtual int? TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

    }
}
