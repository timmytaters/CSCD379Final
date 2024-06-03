namespace Wordle.Api.Dtos;

public record WordDateDTO
{
	public string Date { get; set; }
	public int GameCount { get; set; }
	public double AverageAttempts { get; set; }
	public int AverageSeconds{ get; set; }
    //public Array PlayerList{ get; set; }
}