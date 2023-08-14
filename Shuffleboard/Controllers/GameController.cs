using Microsoft.AspNetCore.Mvc;
using Shuffleboard.Data;
using Shuffleboard.Models;
using System.Globalization;

namespace Shuffleboard.Controllers;

public class GameController : Controller
{
    private readonly ApplicationDbContext _db;

    public GameController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Game
    public IActionResult Index()
    {
        IEnumerable<Player> objPlayerList = _db.Players
            .Where(x => x.Deleted == false)
            .Select(x => new Player
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nick = x.Nick
            });

        return View(objPlayerList);
    }

    // GET: Game/Play
    public IActionResult Play()
    {
        return View();
    }

    // POST: Game/Play
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Play(Match obj)
    {
        ModelState.Remove("MatchId"); // NOTE: It is unknown why this FK is here. Preferably remove it within "Models/Player.cs" if possible
        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _db.Matches.Add(obj);
        _db.SaveChanges();

        return RedirectToAction("Results");
    }

    // GET: Game/Results
    public IActionResult Results()
    {
        ViewBag.MatchId = _db.Matches.OrderBy(x => x.Id).Last().Id;
        return View();
    }

    // POST: Game/Results
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Results(Player_Match obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _db.Player_Matches.Add(obj);
        _db.SaveChanges();

        return RedirectToAction("Results");
    }
}
