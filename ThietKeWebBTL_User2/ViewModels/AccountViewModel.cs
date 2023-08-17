using ThietKeWebBTL_User2.Models;

namespace ThietKeWebBTL_User2.ViewModels
{
	public class AccountViewModel
	{
        public string? Password { get; set; }

        public string? Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }

        public string Email { get; set; } = null!;

        public long? RoleId { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public IFormFile Image { get; set; }
        public virtual Role? Role { get; set; }
    }
}
