using Microsoft.AspNetCore.Mvc;
using BugTracker.Model.Interfaces;
using System;

namespace BugTracker.Controllers;

[ApiController]
[Route("user/[controller]")]
public class BugTrackerUserController : ControllerBase
{
    private readonly IBugRepo _bugRepo;
    private readonly IUserRepo _userRepo;
    private readonly IStaffRepo _staffRepo;
    private readonly IInteractionRepo _interactionRepo;
    private readonly INoteRepo _noteRepo;

    public BugTrackerUserController(IBugRepo bugRepo,
        IUserRepo userRepo,
        IStaffRepo staffRepo,
        IInteractionRepo interactionRepo,
        INoteRepo noteRepo)
    {
        _bugRepo = bugRepo;
        _userRepo = userRepo;
        _staffRepo = staffRepo;
        _interactionRepo = interactionRepo;
        _noteRepo = noteRepo;
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
        return Ok($"Ticket has been created with ticked ID: {bug.BugID}");
    }

    [HttpPost("AddInteractionUser")]
    public async Task<IActionResult> AddInteractionUser(ModificationInput inp)
    {
        var bug = await _bugRepo.GetByID(inp.BugID);
        var user = await CheckIfNameOrID(inp.NameOrID);

        if ((user != null) && (bug != null))
        {
            var interaction = new Interaction();
            interaction.BugID = inp.BugID;
            interaction.InteractionText = inp.Text;
            interaction.UserID = user.UserID;
            interaction.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            await _interactionRepo.Add(interaction);
            return Ok("Interaction added successfully.");
        }
        else
        {
            return NotFound($"Bug with ID {inp.BugID} or user name or ID {inp.NameOrID} not found.");
        }
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

}
