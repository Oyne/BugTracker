﻿namespace BugTracker.API.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Bug> Bugs { get; set; } = new();
    }
}
