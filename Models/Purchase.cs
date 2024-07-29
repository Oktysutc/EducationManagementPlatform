using EducationManagementPlatform.Models;

public class Purchase
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string CVC { get; set; }
    public string ExpirationDate { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string UserId { get; set; }
    public Course Course { get; set; }
}
