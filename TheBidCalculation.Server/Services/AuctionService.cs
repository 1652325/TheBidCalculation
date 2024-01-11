using TheBidCalculation.Server.Models;

namespace TheBidCalculation.Server.Services;

public interface IAuctionService
{
    AuctionResult CalculateTotalCost(AuctionRequest request);
}