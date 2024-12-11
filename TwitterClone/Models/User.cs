using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Models
{
    public class User
    {
        [DisplayName("User ID")]
        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(10)]
        [Key]
        public string UserId { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username can't be longer than 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression("^[6-9][0-9]{9}$",ErrorMessage = "Invalid Phone Number") ]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(500, ErrorMessage = "Bio can't be longer than 500 characters.")]
        public string Bio { get; set; }

        public string Pic { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 100 characters.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Joined { get; set; }

        [DefaultValue(1)]
        [Range(0, 1, ErrorMessage = "Active status must be either 0 (Inactive) or 1 (Active).")]
        public int Active { get; set; }

        public ICollection<Tweet> Tweet { get; set; }
    }
}
