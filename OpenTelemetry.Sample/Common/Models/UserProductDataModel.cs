namespace Common.Models;

public class UserProductDataModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<Product> ProductDataModels { get; set; } = new List<Product>();
}
