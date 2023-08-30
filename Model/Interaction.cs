using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class Interaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InteractionID { get; set; }
    [Required]
    [ForeignKey("BugID")]
    [DisplayName("Bug ID")]
    public int BugID { get; set; }
    [ForeignKey("UserID")]
    [DisplayName("User ID")]
    public int UserID { get; set; }
    [ForeignKey("StaffID")]
    [DisplayName("IT Staff ID")]
    public int StaffID { get; set; }
    [Required]
    [MaxLength(255)]
    [DisplayName("Comment")]
    public string InteractionText { get; set; }
    [Required]
    [DisplayName("Date Added")]
    public DateTime DateAdded { get; set; }
}

