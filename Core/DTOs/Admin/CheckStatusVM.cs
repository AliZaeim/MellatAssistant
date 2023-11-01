using System;
using System.Collections.Generic;

namespace Core.DTOs.Admin
{
    public class CheckStatusVM<T>
    {
        public bool Exist { get; set; }
        public int RecordCount { get; set; }        
        public List<T> Records { get; set; }
        public string Message { get; set; }
    }
}
