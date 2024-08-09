namespace DessertsAPI.Models;

public class Dessert
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public double Price { get; set; }
}
