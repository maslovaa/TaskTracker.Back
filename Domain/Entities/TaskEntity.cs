using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskEntity : IdEntity
    {
        public string Ticket { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string Comment { get; set; }
    }
}
