import { Grid, TextField } from "@mui/material"
import React from "react"
import RecipeListMenu from "./recipe-list-menu"
import appTheme from "../../theme"
import AddIcon from '@mui/icons-material/Add';
import CustomTextField from "../../controls/custom-text-field";


const RecipeListHeader = () => {

    return (
        <Grid container>
            <Grid item xs={3} />

            <Grid item xs={8} sx={{ py: 3, flexGrow: 1, display: 'flex' }}>
                <CustomTextField name="email" placeholder="Recipe name..." inputStyles={{ borderRadius: "5px 0 0 5px" }} />

                <div className="button-std search-button">Search</div>
            </Grid>

            <Grid item xs={1} sx={{ pt: 3 }} display="flex" justifyContent="flex-end" color="white">
                <div className="button-std add-button">
                    <AddIcon />
                </div>
            </Grid>
        </Grid>

    )
}

export default RecipeListHeader