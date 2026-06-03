using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "ФИО")]
    public string FullName { get; set; }

    [Required]
    [Display(Name = "Дата рождения")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}

