using TheBidCalculation.Server.Models;

namespace TheBidCalculation.Server.Validators;

public interface IAuctionRequestValidator
{
    bool ValidateAuctionRequest(AuctionRequest auctionRequest);
}