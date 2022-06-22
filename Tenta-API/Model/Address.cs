namespace Tenta_API.Model
{
  public class Address
  {
    public int Id { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? Zipcode { get; set; }
    public string? City { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
  }
}