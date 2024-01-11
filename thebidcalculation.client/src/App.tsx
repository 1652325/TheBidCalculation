import './App.css';
import {useEffect, useState} from "react";
import axios from "axios";
import {
    FormControl,
    Grid,
    InputLabel,
    List,
    ListItem,
    MenuItem,
    Paper,
    Select,
    TextField,
    Typography
} from "@mui/material";
import {AuctionResult, formatNumber} from "./models/Auction.ts";

function App() {
    const [vehicleBasePrice, setVehicleBasePrice] = useState<number | null>();
    const [vehicleType, setVehicleType] = useState<number>(0)
    const [auctionResult, setAuctionResult] = useState<AuctionResult | null>(null)

    useEffect(() => {
        const fetchTimeout = setTimeout(handleCalculateCost, 200)
        return () => clearTimeout(fetchTimeout)
    }, [vehicleBasePrice, vehicleType]);

    const handleCalculateCost = async () => {
        if (!vehicleBasePrice || vehicleBasePrice <= 0) {
            setAuctionResult(null)
        } else {
            try {
                const result = await axios.get<AuctionResult>(`https://localhost:5173/api/Auction?VehiclePrice=${vehicleBasePrice}&VehicleType=${vehicleType}`);
                setAuctionResult(result.data);
            } catch (e) {
                setAuctionResult(null)
            }
        }
    }

    return <div className={"w-screen h-screen flex items-center justify-center"}>
        <Grid>
            <Paper elevation={20}>
                <div className={"p-4 flex flex-col gap-y-8 w-96 items-center"}>
                    <Typography variant={"h4"} className={"w-full text-left"}>Bid calculation</Typography>

                    <div className={"flex flex-col gap-y-3 w-full"}>
                        <TextField label={"Vehicle price"} value={vehicleBasePrice}
                                   onChange={event => {
                                       event.target.value ? setVehicleBasePrice(parseInt(event.target.value)) : setVehicleBasePrice(null)
                                   }}
                                   fullWidth
                                   type={"number"}
                        />

                        <FormControl fullWidth>
                            <InputLabel id="vehicle-type">Vehicle type</InputLabel>
                            <Select
                                labelId="vehicle-type"
                                id="vehicle-type-select"
                                value={vehicleType}
                                label="Vehicle type"
                                onChange={event => setVehicleType(event.target.value as number)}
                            >
                                <MenuItem value={0}>Common</MenuItem>
                                <MenuItem value={1}>Luxury</MenuItem>
                            </Select>
                        </FormControl>
                    </div>

                    {auctionResult && vehicleBasePrice && (
                        <div className={"w-full flex flex-col"}>
                            <List>
                                <ListItem>Vehicle Price
                                    ({vehicleType ? "Luxury" : "Common"}): {formatNumber(vehicleBasePrice)}</ListItem>
                                <ListItem>Basic fee: {formatNumber(auctionResult.basicFee)}</ListItem>
                                <ListItem>Special fee: {formatNumber(auctionResult.specialFee)}</ListItem>
                                <ListItem>Association fee: {formatNumber(auctionResult.associationFee)}</ListItem>
                                <ListItem>Storage fee: {formatNumber(auctionResult.storageFee)}</ListItem>
                                <ListItem>Total: {formatNumber(auctionResult.totalCost)}</ListItem>
                            </List>
                        </div>
                    )}
                </div>
            </Paper>
        </Grid>
    </div>
}

export default App;