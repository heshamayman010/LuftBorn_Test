using System.ComponentModel.DataAnnotations;

namespace Luftborn.Dtos;

public class CategoryAddDto:BaseDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? NameAr { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? NameEn { get; set; }

}