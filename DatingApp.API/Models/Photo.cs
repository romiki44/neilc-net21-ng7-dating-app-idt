using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string  Description { get; set; }
        public DateTime dateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public bool IsApproved { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}