using System.ComponentModel.DataAnnotations;

namespace StationeryShop.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public IList<string> Roles { get; set; }
    }
}
