using System.ComponentModel.DataAnnotations;

namespace ABPBackendTZ.Models;

public class ButtonColor
{
    [Key] public int Id { get; set; }
    public string HEX { get; set; }
}