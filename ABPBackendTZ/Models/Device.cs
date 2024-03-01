using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPBackendTZ.Models;

public class Device
{
    [Key] public string Token { get; set; }
    
    public int? ButtonColorId { get; set; }
    [ForeignKey("ButtonColorId")]
    public ButtonColor? ButtonColor { get; set; }
    
    public int? PriceToShowId { get; set; }
    [ForeignKey("PriceToShowId")]
    public PriceToShow? PriceToShow { get; set; }
    
}