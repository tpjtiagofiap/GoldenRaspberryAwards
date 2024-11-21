﻿namespace GoldenRaspberryAwards.Api.Models
{
    public class ProducerInterval
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
