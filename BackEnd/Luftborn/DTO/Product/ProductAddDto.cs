using System.ComponentModel.DataAnnotations;
using Luftborn.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Dtos;

public class ProductAddDto:BaseDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string NameAr { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string NameEn { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, 999999.99)]
    public decimal Price { set; get; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string BarCode { get; set; }

    public string? NotesAr { get; set; }
    public string? NotesEn { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? CategoryId { get; set; }

}