using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity()
        {

        }

        public ProductEntity(int id, string categoryName, decimal price, string name, int categoryId)
        {
            Id = id;
            CategoryName = categoryName;
            Price = price;
            Name = name;
            CategoryId = categoryId;
        }




        //public ProductEntity(int id, string name, decimal price, int categoryId)
        //{
        //    Id = id;
        //    CategoryName = name;
        //    Price = price;
        //    CategoryId = categoryId;
        //}

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string CategoryName { get; set; }
        
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
