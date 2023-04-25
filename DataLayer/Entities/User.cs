using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public int? StudentId { get; set; } //null daca rolul este profesor
        public Student Student { get; set; }
    }
}
