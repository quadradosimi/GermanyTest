using System;

namespace Data.Domain.Models
{
    public class DataModel : BaseEntity
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Description{ get; set; }
    }
}