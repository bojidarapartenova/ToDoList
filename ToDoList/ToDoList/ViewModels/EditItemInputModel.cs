using System.ComponentModel.DataAnnotations;

public class EditItemInputModel
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = null!;
}