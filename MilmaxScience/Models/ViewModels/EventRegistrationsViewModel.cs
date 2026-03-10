using MilmaxScience.Models;
using System.Collections.Generic;

public class EventRegistrationsViewModel
{
    public string EventTitle { get; set; }

    public int MaxParticipants { get; set; }

    public int CurrentParticipants { get; set; }

    public List<Registration> Registrations { get; set; }
}