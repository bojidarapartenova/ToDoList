using System.ComponentModel.DataAnnotations;

public class AddItemInputModel
{
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = null!;
}