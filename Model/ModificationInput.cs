using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class ModificationInput
{
    public int BugID { get; set; }
    public string Text { get; set; }
    public string NameOrID { get; set; }
}

