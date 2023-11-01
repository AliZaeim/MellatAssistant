using System;

namespace Core.DTOs.Admin
{
    public class StatusCommentOperationResultVM
    {
        public Guid InsId { get; set; }
        public string IsSuccess { get; set; }
        public string InsType { get; set; }
        public string RefreshElId { get; set; }
        public string StatusType { get; set; }
        public string RefreshPage { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
    }
}
