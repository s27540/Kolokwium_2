﻿namespace WebApplication1.DTOs;

public class ClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<SubscriptionDto> Subscriptions { get; set; }
}
