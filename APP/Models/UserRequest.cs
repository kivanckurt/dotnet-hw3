using APP.Domain;
using CORE.APP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    /// <summary>
    /// DTO for creating or updating a user.
    /// Used for incoming API requests.
    /// </summary>
    public class UserRequest : Request
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Password must be at least 8 characters.", MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        public Genders Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }


        public bool IsActive { get; set; }


    }
}
