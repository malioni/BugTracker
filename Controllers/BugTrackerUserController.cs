using Microsoft.AspNetCore.Mvc;
using BugTracker.Services.Interfaces;
using System;
using BugTracker.Model;

namespace BugTracker.Controllers;

[ApiController]
[Route("user/[controller]")]
public class BugTrackerUserController : ControllerBase
{
    private readonly IBugRepo _bugRepo;
    private readonly IUserRepo _userRepo;
    private readonly IInteractionRepo _interactionRepo;

    public BugTrackerUserController(IBugRepo bugRepo,
        IUserRepo userRepo,
        IStaffRepo staffRepo,
        IInteractionRepo interactionRepo,
        INoteRepo noteRepo)
    {
        _bugRepo = bugRepo;
        _userRepo = userRepo;
        _interactionRepo = interactionRepo;
    }

    [HttpPost("CreateBugTicket")]
    public async Task<IActionResult> CreateTicket(CreationInput inp)
    {
        var user = await CheckIfNameOrID(inp.NameOrID);
        if (user == null)
        {
            user = new User();
            user.UserName = inp.NameOrID;
            await _userRepo.Add(user);
        }
        var bug = new Bug();
        bug.Description = inp.Text;
        bug.UserID = user.UserID;
        bug.Status = "New";
        bug.DateReported = DateOnly.FromDateTime(DateTime.Now);
        await _bugRepo.Add(bug);
        await AddInteraction(bug.BugID, user.UserID, "Ticket created.");
        return Ok($"Ticket has been created with ticked ID: {bug.BugID}");
    }

    [HttpGet("GetAllBugsForUser")]
    public async Task<ActionResult<IEnumerable<Bug>>> GetBugStatusUserName(string userName)
    {
        var user = await _userRepo.GetByName(userName);
        if (user != null)
            return Ok(await _bugRepo.GetByUserID(user.UserID));
        else
            return NotFound(Enumerable.Empty<Bug>());
    }

    [HttpGet("GetBugStatusID")]
    public async Task<ActionResult<Bug>> GetBugStatusID(int bugID)
    {
        var bug = await _bugRepo.GetByID(bugID);
        if (bug != null)
            return Ok(bug);
        else
            return NotFound($"Ticket not found with ID: {bugID}.");
    }

    [HttpPut("CloseBugTicket")]
    public async Task<IActionResult> CloseTicket(int bugID)
    {
        var bug = await _bugRepo.GetByID(bugID);
        if (bug != null)
        {
            bug.Status = "Closed";
            await _bugRepo.Update(bug);
            await AddInteraction(bug.BugID, bug.UserID, "Ticket closed.");
            return Ok("Ticket has been closed.");
        }
        else
        {
            return NotFound($"Bug with ID {bugID} not found.");
        }

    }

    private async Task<User> CheckIfNameOrID(string name)
    {
        var user = new User();
        if (int.TryParse(name, out int userID))
        {
            user = await _userRepo.GetByID(userID);
        }
        else
        {
            user = await _userRepo.GetByName(name);
        }
        return user;
    }

    private async Task AddInteraction(int bugID, int userID, string text)
    {
        var interaction = new Interaction();
        interaction.BugID = bugID;
        interaction.UserID = userID;
        interaction.InteractionText = text;
        interaction.DateAdded = DateTime.Now;
        await _interactionRepo.Add(interaction);
    }

}
