global using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PharmacyWebAPI.Models
{
    public class User : IdentityUser
    {
        public int PublicId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ProfilePictureURL { get; set; } = string.Empty;
    }
}