﻿namespace UzWorks.Core.DataTransferObjects.FeedBacks;

public class FeedBackEM
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string FullName { get; set; }
    public DateTime DueDate { get; set; }
}
