using System.ComponentModel.DataAnnotations;
using Luftborn.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Dtos;

public class ProductReturnDto:BaseDto
{
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }
    public int Quantity { get; set; }

    public decimal Price { set; get; }
    public string? BarCode { get; set; }

    public string? NotesAr { get; set; }
    public string? NotesEn { get; set; }
    public int? CategoryId { get; set; }
    public string CategoryNameAr { get; set; }
    public string CategoryNameEn { get; set; }




}