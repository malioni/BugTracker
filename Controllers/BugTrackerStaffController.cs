﻿using Microsoft.AspNetCore.Mvc;
using BugTracker.Services.Interfaces;
using BugTracker.Model;

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

    [HttpPost("AddNote")]
    public async Task<IActionResult> AddNote(ModificationInput inp)
    {
        var staff = await CheckIfNameOrID(inp.NameOrID);
        var bug = await _bugRepo.GetByID(inp.BugID);
        if ((staff != null) && (bug != null))
        {
            var note = new Note();
            note.BugID = inp.BugID;
            note.StaffID = staff.StaffID;
            note.NoteText = inp.Text;
            note.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            await _noteRepo.Add(note);
            await AddInteraction(bug.BugID, staff.StaffID, $"Note with ID {note.NoteID} added to the ticket.");
            return Ok($"Note added successfully to Bug ID {inp.BugID}");
        }
        else
        {
            return NotFound($"Bug ID {inp.BugID} or staff name {inp.NameOrID} not found in the database.");
        }
    }

    [HttpPut("AssignTicketStaff")]
    public async Task<IActionResult> AssignTicketStaff(IDTextInput inp)
    {
        var staff = await CheckIfNameOrID(inp.Text);
        if (staff == null)
        {
            staff = new Staff();
            staff.StaffName = inp.Text;
            await _staffRepo.Add(staff);
        }
        var bug = await _bugRepo.GetByID(inp.BugID);
        if (bug != null)
        {
            bug.StaffID = staff.StaffID;
            await _bugRepo.Update(bug);
            await AddInteraction(bug.BugID, staff.StaffID, $"{staff.StaffName} has been assigned the ticket.");
            return Ok($"Ticket with ID {inp.BugID} has been assigned to {inp.Text}.");
        }
        else
        {
            return NotFound($"Ticket with ID {inp.BugID} not found.");
        }

    }

    [HttpPut("ChangeStatus")]
    public async Task<IActionResult> ChangeStatus(ModificationInput inp)
    {
        var staff = await CheckIfNameOrID(inp.NameOrID);
        var bug = await _bugRepo.GetByID(inp.BugID);
        if ((bug != null) && (staff != null))
        {
            bug.Status = inp.Text;
            await _bugRepo.Update(bug);
            await AddInteraction(bug.BugID, staff.StaffID, $"Ticket status has been changed to {inp.Text}");
            return Ok($"Status changed to {inp.Text} for bug ID {inp.BugID}");
        }
        else
        {
            return NotFound($"No ticket with ID {inp.BugID} ot staff with {inp.NameOrID} found.");
        }
    }

    private async Task<Staff> CheckIfNameOrID(string name)
    {
        var staff = new Staff();
        if (int.TryParse(name, out int staffID))
        {
            staff = await _staffRepo.GetByID(staffID);
        }
        else
        {
            staff = await _staffRepo.GetByName(name);
        }
        return staff;
    }

    private async Task AddInteraction(int bugID, int staffID, string text)
    {
        var interaction = new Interaction();
        interaction.BugID = bugID;
        interaction.StaffID = staffID;
        interaction.InteractionText = text;
        interaction.DateAdded = DateTime.Now;
        await _interactionRepo.Add(interaction);
    }

}
