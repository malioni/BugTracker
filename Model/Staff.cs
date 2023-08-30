using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class Staff
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StaffID { get; set; }
    [Required]
    [DisplayName("IT Staff Name")]
    [MaxLength(50)]
    public string StaffName { get; set; }
}

