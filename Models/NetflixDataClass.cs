using System;
using System.Collections.Generic;

namespace NetflixAPISolution.Models
{
    public class NetflixData
    {
        public int Id { get; set; }
        public string? title { get; set; } = null;
        public string? rating { get; set; } = null;
        public string? ratingLevel { get; set; }
        public int ratingDescription { get; set; }
        public int releaseyear { get; set; }
        public int userratingscore { get; set; }
        public int userratingsize { get; set; }

        //NetflixExcel ex = new NetflixExcel(@"C:\Users\zwane\Downloads\netflix.xlsx", 1);//netflix.xlsx"
    }
}


