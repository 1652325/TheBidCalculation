using Microsoft.AspNetCore.Mvc;
using TheBidCalculation.Server.Models;
using TheBidCalculation.Server.Services;

namespace TheBidCalculation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController(IAuctionService auctionService) : ControllerBase
{
    [HttpGet]
    public AuctionResult GetAuctionResult([FromQuery] AuctionRequest auctionRequest)
    {
        return auctionService.CalculateTotalCost(auctionRequest);
    }
}