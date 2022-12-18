using System.ComponentModel.DataAnnotations;

namespace HiringManagerAPI.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfInterview { get; set; }

        [Required]
        public bool OfferJob { get; set; }
        public string Remarks { get; set; }


    }
}
