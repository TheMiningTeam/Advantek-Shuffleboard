#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shuffleboard.Data;
using Shuffleboard.Models;

namespace Shuffleboard.Controllers;

public class PlayersController : Controller
{
    private readonly ApplicationDbContext _db;

    public PlayersController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Players
    public async Task<IActionResult> Index()
    {
        return View(await _db.Players.Where(x => x.Deleted == false).ToListAsync());
    }

    // GET: Players/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playerFromDb = _db.Players.Find(id);
        if (playerFromDb == null)
        {
            return NotFound();
        }

        return View(playerFromDb);
    }

    //GET: Players/Create
    public IActionResult Create()
    {
        return View();
    }

    //POST: Players/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Nick")] Player playerToCreate)
    {
        if (!ModelState.IsValid)
        {
            return View(playerToCreate);
        }

        _db.Players.Add(playerToCreate);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    //GET: Players/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }

        var playerFromDb = await _db.Players
            .Where(x => x.Deleted == false)
            .Select(x => new Player
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nick = x.Nick,
            })
            .AsNoTracking()
            .FirstAsync(x => x.Id == id);
        
        if (playerFromDb == null)
        {
            return NotFound();
        }

        return View(playerFromDb);
    }

    //POST: Players/Edit/5
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(int? id)
    {

        if (id == null || id == 0)
        {
            return NotFound();
        }

        Player playerInDb = await _db.Players
            .Where(x => x.Deleted == false && x.Id == id)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (await TryUpdateModelAsync<Player>(playerInDb, "",
            x => x.FirstName,
            x => x.LastName,
            x => x.Nick))
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
        }

        return RedirectToAction(nameof(Index));
    }

    //GET: Players/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var playerFromDb = await _db.Players
            .Where(x => x.Deleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (playerFromDb == null)
        {
            return NotFound();
        }
        else if (playerFromDb.Deleted == true)
        {
            return NotFound();
        }

        return View(playerFromDb);
    }

    //Post: Players/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var playerFromDb = await _db.Players
            .Where(x => x.Deleted == false && x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (playerFromDb == null)
        {
            return NotFound();
        }
        playerFromDb.Deleted = true;

        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
