import React, { useState } from "react"
import CustomTextField from "../../controls/custom-text-field";
import { useNavigate } from "react-router-dom";
import { RECIPE_NEW } from "../../../constants/app-route";
import { RecipeListMode } from "../../../enums/recipe";
import CheckIcon from '@mui/icons-material/Check';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import moment, { Moment } from "moment";
import { Grid } from "@mui/material"
import AddIcon from '@mui/icons-material/Add';

interface RecipeListHeaderInterface {
    mode: RecipeListMode,
    onSearchSubmit: (value: string) => void,
    onSelectRecipeSubmit?: (value: Date) => void,
}

const DEFAULT_MEAL_TIME: string = "2022-04-17T08:00"

const RecipeListHeader = ({ mode, onSearchSubmit, onSelectRecipeSubmit: onSelectRecipe }: RecipeListHeaderInterface) => {
    const navigate = useNavigate();

    const [searchValue, setSearchValue] = useState<string>("")
    const [time, setTime] = useState<Moment | null>(moment(DEFAULT_MEAL_TIME))

    const handleNewRecipe = () => {
        navigate(RECIPE_NEW);
    }

    const handleAccept = () => {
        if (!onSelectRecipe || !time)
            return;

        onSelectRecipe(time?.toDate())
    }

    return (
        <Grid container>
            <Grid item xs={3} />

            <Grid item xs={mode == RecipeListMode.View ? 8 : 6} sx={{ py: 3, flexGrow: 1, display: 'flex' }}>
                <CustomTextField
                    name="email"
                    placeholder="Recipe name..."
                    inputStyles={{ borderRadius: "5px 0 0 5px" }}
                    value={searchValue}
                    onChange={setSearchValue}
                />

                <div className="button-std search-button" onClick={() => onSearchSubmit(searchValue)}>Search</div>
            </Grid>
            {
                mode === RecipeListMode.View ?
                    <Grid item xs={1} sx={{ pt: 3 }} display="flex" justifyContent="flex-end" color="white">
                        <div className="button-std add-button" onClick={handleNewRecipe}>
                            <AddIcon />
                        </div>
                    </Grid>
                    :
                    <>
                        <Grid item xs={3} sx={{ pt: 3 }} display="flex" justifyContent="flex-end" color="white">
                            <LocalizationProvider dateAdapter={AdapterMoment} >
                                <TimePicker
                                    sx={{ mx: "3em", width: "200px", fontSize: "0.5em" }}
                                    ampm={false}
                                    value={time}
                                    onChange={(newValue) => setTime(newValue)}
                                    slotProps={{ textField: { size: 'small' } }}
                                />
                            </LocalizationProvider>


                            <div className="button-std add-button" onClick={handleAccept}>
                                <CheckIcon />
                            </div>
                        </Grid>


                    </>
            }


        </Grid>

    )
}

export default RecipeListHeader