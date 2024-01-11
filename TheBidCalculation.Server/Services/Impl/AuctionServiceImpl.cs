using TheBidCalculation.Server.Models;
using TheBidCalculation.Server.Validators;

namespace TheBidCalculation.Server.Services.Impl;

public class AuctionServiceImpl(IAuctionRequestValidator requestValidator) : IAuctionService
{
    private readonly IAuctionRequestValidator _requestValidator = requestValidator;

    private const decimal BasicFeePercentage = 0.10m;
    private const decimal CommonMinBasicFee = 10.00m;
    private const decimal LuxuryMinBasicFee = 25.00m;
    private const decimal CommonMaxBasicFee = 50.00m;
    private const decimal LuxuryMaxBasicFee = 200.00m;
    
    private const decimal CommonSpecialFeePercentage = 0.02m;
    private const decimal LuxurySpecialFeePercentage = 0.04m;
    
    private const decimal AssociationFeeTier1 = 500.00m;
    private const decimal AssociationFeeTier2 = 1000.00m;
    private const decimal AssociationFeeTier3 = 3000.00m;
    private const decimal AssociationFeeTier1Cost = 5.00m;
    private const decimal AssociationFeeTier2Cost = 10.00m;
    private const decimal AssociationFeeTier3Cost = 15.00m;
    private const decimal AssociationFeeMaxCost = 20.00m;
    
    private const decimal StorageFee = 100.00m;

    public AuctionResult CalculateTotalCost(AuctionRequest request)
    {
        _requestValidator.ValidateAuctionRequest(request);
        
        var basicFee = CalculateBasicFee(request.VehiclePrice, request.VehicleType);
        var specialFee = CalculateSpecialFee(request.VehiclePrice, request.VehicleType);
        var associationFee = CalculateAssociationFee(request.VehiclePrice);

        return new AuctionResult(request)
        {
            BasicFee = basicFee,
            SpecialFee = specialFee,
            AssociationFee = associationFee,
            StorageFee = StorageFee,
        };
    }

    private decimal CalculateBasicFee(decimal vehiclePrice, VehicleType vehicleType)
    {
        var minFee = (vehicleType == VehicleType.Common) ? CommonMinBasicFee : LuxuryMinBasicFee;
        var maxFee = (vehicleType == VehicleType.Common) ? CommonMaxBasicFee : LuxuryMaxBasicFee;

        var basicFee = vehiclePrice * BasicFeePercentage;
        return Math.Max(minFee, Math.Min(basicFee, maxFee));
    }

    private decimal CalculateSpecialFee(decimal vehiclePrice, VehicleType vehicleType)
    {
        var appliedPercentage = (vehicleType == VehicleType.Common) ? CommonSpecialFeePercentage : LuxurySpecialFeePercentage;
        return vehiclePrice * appliedPercentage;
    }

    private decimal CalculateAssociationFee(decimal vehiclePrice)
    {
        return vehiclePrice switch
        {
            <= AssociationFeeTier1 => AssociationFeeTier1Cost,
            <= AssociationFeeTier2 => AssociationFeeTier2Cost,
            <= AssociationFeeTier3 => AssociationFeeTier3Cost,
            _ => AssociationFeeMaxCost
        };
    }
}