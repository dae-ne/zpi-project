import "./recipe-preview.scss"
import React, { useEffect, useState } from "react"
import RecipePreviewStats from "./recipe-preview-stats"
import RecipePreviewContent from "./recipe-preview-content"
import { useNavigate, useParams } from "react-router-dom"
import { RECIPE_LIST } from "../../../constants/app-route"

import Grid from "@mui/material/Grid"
import { DifficultyLevel, RecipeGetResponse, RecipePostDirectionDto, RecipePostIngredientDto, RecipePostTagDto, RecipesService } from "@dietly/sdk"


const RecipePreview = () => {
    const [title, setTitle] = useState<string | null | undefined>("");
    const [description, setDescription] = useState<string | null | undefined>("");
    const [difficultyLevel, setDifficultyLevel] = useState<DifficultyLevel | null | undefined>(DifficultyLevel._0);
    const [imageUrl, setImageUrl] = useState<string | null | undefined>("");
    const [time, setTime] = useState<number | null | undefined>(60);
    const [calories, setCalories] = useState<number | null | undefined>(0);
    const [ingredients, setIngredients] = useState<Array<RecipePostIngredientDto> | null | undefined>(null);
    const [directions, setDirections] = useState<Array<RecipePostDirectionDto> | null | undefined>(null);
    const [tags, setTags] = useState<Array<RecipePostTagDto> | null | undefined>(null);

    const params = useParams()
    const navigate = useNavigate()

    useEffect(() => {

        if (!params.id) {
            navigate(RECIPE_LIST)
            return;
        }

        const recipeId = parseInt(params.id)
        if (!recipeId) {
            navigate(RECIPE_LIST)
            return;
        }

        RecipesService.getRecipe(recipeId)
            .then((response: RecipeGetResponse) => {
                setTitle(response.title);
                setDescription(response.description);
                setDifficultyLevel(response.difficultyLevel);
                setImageUrl(response.imageUrl);
                setTime(response.time);
                setCalories(response.calories);
                setIngredients(response.ingredients);
                setDirections(response.directions);
                setTags(response.tags);
            })
            .catch(() => { })
    }, [])


    return (
        <Grid container sx={{ my: 4 }}>

            <Grid item xs={8.2} >
                <RecipePreviewContent
                    title={title || ""}
                    description={description || ""}
                    imageUrl={imageUrl || ""}
                    ingredients={ingredients}
                    directions={directions}
                    tags={tags}
                />
            </Grid>

            <Grid item xs={3.8}>
                <RecipePreviewStats
                    difficultyLevel={difficultyLevel || DifficultyLevel._0}
                    time={time || 0}
                    calories={calories || 0}
                    tags={tags}
                />
            </Grid>

        </Grid>
    )
}

export default RecipePreview
