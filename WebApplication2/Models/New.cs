using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNewsV2.Models
{
    public class New 
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [AllowHtml]
        public string Noticia { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public virtual Client Client { get; set; }
    }

    public class NoticiasViewModel
    {
        //public IEnumerable<New> noticia { get; set; }
        public New noticia { get; set; }
        public IEnumerable<New> ListOfNews { get; set; }
        public SelectList ListOfEmployees { get; set; }
        public SelectList ListOfClients { get; set; }
        public SelectList ListOfDates { get; set; }

        public Employee employee { get; set; }
        public int? clientID { get; set; }
        public int? employeeID { get; set; }
        public DateTime Fecha { get; set; }

    }

}
