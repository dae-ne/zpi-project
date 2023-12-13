import React from "react"
import "./recipe-edit.scss"
import RecipeEditContent from "./recipe-edit-content"
import RecipeEditStats from "./recipe-edit-stats"
import Grid from "@mui/material/Grid"

const RecipeEdit = () => {
    return (
        <Grid container sx={{ my: 4 }}>
            <Grid item xs={7.5} >
                <RecipeEditContent />
            </Grid>

            <Grid item xs={4.5} >
                <RecipeEditStats />
            </Grid>
        </Grid>
    )
}

export default RecipeEdit