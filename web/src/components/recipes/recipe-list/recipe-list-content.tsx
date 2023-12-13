import { Box, Grid } from "@mui/material"
import React from "react"
import RecipeListMenu from "./recipe-list-menu"
import RecipeListElement from "./recipe-list-element"


const RecipeListContent = () => {

    return (
        <Grid container sx={{ mt: 1 }}>

            <Grid item xs={3} >
                <RecipeListMenu />
            </Grid>

            <Grid item xs={9} >
                <RecipeListElement />
                <RecipeListElement />
                <RecipeListElement />
            </Grid>

        </Grid>
    )
}

export default RecipeListContent