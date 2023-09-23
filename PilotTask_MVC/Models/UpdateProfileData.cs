using System;

namespace PilotTask_MVC.Models
{
    public class UpdateProfileData
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
    }
}
