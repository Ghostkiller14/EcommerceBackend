
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

public class UserRegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public string Address {get;set;} = string.Empty;
        public int Age {get;set;}

         public bool IsAdmin = false;
        public bool IsBanned = false;

    }
