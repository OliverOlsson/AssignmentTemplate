namespace WebApi.Models
{
    public class CreateProductModel
    {
        public CreateProductModel()
        {
        }

        public CreateProductModel(string categoryName, string name, decimal price, int categoryId)
        {
            CategoryName = categoryName;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }


        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
