﻿namespace GoldenRaspberryAwards.Api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Studios { get; set; }
        public int Year { get; set; }
        public bool IsWinner { get; set; }
        public string Producers { get; set; }
    }
}
