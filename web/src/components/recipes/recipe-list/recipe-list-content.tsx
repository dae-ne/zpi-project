import { Box, Grid } from "@mui/material"
import React, { useEffect, useState } from "react"
import RecipeListMenu from "./recipe-list-menu"
import RecipeListElement from "./recipe-list-element"
import { GetRecipeResponse, GetRecipesResponse, RecipesService } from "../../../sdk"
import { useNavigate } from 'react-router-dom';
import { RECIPE_PREVIEW, RECIPE_PREVIEW_RAW } from "../../../constants/app-route"



const RecipeListContent = () => {
    const [recipes, setRecipes] = useState<Array<GetRecipeResponse> | null>()

    const navigate = useNavigate();

    const handleListClick = (recipeId: number) => {
        ///recipe/preview/:id
        navigate(RECIPE_PREVIEW_RAW + recipeId)
    }

    useEffect(() => {
        console.log("Load")
        RecipesService.getRecipes()
            .then((result: GetRecipesResponse) => {
                console.log(result)
                setRecipes(result.data)
            })
            .catch((err) => {
                //  console.log(err)
            })
    }, [])

    return (
        <Grid container sx={{ mt: 1 }}>

            <Grid item xs={3} >
                <RecipeListMenu />
            </Grid>

            <Grid item xs={9} >
                {
                    recipes?.map((recipe: GetRecipeResponse, index: number) =>
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