﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanttApplication.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Progress { get; set; }
        public int? ParentId { get; set; }
        public string Type { get; set; }
        public int SortOrder { get; set; }
    }
}