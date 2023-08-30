using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class Bug
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BugID { get; set; }
    [Required]
    [MaxLength(255)]
    public string Description { get; set; }
    [Required]
    [DisplayName("Date Reported")]
    public DateOnly DateReported { get; set; }
    [Required]
    [ForeignKey("UserID")]
    [DisplayName("User ID")]
    public int UserID { get; set; }
    [ForeignKey("StaffID")]
    [DisplayName("IT Staff ID")]
    public int StaffID { get; set; }
    [Required]
    [DisplayName("Bug Status")]
    [MaxLength(20)]
    public string Status { get; set; }
}