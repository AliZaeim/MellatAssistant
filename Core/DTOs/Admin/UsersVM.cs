using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Admin
{
    public class UsersVM
    {
        public List<User> Users { get; set; }
        public string Search { get; set; }
        public string SearchField { get; set; }
        public string OrderField { get; set; }
        public string OrderType { get; set; }
        public int? RecCount { get; set; }
        public int? PageN { get; set; }
    }
}
