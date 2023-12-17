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
        const token = "CfDJ8LvhaPBiwK9Bod5fTvnsfw3uPzrNhFD1fSIdBxiwFUlbRgT-IQoRv0e-iM0DfaXaiT1UHxt6y0qiuH5FBC4EwW3XpL9o9-OwViv7GVLNcYBCT9w9pAyN2JM6Dxb034gLyKYHUHWaIkXtOxY4fNZwkb_LpqVTvDI50gnDbJhcxMmyaZeIM2lUwtlFM7zCX-k98BEkY0XHkrRCM5oMQ1kF4ZanrWiwC9y8aE9U0MVppduG1kQlF6nkZLzmeaz79V_ec9Lj_Kc5Uc7JBH0fGCHetwgjiifb66ZOI3SCJf4dMBI_R4oaOXJCOcXqvbx41wkgM5tvisiE3nFL-CQfbJzC-ygcHvXXV79pmNprqUQp9Z-ykmbrDqDMcPZxKC7qaz2YJ2nFsgy-51fMIOeSAT6M8h1zEsw_Vig6JnobXeFgDkt5SLLf95oKYLlbVRpQOno8XQ6JDgv3OzBgvGr5ke3VihANpXNVpA1IbQ-0o9V7UUfIjw_uLJ6CjvhyNTri6DZ9YzzalAWWehWIej9-83mq2roSIsETbMV_qsShk3SeOikrfg0SpUjK7dgDfP_uC5mWhL6i49CB_s43Nv5a_MQON9RcdvxeZDr4gJT_UquhrWeQWfsUXlg7B9tYUHVHoUCvYw"
        console.log("Load")
        fetch("http://localhost:8080/api/recipes", {
            headers: { Authorization: `Bearer ${token}` }
        })
            .then(resp => resp.json())
            .then(json => console.log(json))

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