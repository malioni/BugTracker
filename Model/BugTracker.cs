using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BugTracker;

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

public class Interaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InteractionID { get; set; }
    [Required]
    [ForeignKey("BugID")]
    [DisplayName("Bug ID")]
    public int BugID { get; set; }
    [Required]
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
    public DateOnly DateAdded { get; set; }
}

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

public class ModificationInput
{
    public int BugID { get; set; }
    public string Text { get; set; }
    public string NameOrID { get; set; }
}

public class CreationInput
{
    public string Text { get; set; }
    public string NameOrID { get; set; }
}

public class IDTextInput
{
    public int ID { get; set; }
    public string Text { get; set; }
}
