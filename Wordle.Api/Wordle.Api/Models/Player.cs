using System.ComponentModel.DataAnnotations.Schema;

namespace Wordle.Api.Models;

[Table("Players")]
public class Player
{
    public int PlayerId { get; set; }
    public string Name { get; set; } = null!;
    public int Coins { get; set; }
}