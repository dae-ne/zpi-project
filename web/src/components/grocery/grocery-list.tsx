import "./grocery.scss"

import React, { useEffect, useState } from "react"
import Grid from "@mui/material/Grid"
import { DateTimePicker, TimePicker } from "@mui/x-date-pickers"
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment"
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider"
import { Moment } from "moment"
import moment from "moment"
import { GetPlansResponse, PlansService } from "@dietly/sdk"

const GroceryList = () => {
    const [dateFrom, setDateFrom] = useState<Moment | null>(moment(new Date()).startOf('day'))
    const [dateTo, setDateTo] = useState<Moment | null>(moment(new Date()).endOf('day'))

    const handleSendList = () => {
        //  console.log(dateFrom?.toDate())
        // console.log(dateTo?.toDate())
        //TODO wysyłkaa
        console.log("wysyłkaa maila")
    }

    const updateRecipeList = () => {
        console.log("update")
        //todo dorobic do godzin....
        if (!dateFrom || !dateTo) return;
        PlansService.getPlans(dateFrom.format("DD.MM.YYYY"), dateTo.format("DD.MM.YYYY"))
            .then((response: GetPlansResponse) => {
                console.log(response)

            })
            .catch((err) => {
                console.log(err)
            })
    }

    useEffect(() => {
        updateRecipeList()
    }, [dateFrom, dateTo])

    return (
        <Grid container sx={{ my: 4 }}>

            <Grid item xs={7.5} className="grocery-list-ingredients">
                <div className="grocery-list-ingredients-header">Grocery list</div>

                <ul className="recipe-ingridients-list">
                    {/* {ingredients?.map((ingredient: CreateRecipeIngredientDto, index: number) => */}
                    <li className="recipe-ingridient">element 1</li>
                    <li className="recipe-ingridient">element 1</li>
                    <li className="recipe-ingridient">element 1</li>
                    <li className="recipe-ingridient">element 1</li>
                    <li className="recipe-ingridient">element 1</li>

                </ul>

            </Grid>

            <Grid item xs={4.5}>
                <div className="grocery-list-menu">
                    <LocalizationProvider dateAdapter={AdapterMoment}  >

                        <div className="grocery-list-menu-header">Date and time from</div>
                        <DateTimePicker
                            sx={{
                                fontSize: "0.5em", mb: 4, backgroundColor: "white", width: "100%"
                                , input: { fontSize: "1em" },
                                "& fieldset": { border: 'none' }
                            }}
                            ampm={false}
                            format="DD/MM/YYYY HH:mm"
                            value={dateFrom}
                            onChange={(newValue) => setDateFrom(newValue)}
                            slotProps={{ textField: { size: 'small' } }}
                        />
                        <div className="grocery-list-menu-header">Date and time to</div>
                        <DateTimePicker
                            sx={{
                                fontSize: "0.5em", mb: 4, backgroundColor: "white", width: "100%"
                                , input: { fontSize: "1em" },
                                "& fieldset": { border: 'none' }
                            }}
                            ampm={false}
                            format="DD/MM/YYYY HH:mm"
                            value={dateTo}
                            onChange={(newValue) => setDateTo(newValue)}
                            slotProps={{ textField: { size: 'small' } }}
                        />
                    </LocalizationProvider>

                    <div className="button-std recipe-edit-save" onClick={handleSendList}>Send list as email</div>
                </div>
            </Grid>

        </Grid>
    )
}

export default GroceryList
