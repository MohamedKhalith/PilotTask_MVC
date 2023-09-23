using System;
using System.ComponentModel.DataAnnotations;

namespace PilotTask_MVC.Models.Domain
{
    public class ProfileDetail
    {
        [Key]
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }    
    }
}
