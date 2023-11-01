using DataLayer.Entities.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class AddRoleVM
    {
        public int UserId { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? RoleId { get; set; }
        public string UserFullName { get; set; }

        public List<Role> Roles { get; set; }

    }
}
