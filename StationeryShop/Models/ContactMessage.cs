using System.ComponentModel.DataAnnotations;

namespace StationeryShop.Models
{
    public class ContactMessage
    {
        public int ContactMessageId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 ký tự")]
        [Display(Name = "Tên của bạn")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nội dung tin nhắn")]
        [StringLength(5000, ErrorMessage = "Nội dung không được vượt quá 5000 ký tự")]
        [Display(Name = "Nội dung tin nhắn")]
        public string Message { get; set; } = string.Empty;

        public DateTime MessageSent { get; set; }
    }
}
