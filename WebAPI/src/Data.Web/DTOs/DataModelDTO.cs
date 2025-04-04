using System.ComponentModel.DataAnnotations;

namespace Data.Web.DTOs
{
    public class DataModelDTO
    {
        public int Id { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Description{ get; set; }
    }
}