using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class UpdateRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code must be at least 3 characters long.")]
    [MaxLength(3, ErrorMessage = "Code cannot exceed 3 characters.")]
    public string Code { get; set; }
    
    [Required] 
    [MaxLength(3, ErrorMessage = "Code cannot exceed 3 characters.")]
    public string Name { get; set; }
    
    public string? RegionImageUrl { get; set; }
}