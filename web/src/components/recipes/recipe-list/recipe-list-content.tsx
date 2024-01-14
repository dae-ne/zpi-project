import React, { useEffect, useState } from "react"
import RecipeListMenu from "./recipe-list-menu"
import RecipeListElement from "./recipe-list-element"
import { useNavigate } from 'react-router-dom';
import { RECIPE_PREVIEW_RAW } from "../../../constants/app-route"
import { RecipeListMode } from "../../../enums/recipe"
import { Grid } from "@mui/material"
import { DifficultyLevel, RecipeGetResponse } from "@dietly/sdk";

interface RecipeListContentInterface {
    data: Array<RecipeGetResponse> | null,
    tags: Array<string> | null,
    mode: RecipeListMode,
    onTagSelectionChange: (value: string[] | undefined) => void,
    onDifficultyLevelChange: (value: DifficultyLevel[] | undefined) => void,
    onTimeRangeChange: (min: number, max: number) => void,
    onEnergyRangeChange: (min: number, max: number) => void,
    onRecipeSelect?: (value: RecipeGetResponse) => void
}

const RecipeListContent = (props: RecipeListContentInterface) => {
    const { tags, data, mode, onTagSelectionChange, onDifficultyLevelChange, onTimeRangeChange, onEnergyRangeChange, onRecipeSelect } = props
    const navigate = useNavigate();

    const [selectedRecipeIndex, setSelectedRecipeIndex] = useState<number>(0)

    const handleListClick = (index: number) => {
        if (mode == RecipeListMode.View)
            redirect(index);
        else
            addRecipeToPlan(index);
    }

    const addRecipeToPlan = (index: number) => {
        setSelectedRecipeIndex(index)
    }

    const redirect = (index: number) => {
        if (!data) return;
        navigate(RECIPE_PREVIEW_RAW + (data[index].id || 0))
    }

    useEffect(() => {
        if (!onRecipeSelect || !data)
            return;
        onRecipeSelect(data[selectedRecipeIndex])

    }, [selectedRecipeIndex])
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

            <Grid item xs={9}>
                {
                    data?.map((recipe: RecipeGetResponse, index: number) =>
                        <RecipeListElement
                            key={"recipe" + index}
                            data={recipe}
                            index={index}
                            onTileClick={handleListClick}
                            isOutlined={mode == RecipeListMode.Select && index === selectedRecipeIndex}
                        />
                    )
                }
            </Grid>

        </Grid>
    )
}

export default RecipeListContent
