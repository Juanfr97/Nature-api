using System.ComponentModel.DataAnnotations;

namespace NatureAPI.Entities;

public class Nature
{
    [Key]
    public int Id { get; set; }
    public string Image { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}