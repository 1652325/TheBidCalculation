export interface AuctionResult {
    basicFee: number;
    specialFee: number;
    associationFee: number;
    storageFee: number;
    totalCost: number;
}

export const formatNumber = (value: number): string => {
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',

    });
    
    return formatter.format(value);
}