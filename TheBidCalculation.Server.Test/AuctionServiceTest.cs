using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using TheBidCalculation.Server.Models;
using TheBidCalculation.Server.Services;
using TheBidCalculation.Server.Services.Impl;
using TheBidCalculation.Server.Validators.Impl;

namespace TheBidCalculation.Server.Test;

public class AuctionServiceTest
{
    private readonly AuctionServiceImpl _auctionService = new AuctionServiceImpl(new AuctionRequestValidatorImpl());
    
    [Test]
    public void CalculateTotalCost_ShouldReturnCorrectValues_ForCommonCar398Dollars()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 398,
            VehicleType = VehicleType.Common
        };
        
        // Act
        var result = _auctionService.CalculateTotalCost(auctionRequest);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.BasicFee, Is.EqualTo(39.8));
            Assert.That(result.SpecialFee, Is.EqualTo(7.96));
            Assert.That(result.AssociationFee, Is.EqualTo(5));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(550.76));
        });
    }

    [Test]
    public void CalculateTotalCost_ShouldReturnCorrectValues_ForCommonCar501Dollars()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 501,
            VehicleType = VehicleType.Common
        };

        // Act
        var result = _auctionService.CalculateTotalCost(auctionRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.BasicFee, Is.EqualTo(50));
            Assert.That(result.SpecialFee, Is.EqualTo(10.02));
            Assert.That(result.AssociationFee, Is.EqualTo(10));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(671.02));
        });
    }
    
    [Test]
    public void CalculateTotalCost_ShouldReturnCorrectValues_ForCommonCar57Dollars()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 57,
            VehicleType = VehicleType.Common
        };

        // Act
        var result = _auctionService.CalculateTotalCost(auctionRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.BasicFee, Is.EqualTo(10));
            Assert.That(result.SpecialFee, Is.EqualTo(1.14));
            Assert.That(result.AssociationFee, Is.EqualTo(5));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(173.14));
        });
    }
    
    [Test]
    public void CalculateTotalCost_ShouldReturnCorrectValues_ForLuxuryCar1800Dollars()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 1800,
            VehicleType = VehicleType.Luxury
        };

        // Act
        var result = _auctionService.CalculateTotalCost(auctionRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.BasicFee, Is.EqualTo(180));
            Assert.That(result.SpecialFee, Is.EqualTo(72));
            Assert.That(result.AssociationFee, Is.EqualTo(15));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(2167));
        });
    }
    
    [Test]
    public void CalculateTotalCost_ShouldReturnCorrectValues_ForCommonCar1100Dollars()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 1100,
            VehicleType = VehicleType.Common
        };

        // Act
        var result = _auctionService.CalculateTotalCost(auctionRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.BasicFee, Is.EqualTo(50));
            Assert.That(result.SpecialFee, Is.EqualTo(22));
            Assert.That(result.AssociationFee, Is.EqualTo(15));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(1287));
        });
    }
    
    [Test]
    public void CalculateTotalCost_ShouldReturnCorrectValues_ForLuxuryCar1000000Dollars()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 1_000_000,
            VehicleType = VehicleType.Luxury
        };

        // Act
        var result = _auctionService.CalculateTotalCost(auctionRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.BasicFee, Is.EqualTo(200));
            Assert.That(result.SpecialFee, Is.EqualTo(40000));
            Assert.That(result.AssociationFee, Is.EqualTo(20));
            Assert.That(result.StorageFee, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(1_040_320));
        });
    }

    [Test]
    public void CalculateTotalCost_ShouldThrowBadRequest_For0DollarsCar()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = 0,
            VehicleType = VehicleType.Common
        };
        
        // Act + Assert
        Assert.Throws<BadHttpRequestException>(() => _auctionService.CalculateTotalCost(auctionRequest));
    }
    
    [Test]
    public void CalculateTotalCost_ShouldThrowBadRequest_ForNegativeDollarsCar()
    {
        // Arrange
        var auctionRequest = new AuctionRequest
        {
            VehiclePrice = -200,
            VehicleType = VehicleType.Common
        };
        
        // Act + Assert
        Assert.Throws<BadHttpRequestException>(() => _auctionService.CalculateTotalCost(auctionRequest));
    }
}