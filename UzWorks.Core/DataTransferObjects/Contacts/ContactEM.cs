﻿namespace UzWorks.Core.DataTransferObjects.Contacts;

public class ContactEM
{
    public Guid Id { get; set; }
    public string Title { get; set; }   
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public bool IsComplated { get; set; }
}