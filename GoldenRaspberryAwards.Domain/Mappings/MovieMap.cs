﻿using CsvHelper.Configuration;
using GoldenRaspberryAwards.Core.Helpers;
using GoldenRaspberryAwards.Core.Models;

namespace GoldenRaspberryAwards.Core.Mappings
{
    public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Map(m => m.Year).Name("year");
            Map(m => m.Title).Name("title");
            Map(m => m.Studios).Name("studios");
            Map(m => m.Producers).Name("producers");
            Map(m => m.IsWinner).Name("winner").TypeConverter<YesNoBooleanConverter>(); 
        }
    }
}