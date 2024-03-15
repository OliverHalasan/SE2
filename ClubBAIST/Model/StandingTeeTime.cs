﻿namespace ClubBAIST.Model
{
    public class StandingTeeTime
    {
        public int StandingID { get; set; }
        public string MemberNumber1 { get; set; } = string.Empty;
        public string MemberNumber2 { get; set; } = string.Empty;
        public string MemberNumber3 { get; set; } = string.Empty;
        public string MemberNumber4 { get; set;} = string.Empty;
        public string name1 { get; set; } = string.Empty;
        public string name2 { get; set; } = string.Empty;
        public string name3 { get; set; } = string.Empty;
        public string name4 { get; set; } = string.Empty;
        public TimeOnly RequestedTeeTime { get; set; }
        public DateOnly RequestedDate {  get; set; }

        public StandingTeeTime()
        {

        }
    }
}
