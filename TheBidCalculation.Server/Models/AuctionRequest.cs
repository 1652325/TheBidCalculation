using System.ComponentModel.DataAnnotations;

namespace TheBidCalculation.Server.Models;

public class  AuctionRequest
{
    public decimal VehiclePrice { get; set; }
    public VehicleType VehicleType { get; set; }
}