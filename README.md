# BugTracker

## Description
This repository provides a .NET Core Web APIs that can be used to track bugs. From the user side the users are able to create tickets, close tickets and retrieve the status of a ticket. They are also able to see all of their tickets in the database. On the other end, the IT staff are able to assign the ticket to a particlar person, change the status of a ticket as well as make notes about the ticket. Each modification action is saved in an Interactions table in the database for overview of the history.

## Endpoints
### User
#### /user/BugTrackerUser/CreateBugTicket (POST)
**Inputs:**
1. "text": description of the issue (string)
2. "nameOrID": name of the user submitting the ticket, or the ID of the user if known (string)
   
**Outputs:**

Ok (200) IActionResult with a confirming message.

#### /user/BugTrackerUser/GetAllBugsForUser (GET)
**Inputs:**
1. "userName": name of the user (string)
   
**Outputs:**

List of tickets for the specified user or

NotFound (404) IActionResult if no tickets were found for the specified user.

#### /user/BugTrackerUser/GetBugStatusID (GET)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
   
**Outputs:**

Information for the specified bug or

NotFound (404) IActionResult if the ticket was not found.

#### /user/BugTrackerUser/CloseBugTicket (PUT)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
   
**Outputs:**

Ok (200) IActionResult with a confirming message or

NotFound (404) IActionResult if the ticket was not found.

### IT Staff

#### /staff/BugTrackerStaff/AddNote (POST)
**Inputs:**
1. "bugID": identifier of the ticket in which the bug is reported (integer)
2. "text": message (string)
3. "nameOrID": name of the staff member adding the note, or the ID of the staff member if known (string)
   
**Outputs:**

Ok (200) IActionResult with a confirming message or

NotFound (404) IActionResult if the user or the ticket could not be found.

#### /staff/BugTrackerStaff/AssignTicket (PUT)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
3. "Text": name of the staff member assigned to the ticket, or the ID of the user if known (string)
   
**Outputs:**

Ok (200) IActionResult with a confirming message or

NotFound (404) IActionResult if the user or the ticket could not be found.

#### /staff/BugTrackerStaff/ChangeStatus (PUT)
**Inputs:**
1. "BugID": identifier of the ticket in which the bug is reported (integer)
2. "text": new status (string)
3. "nameOrID": name of the staff member changing the status, or the ID of the staff member if known (string)
   
**Outputs:**

Ok (200) IActionResult with a confirming message or

NotFound (404) IActionResult if the user or the ticket could not be found.

## Instructions
Make sure you have SQLite installed on your computer.

Clone the git repository in your desired location.

Open the solution in the repository from Visual Studio (Visual Studio for Mac used in development).

Open "Manage NuGet packages" in the project and make sure that following packages have been installed:
- Microsoft.AspNetCore.OpenApi
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Swashbuckle.AspNetCore

Build and run the solution. A Swagger webpage will open up in your browser. The created database can be found in the project directory under the name of "bug_tracker.db".
