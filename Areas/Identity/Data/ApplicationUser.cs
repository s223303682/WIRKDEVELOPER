using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage ="Required")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Required")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Required")]
    
    public int number { get; set; }

    [Required]
    public string Role  { get; set; }   


}

