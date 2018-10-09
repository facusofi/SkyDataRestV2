using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDataRestV2.Models
{
    public class Request
    {
        public string code { get; set; }
        public int vid { get; set; }
        public string license_plane { get; set; }
        public decimal lon { get; set; }
        public decimal lat { get; set; }
        public string valid_position { get; set; }
        public DateTime event_time { get; set; }
        public decimal kmph { get; set; }
        public string placeneme { get; set; }
        public string client_id { get; set; }
        public decimal head { get; set; }
        public decimal sys_odometer { get; set; }
        public decimal sys_hourmeter { get; set; }
    }
}