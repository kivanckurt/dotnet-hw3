using CORE.APP.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain
{
    /// <summary>
    /// Enum to represent user gender options.
    /// </summary>
    public enum Genders
    {
        NotSpecified,
        Male,
        Female,
        Other
    }


    public class User: Entity
    {


        // --- Core Properties ---
        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; // Note: This should be hashed, not stored in plain text.

        // --- Nullable Personal Details ---
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        public Genders Gender { get; set; }

        public DateTime? BirthDate { get; set; } // Nullable DateTime

        public DateTime RegistrationDate { get; set; }

        public decimal Score { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

    }
}
