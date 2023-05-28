using System.ComponentModel.DataAnnotations.Schema;
namespace FarmersAPI.Models;
public class Crop
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }
}