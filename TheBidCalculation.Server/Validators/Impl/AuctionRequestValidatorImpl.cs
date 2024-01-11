using TheBidCalculation.Server.Models;

namespace TheBidCalculation.Server.Validators.Impl;

public class AuctionRequestValidatorImpl : IAuctionRequestValidator
{
    public bool ValidateAuctionRequest(AuctionRequest auctionRequest)
    {
        if (auctionRequest.VehiclePrice <= 0)
            throw new BadHttpRequestException("Vehicle price isn't set properly");

        return true;
    }
}