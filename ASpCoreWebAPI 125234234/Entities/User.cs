namespace AspWebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class User
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}