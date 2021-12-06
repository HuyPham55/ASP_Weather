using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Models
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
    }
}