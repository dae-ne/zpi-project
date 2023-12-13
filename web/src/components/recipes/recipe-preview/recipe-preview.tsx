
import React from "react"
import Grid from "@mui/material/Grid"

import "./recipe-preview.scss"

import RecipePreviewStats from "./recipe-preview-stats"
import RecipePreviewContent from "./recipe-preview-content"

const RecipePreview = () => {
    return (
        <Grid container sx={{ my: 4 }}>

            <Grid item xs={8.2} >
                <RecipePreviewContent />
            </Grid>

            <Grid item xs={3.8} >
                <RecipePreviewStats />
            </Grid>

        </Grid>
    )
}

export default RecipePreview