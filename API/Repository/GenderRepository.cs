using API.Data;
using API.Models.Static;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class GenderRepository : IGenderRepository
{
    private readonly ApplicationDbContext _context;
    
    public GenderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Gender> GetAll()
    {
        var genders = _context.Gender
            .AsNoTracking()
            .ToList();

        return genders;
    }

    public Gender? GetById(int id)
    {
        var gender = _context.Gender
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        return gender;
    }
}