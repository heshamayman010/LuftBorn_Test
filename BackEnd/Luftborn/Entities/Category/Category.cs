namespace Luftborn.Entities;

public class Category:BaseEntity
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}