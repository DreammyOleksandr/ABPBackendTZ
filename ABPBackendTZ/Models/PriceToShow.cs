using System.ComponentModel.DataAnnotations;

namespace ABPBackendTZ.Models;

public class PriceToShow
{
    [Key] public int Id { get; set; }
    public decimal Value {get; set;}
    public float Percentage { get; set; }
}