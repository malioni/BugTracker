using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker.Model;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }
    [Required]
    [DisplayName("User Name")]
    [MaxLength(50)]
    public string UserName { get; set; }
}

