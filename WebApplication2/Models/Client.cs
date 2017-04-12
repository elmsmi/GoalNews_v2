using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoalNewsV2.Models
{
    public class Client
    {

        public Client()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int ClientID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo es obligatorio")]
        [MaxLength(100)]
        public string ClientName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<New> News { get; set; }

    }

}