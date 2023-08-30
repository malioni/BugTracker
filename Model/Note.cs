using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NoteID { get; set; }
    [Required]
    [ForeignKey("BugID")]
    [DisplayName("Bug ID")]
    public int BugID { get; set; }
    [ForeignKey("StaffID")]
    [DisplayName("IT Staff ID")]
    public int StaffID { get; set; }
    [Required]
    [MaxLength(255)]
    [DisplayName("Comment")]
    public string NoteText { get; set; }
    [Required]
    [DisplayName("Date Added")]
    public DateOnly DateAdded { get; set; }
}

