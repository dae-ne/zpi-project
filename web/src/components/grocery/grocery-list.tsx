import "./grocery.scss"
import React, { useEffect, useState } from "react"
import { DefaultDateTimeFormat } from "../../constants/time"
import moment, { Moment } from "moment"
import { ListGetIngredientDto, ListGetResponse, ListsService, SendEmailWithListRequest } from "@dietly/sdk"
import Grid from "@mui/material/Grid"
import { DateTimePicker } from "@mui/x-date-pickers"
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment"
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider"
import { fitAlert } from "../../tools/fit-alert"

const GroceryList = () => {
    const [dateFrom, setDateFrom] = useState<Moment | null>(moment(new Date()).startOf('day'))
    const [dateTo, setDateTo] = useState<Moment | null>(moment(new Date()).endOf('day'))
    const [ingredients, setIngredients] = useState<Array<ListGetIngredientDto>>([])

    const handleSendList = () => {
        if (!ingredients || ingredients.length == 0) {
            fitAlert("Error", "Shopping list is empty", "error");
            return;
        }
        const list: SendEmailWithListRequest = {
            ingredientIds: ingredients.map(ingr => ingr.id || 0)
        }

        ListsService.sendEmailWithList(list)
            .then(() => {
                fitAlert("Success", "The shopping list has been successfully sent to your email address", "success");
            })
            .catch(() => {
                fitAlert("Error", "Error during action", "error");
            })
    }

    const updateGroceryList = () => {
        if (!dateFrom || !dateTo) return;
        ListsService.getList(dateFrom.format(DefaultDateTimeFormat), dateTo.format(DefaultDateTimeFormat))
            .then((response: ListGetResponse) => {
                setIngredients(response.ingredients || [])
            })
            .catch(() => { })
    }

    useEffect(() => {
        updateGroceryList()
    }, [dateFrom, dateTo])

    return (
        <Grid container sx={{ my: 4 }}>

            <Grid item xs={7.5} >
                <div className="grocery-list-ingredients">
                    <div className="grocery-list-ingredients-header">Grocery list</div>

                    <ul className="recipe-ingridients-list">
                        {ingredients?.map((ingred: ListGetIngredientDto) =>
                            <li key={"igr" + ingred.id} className="recipe-ingridient">{ingred.name}</li>)}
                    </ul>
                </div>
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

        </Grid >
    )
}

export default GroceryList
