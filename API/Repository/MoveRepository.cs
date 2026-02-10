using API.Data;
using API.Models.Static;
using API.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class MoveRepository : IMoveRepository
{
    private readonly ApplicationDbContext _context;

    public MoveRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Move> GetAll()
    {
        var moves = _context.Move
            .Include(x => x.PkmnType)
            .Include(x => x.DamageClass)
            .Include(x => x.MoveEffect)
            .AsNoTracking()
            .ToList();

        return moves;
    }

    public List<Move>? GetMovesByPokemonId(int pokemonId)
    {
        var pokemon = _context.Pokemon
            .Include(x => x.Moves)
                .ThenInclude(x => x.PkmnType)
            .Include(x => x.Moves)
                .ThenInclude(x => x.DamageClass)
            .Include(x => x.Moves)
                .ThenInclude(x => x.MoveEffect)
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == pokemonId);

        if (pokemon == null)
        {
            return null;
        }

        var moves = pokemon.Moves;

        return moves;
    }
}