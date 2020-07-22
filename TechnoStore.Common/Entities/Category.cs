using System.Collections.Generic;

namespace TechnoStore.Common.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Technic> Technics { get; set; }
        public Category()
        {
            this.Technics = new List<Technic>();
        }
    }
}