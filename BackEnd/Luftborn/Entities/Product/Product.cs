using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Entities;

public class Product:BaseEntity
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public int Quantity { get; set; }

    public decimal Price { set; get; }
    public string BarCode { get; set; }

    public string? NotesAr { get; set; }
    public string? NotesEn { get; set; }
    public int CategoryId { get; set; }
    public virtual Category category { get; set; }



}