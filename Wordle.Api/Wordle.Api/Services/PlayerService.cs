using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Wordle.Api.Dtos;
using Wordle.Api.Models;

namespace Wordle.Api.Services;

public class PlayerService(WordleDbContext Db)
{

    public async Task<Player?> GetPlayer(string playerName)
    {
        return await Db.Players.FirstOrDefaultAsync(p => p.Name == playerName);
    }

    public async Task AddPlayer(PlayerDTO playerDto)
    {
	    Player? existingPlayer = Db.Players.FirstOrDefault(player => player.Name == playerDto.Name);

	    if (existingPlayer != null)
	    {//Name, GameCount, AverageAttempts, AverageSecondsPerGame
			Db.Players.Entry(existingPlayer).CurrentValues.SetValues(playerDto);
	    }
	    else
	    {
		    Player player = new()
		    {
			    Name = playerDto.Name,
			    Coins = playerDto.Coins,
		    };
		    await Db.Players.AddAsync(player);
	    }
	    
	    await Db.SaveChangesAsync();

    }
    
    public async Task<Player[]> GetTopPlayers(int numberOfPlayers)
    {
        return await Db.Players.OrderBy(p => p.Coins).Take(numberOfPlayers).ToArrayAsync();
    }
}