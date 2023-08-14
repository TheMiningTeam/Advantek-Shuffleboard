#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shuffleboard.Data;
using Shuffleboard.Models;
using System.Diagnostics;

namespace Shuffleboard.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Home
    public IActionResult Index()
    {
        try
        {
            var matchRed = _db.Player_Matches
                .Where(x => x.Color == "RED")
                .Select(x => new Player_Match
                {
                    Id = x.Id,
                    PlayerId = x.PlayerId,
                    MatchId = x.MatchId,
                    Score = x.Score
                })
                .OrderBy(x => x.Id)
                .Last();

            var matchBlue = _db.Player_Matches
                .Where(x => x.Color == "BLUE")
                .Select(x => new Player_Match
                {
                    Id = x.Id,
                    PlayerId = x.PlayerId,
                    Score = x.Score
                })
                .OrderBy(x => x.Id)
                .Last();


            var viewModel = new LatestMatch()
            {
                RedPlayer = _db.Players.First(x => x.Id == matchRed.PlayerId).FirstName,
                RedScore = matchRed.Score,
                BluePlayer = _db.Players.First(x => x.Id == matchBlue.PlayerId).FirstName,
                BlueScore = matchBlue.Score,
                MatchDate = _db.Matches.First(x => x.Id == matchRed.MatchId).StartTime.ToString("g"),
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            ViewBag.Exception = ex;
            return View();
        }
    }

    // GET: Home/AFK
    public IActionResult AFK()
    {
        try
        {
            var bestPlayer = _db.Players
                .Where(x => x.Deleted == false)
                .Select(x => new Player { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Nick = x.Nick })
                .First(x => x.Id == _db.Players
                    .Where(x => x.Deleted == false)
                    .OrderByDescending(m => m.Won)
                    .First().Id);

            var dataModel = new BestPlayer()
            {
                FirstName = bestPlayer.FirstName,
                LastName = bestPlayer.LastName,
                Nick = bestPlayer.Nick,
            };

            return View(dataModel);
        }
        catch (Exception ex)
        {
            ViewBag.Exception = ex;
            return View();
        }
    }

    // GET: Home/Leaderboard
    public async Task<IActionResult> Leaderboard(string sortOrder)
    {
        ViewData["WonSortParm"] = String.IsNullOrEmpty(sortOrder) ? "won_rev" : "";
        ViewData["LostSortParm"] = sortOrder == "lost" ? "lost_rev" : "lost";
        ViewData["ScoreSortParm"] = sortOrder == "score" ? "score_rev" : "score";


        var players = _db.Players
            .Where(x => x.Deleted == false)
            .Select(x => new Player
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nick = x.Nick,
                Won = x.Won,
                Lost = x.Lost,
                TotalScore = x.TotalScore,
                FavTeam = x.FavTeam
            });

        switch (sortOrder)
        {
            case "lost_rev":
                ViewData["SortAfter"] = "Lost";
                players = players.OrderBy(x => x.Lost);
                break;
            case "lost":
                ViewData["SortAfter"] = "Lost";
                players = players.OrderByDescending(x => x.Lost);
                break;
            case "score_rev":
                ViewData["SortAfter"] = "Score";
                players = players.OrderBy(x => x.TotalScore);
                break;
            case "score":
                ViewData["SortAfter"] = "Score";
                players = players.OrderByDescending(x => x.TotalScore);
                break;
            case "won_rev":
                ViewData["SortAfter"] = "Won";
                players = players.OrderBy(x => x.Won);
                break;
            default:
                ViewData["SortAfter"] = "Won";
                players = players.OrderByDescending(x => x.Won);
                break;
        }
        return View(await players.AsNoTracking().ToListAsync());

    }

    // GET: Home/StatsRefresh
    public async Task<IActionResult> StatsRefresh()
    {
        List<Player> players = await _db.Players
            .Where(x => x.Deleted == false)
            /* If possible, implement this. Problem is that update() **needs** access to FirstName, etc. to update.
            .Select(x => new Player
            {
                Id = x.Id,
                Won = x.Won,
                Lost = x.Lost,
                TotalScore = x.TotalScore,
                RedTeam = x.RedTeam,
                BlueTeam = x.BlueTeam,
            })*/
            .ToListAsync();

        List<Player_Match> matches = await _db.Player_Matches.AsNoTracking().ToListAsync();

        // Resets all players' stats
        foreach (Player player in players)
        {
            player.Won = 0;
            player.Lost = 0;
            player.TotalScore = 0;
            player.RedTeam = 0;
            player.BlueTeam = 0;
        }

        // Iterate through ALL matches and append stats to each user.
        foreach (Player_Match match in matches)
        {
            if (match.Won)
            {
                players.First(x => x.Id == match.PlayerId).Won += 1;
            }

            else if (!match.Won)
            {
                players.First(x => x.Id == match.PlayerId).Lost += 1;
            }

            if (match.Color == "RED")
            {
                players.First(x => x.Id == match.PlayerId).RedTeam += 1;
            }
            else if (match.Color == "BLUE")
            {
                players.First(x => x.Id == match.PlayerId).BlueTeam += 1;
            }

            players.First(x => x.Id == match.PlayerId).TotalScore += match.Score;
        }

        foreach (Player player in players)
        {
            if (player.RedTeam > player.BlueTeam)
            {
                player.FavTeam = "RED";
            }
            else if (player.RedTeam < player.BlueTeam)
            {
                player.FavTeam = "BLUE";
            }
            else
            {
                player.FavTeam = "NONE";
            }

            // Needs access to all player columns
            _db.Update(player);
        }

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Leaderboard));
    }

    public IActionResult NoScript()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}