using System;
namespace Xpenser.Models
{
    public class ReportInput
    {
        public long MainId
        { get; set; }
        public int IntMainId
        { get; set; }
        public long SubId
        { get; set; }
        public DateTime StartDate
        { get; set; }
        public DateTime EndDate
        { get; set; }
        public bool BooleanCheck
        { get; set; }
    }
}
