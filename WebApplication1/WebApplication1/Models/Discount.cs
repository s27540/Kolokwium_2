﻿namespace WebApplication1.Models;

public class Discount
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public decimal Value { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
