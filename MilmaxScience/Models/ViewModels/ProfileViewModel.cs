using MilmaxScience.Models;
using System.Collections.Generic;

public class ProfileViewModel
{
    public ApplicationUser User { get; set; }
    public List<Registration> Registrations { get; set; }
}