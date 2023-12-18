import { Box, Grid } from "@mui/material"
import React, { useEffect, useState } from "react"
import RecipeListMenu from "./recipe-list-menu"
import RecipeListElement from "./recipe-list-element"
import { DifficultyLevel, GetRecipeResponse, GetRecipesResponse, RecipesService } from "../../../sdk"
import { useNavigate } from 'react-router-dom';
import { RECIPE_PREVIEW_RAW } from "../../../constants/app-route"

interface RecipeListContentInterface {
    data: Array<GetRecipeResponse> | null,
    tags: Array<string> | null,
    onTagSelectionChange: (value: string[] | undefined) => void,
    onDifficultyLevelChange: (value: DifficultyLevel[] | undefined) => void,
    onTimeRangeChange: (min: number, max: number) => void,
    onEnergyRangeChange: (min: number, max: number) => void
}

const RecipeListContent = (props: RecipeListContentInterface) => {
    const { tags, data, onTagSelectionChange, onDifficultyLevelChange, onTimeRangeChange, onEnergyRangeChange } = props
    const navigate = useNavigate();

    const handleListClick = (recipeId: number) => {
        navigate(RECIPE_PREVIEW_RAW + recipeId)
    }



    return (
        <Grid container sx={{ mt: 1 }}>

            <Grid item xs={3} >
                <RecipeListMenu tags={tags}
                    onTagSelectionChange={onTagSelectionChange}
                    onDifficultyLevelChange={onDifficultyLevelChange}
                    onTimeRangeChange={onTimeRangeChange}
                    onEnergyRangeChange={onEnergyRangeChange}
                />
            </Grid>

            <Grid item xs={9} >
                {
                    data?.sort((a, b) => (b.id || 0) - (a.id || 0))
                        .map((recipe: GetRecipeResponse, index: number) =>
                            <RecipeListElement
                                key={"recipe" + index}
                                data={recipe}
                                onTitleClick={handleListClick}
                            />
                        )
                }
            </Grid>

        </Grid>
    )
}

export default RecipeListContent