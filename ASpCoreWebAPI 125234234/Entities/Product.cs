using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspWebAPI
{
    [Table(name:"Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] image { get; set; }
        public int IdCategory { get; set; }
        public int IdSubCategory { get; set; }
    }
}
