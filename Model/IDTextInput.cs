using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class IDTextInput
{
    public int BugID { get; set; }
    public string Text { get; set; }
}