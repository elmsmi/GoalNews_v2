using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNewsV2.Models
{
    public class Employee
    {
        public Employee()
        {
            Clients = new HashSet<Client>();
        }


        [Key]
        public int EmployeeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo es obligatorio")]
        public string EmployeeName { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }

    public class EmpCliViewModel
    {
        public Employee employee { get; set; }

        public MultiSelectList ListOfClients { get; set; }

        public IEnumerable<int> SelectedClients { get; set; }

    }

}