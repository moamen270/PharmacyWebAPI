namespace PharmacyWebAPI.Models.Dto
{
    public class UserRolesDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public List<RoleDto>? Roles { get; set; }
    }
}