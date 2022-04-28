namespace BLL.Entities
{
    /// <summary>View model of class User.</summary>
    public class UserViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? RoleName { get; set; }

    }
}
