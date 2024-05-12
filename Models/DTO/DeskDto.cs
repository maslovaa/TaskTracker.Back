using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DeskDto
    {
        /// <summary>
        /// Идентификатор доски
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Наименование доски
        /// </summary>
        public string Name { get; set; }
    }
}
