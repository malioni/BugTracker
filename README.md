# BugTracker

## Description
This repository provides a .NET Core Web APIs that can be used to track bugs. From the user side the users are able to create tickets, close tickets and retrieve the status of a ticket. They are also able to add an interaction regarding a ticket and see all of their tickets in the database. On the other end, the IT staff are able to assign the ticket to a particlar person, change the status of a ticket as well as make notes or send messages (interactions) to the user.

## Endpoints
### User
#### /user/BugTrackerUser/CreateBugTicket (POST)
**Inputs:**
1. "text": description of the issue (string)
2. "nameOrID": name of the user submitting the ticket, or the ID of the user if known (string)
   
**Outputs:**

Ok IActionResult with a confirming message.

#### /user/BugTrackerUser/AddInteractionUser (POST)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
2. "text": message (string)
3. "nameOrID": name of the user sending the message, or the ID of the user if known (string)
   
**Outputs:**

Ok IActionResult with a confirming message or

NotFound IActionResult if the user or the ticket could not be found.

#### /user/BugTrackerUser/GetAllBugsForUser (GET)
**Inputs:**
1. "userName": name of the user (string)
   
**Outputs:**

List of tickets for the specified user or

NotFound IActionResult if no tickets were found for the specified user.

#### /user/BugTrackerUser/GetBugStatusID (GET)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
   
**Outputs:**

Information for the specified bug or

NotFound IActionResult if the ticket was not found.

#### /user/BugTrackerUser/CloseBugTicket (PUT)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
   
**Outputs:**

Ok IActionResult with a confirming message or

NotFound IActionResult if the ticket was not found.

### IT Staff
#### /staff/BugTrackerStaff/AddInteractionStaff (POST)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
2. "text": message (string)
3. "nameOrID": name of the staff member sending the message, or the ID of the staff member if known (string)
   
**Outputs:**

Ok IActionResult with a confirming message or

NotFound IActionResult if the user or the ticket could not be found.

#### /staff/BugTrackerStaff/AddNote (POST)
**Inputs:**
1. "bugID": identifier of the ticket in which the bug is reported (integer)
2. "text": message (string)
3. "nameOrID": name of the staff member adding the note, or the ID of the staff member if known (string)
   
**Outputs:**

Ok IActionResult with a confirming message or

NotFound IActionResult if the user or the ticket could not be found.

#### /staff/BugTrackerStaff/AssignTicket (PUT)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
3. "Text": name of the staff member assigned to the ticket, or the ID of the user if known (string)
   
**Outputs:**

Ok IActionResult with a confirming message or

NotFound IActionResult if the user or the ticket could not be found.

#### /staff/BugTrackerStaff/ChangeStatus (PUT)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
2. "text": new status (string)
   
**Outputs:**

Ok IActionResult with a confirming message or

NotFound IActionResult if the user or the ticket could not be found.

## Instructions
