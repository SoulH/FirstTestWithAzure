using SuperShoes.Models.Protoypes;
using System.ComponentModel.DataAnnotations;

namespace SuperShoes.Models.Entities
{
    public class Article : Entity
    {
        public int StoreId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Total_in_shelf { get; set; }

        public int Total_in_vault { get; set; }

        public virtual Store Store { get; set; }
    }
}