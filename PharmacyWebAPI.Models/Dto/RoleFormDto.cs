using System.ComponentModel.DataAnnotations;

namespace PharmacyWebAPI.Models.ViewModels
{
    public class RoleFormDto
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}