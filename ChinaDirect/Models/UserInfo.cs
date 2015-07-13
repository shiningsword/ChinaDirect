using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChinaDirect.Models
{
    public class UserInfo
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Zip { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Wechat { get; set; }
        //[Key, ForeignKey("User")]
        [Key]
        public string UserId { get; set; }

        public virtual List<Request> requests { get; set; }
    }
}