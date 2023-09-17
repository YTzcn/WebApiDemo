namespace Yahya.WebApiDemo.Entities
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
