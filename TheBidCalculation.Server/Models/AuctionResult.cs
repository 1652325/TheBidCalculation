namespace TheBidCalculation.Server.Models;

public class AuctionResult(AuctionRequest request)
{
    private AuctionRequest Request { get; } = request;
    public decimal BasicFee { get; init; }
    public decimal SpecialFee { get; init; }
    public decimal AssociationFee { get; init; }
    public decimal StorageFee { get; init; }
    public decimal TotalCost => Request.VehiclePrice + BasicFee + SpecialFee + AssociationFee + StorageFee;
}