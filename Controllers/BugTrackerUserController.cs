using Microsoft.AspNetCore.Mvc;
using BugTracker.Model.Interfaces;

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
    public async Task<IActionResult> CreateTicket(string userName, string description)
    {
        var user = await _userRepo.GetByName(userName);
        if (user == null)
        {
            user = new User();
            user.UserName = userName;
            await _userRepo.Add(user);
        }
        var bug = new Bug();
        bug.Description = description;
        bug.UserID = user.UserID;
        bug.Status = "New";
        bug.DateReported = DateOnly.FromDateTime(DateTime.Now);
        await _bugRepo.Add(bug);
        return Ok($"Ticket has been created with ticked ID: {bug.BugID}");
    }

    [HttpPost("AddInteractionUser")]
    public async Task<IActionResult> AddInteractionUser(int bugID, string text, string userName)
    {
        var bug = await _bugRepo.GetByID(bugID);
        var user = await _userRepo.GetByName(userName);

        if ((user != null) && (bug != null))
        {
            var interaction = new Interaction();
            interaction.BugID = bugID;
            interaction.InteractionText = text;
            interaction.UserID = user.UserID;
            interaction.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            await _interactionRepo.Add(interaction);
            return Ok("Interaction added successfully.");
        }
        else
        {
            return NotFound($"Bug with ID {bugID} or user name {userName} not found.");
        }
    }

    [HttpGet("GetAllBugsForUser")]
    public async Task<IEnumerable<Bug>> GetBugStatusUserName(string userName)
    {
        var user = await _userRepo.GetByName(userName);
        if (user != null)
            return await _bugRepo.GetByUserID(user.UserID);
        else
            return Enumerable.Empty<Bug>();
    }

    [HttpGet("GetBugStatusID")]
    public async Task<Bug> GetBugStatusID(int bugID)
    {
        return await _bugRepo.GetByID(bugID);
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

}
