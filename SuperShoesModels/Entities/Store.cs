using SuperShoesModels.Prototypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperShoesModels.Entities
{
    public class Store : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Address { get; set; }

        public virtual IList<Article> Articles { get; set; }
    }
}
