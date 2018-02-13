using System.ComponentModel.DataAnnotations;

namespace SuperShoesModels.Prototypes
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
