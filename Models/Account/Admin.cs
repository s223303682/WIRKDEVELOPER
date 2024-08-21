using System.ComponentModel.DataAnnotations;
using WIRKDEVELOPER.Areas.Identity.Data;

namespace WIRKDEVELOPER.Models.Account
{
    public class Admin
    {
        [Key]
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
