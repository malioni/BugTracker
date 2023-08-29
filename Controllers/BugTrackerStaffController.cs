using Microsoft.AspNetCore.Mvc;
using BugTracker.Model.Interfaces;

namespace BugTracker.Controllers;

[ApiController]
[Route("staff/[controller]")]
public class BugTrackerStaffController : ControllerBase
{
    private readonly IBugRepo _bugRepo;
    private readonly IUserRepo _userRepo;
    private readonly IStaffRepo _staffRepo;
    private readonly IInteractionRepo _interactionRepo;
    private readonly INoteRepo _noteRepo;

    public BugTrackerStaffController(IBugRepo bugRepo,
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

    [HttpPost("AddInteractionStaff")]
    public async Task<IActionResult> AddInteractionUser(int bugID, string text, string staffName)
    {
        var staff = await _staffRepo.GetByName(staffName);
        var bug = await _bugRepo.GetByID(bugID);
        if ((staff != null) && (bug != null))
        {
            var interaction = new Interaction();
            interaction.BugID = bugID;
            interaction.InteractionText = text;
            interaction.StaffID = staff.StaffID;
            interaction.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            await _interactionRepo.Add(interaction);
            return Ok("Interaction added successfully.");
        }
        else
        {
            return NotFound($"Ticket with ID {bugID} or staff with name {staffName} not found in the database.");
        }
    }

    [HttpPost("AddNote")]
    public async Task<IActionResult> AddNote(int bugID, string text, string staffName)
    {
        var staff = await _staffRepo.GetByName(staffName);
        var bug = await _bugRepo.GetByID(bugID);
        if ((staff != null) && (bug != null))
        {
            var note = new Note();
            note.BugID = bugID;
            note.StaffID = staff.StaffID;
            note.NoteText = text;
            note.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            await _noteRepo.Add(note);
            return Ok($"Note added successfully to Bug ID {bugID}");
        }
        else
        {
            return NotFound($"Bug ID {bugID} or staff name {staffName} not found in the database.");
        }
    }

    [HttpPut("AssignTicketStaffName")]
    public async Task<IActionResult> AssignTicketStaffName(int bugID, string staffName)
    {
        var staff = await _staffRepo.GetByName(staffName);
        if (staff == null)
        {
            staff = new Staff();
            staff.StaffName = staffName;
            await _staffRepo.Add(staff);
        }
        var bug = await _bugRepo.GetByID(bugID);
        if (bug != null)
        {
            bug.StaffID = staff.StaffID;
            await _bugRepo.Update(bug);
            return Ok($"Ticket with ID {bugID} has been assigned to {staffName}.");
        }
        else
        {
            return NotFound($"Ticket with ID {bugID} not found.");
        }

    }

    [HttpPut("AssignTicketStaffID")]
    public async Task<IActionResult> AssignTicketStaffID(int bugID, int staffID)
    {
        var staff = await _staffRepo.GetByID(staffID);
        var bug = await _bugRepo.GetByID(bugID);
        if ((staff != null) && (bug != null))
        {
            bug.StaffID = staff.StaffID;
            await _bugRepo.Update(bug);
            return Ok($"Ticket with ID {bugID} has been assigned to staff with ID {staffID}.");
        }
        else
        {
            return NotFound($"Ticket with ID {bugID} or staff with ID {staffID} not found in the database.");
        }

    }

    [HttpPut("ChangeStatus")]
    public async Task<IActionResult> ChangeStatus(int bugID, string status)
    {
        var bug = await _bugRepo.GetByID(bugID);
        if (bug != null)
        {
            bug.Status = status;
            await _bugRepo.Update(bug);
            return Ok($"Status changed to {status} for bug ID {bugID}");
        }
        else
        {
            return NotFound($"No ticket with ID {bugID} found.");
        }
    }

}
