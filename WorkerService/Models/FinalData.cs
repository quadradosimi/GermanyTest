using System.ComponentModel.DataAnnotations;

namespace ApiHangFire.Models
{
    public class FinalData
    {
        public int Id { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
