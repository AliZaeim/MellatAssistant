using System;

namespace Core.DTOs.Admin
{
    public class LastAnyStatusVM
    {
        public int StatusId { get; set; }
        public string InsStatusText { get; set; }
        public bool IsEndOfProcess { get; set; }
        public bool IsDefualt { get; set; }
        public bool IsSystemic { get; set; }
        public DateTime RegLastStatusDate { get; set; }

    }
}
