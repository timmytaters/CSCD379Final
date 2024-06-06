namespace Wordle.Api.Dtos;

public record PlayerDTO
{
	public string Name { get; set; }
	public int Coins { get; set; }
}