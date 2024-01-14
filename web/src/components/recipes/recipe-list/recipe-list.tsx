import "./recipe-list.scss"
import React, { useEffect, useState } from "react"
import RecipeListHeader from "./recipe-list-header"
import RecipeListContent from "./recipe-list-content"
import { RecipesService, GetRecipesResponse, GetRecipeResponse, DifficultyLevel } from "@dietly/sdk"
import { RecipeListMode } from "../../../enums/recipe"

interface RecipeListInterface {
    mode: RecipeListMode,
    onAccept?: (value: Date, recipe: GetRecipeResponse) => void
}
const RecipeList = ({ mode, onAccept }: RecipeListInterface) => {
    const [recipes, setRecipes] = useState<Array<GetRecipeResponse> | null>(null)
    const [displayRecipes, setDisplayRecipes] = useState<Array<GetRecipeResponse> | null>(null)
    const [tags, setTags] = useState<Array<string> | null>(null)
    const [selectedRecipe, setSelectedRecipe] = useState<GetRecipeResponse | null>(null)

    const handleSearchData = (searchValue: string) => {
        setDisplayRecipes(filterByName(searchValue))
    }

    const handleDifficultyLevel = (levels: DifficultyLevel[] | undefined) => {
        setDisplayRecipes(filterByDifficultyLevel(levels))
    }

    const handleTime = (min: number, max: number) => {
        setDisplayRecipes(filterByTime(min, max))
    }

    const handleEnergy = (min: number, max: number) => {
        setDisplayRecipes(filterByEnergy(min, max))
    }

    const handleTags = (tags: string[] | undefined) => {
        if (!tags)
            return;
        setDisplayRecipes(filterByTag(tags))
    }

    const handleSelectRecipe = (time: Date) => {
        console.log(time)
        if (!onAccept || !selectedRecipe)
            return;

        onAccept(time, selectedRecipe)
    }

    const filterByDifficultyLevel = (levels: DifficultyLevel[] | undefined): Array<GetRecipeResponse> | null => {
        if (!recipes || recipes.length == 0 || !levels)
            return recipes;

        return recipes.filter((recipe: GetRecipeResponse) => {
            return recipe.difficultyLevel && levels.indexOf((recipe.difficultyLevel)) >= 0;
        });
    }

    const filterByTime = (min: number, max: number): Array<GetRecipeResponse> | null => {
        if (!recipes || recipes.length == 0)
            return recipes;

        return recipes.filter((recipe: GetRecipeResponse) => recipe.time && recipe.time >= min && recipe.time <= max);
    }

    const filterByEnergy = (min: number, max: number): Array<GetRecipeResponse> | null => {
        if (!recipes || recipes.length == 0)
            return recipes;

        return recipes.filter((recipe: GetRecipeResponse) => recipe.calories && recipe.calories >= min && recipe.calories <= max);
    }

    const filterByName = (searchValue: string): Array<GetRecipeResponse> | null => {
        if (!recipes || recipes.length == 0 || searchValue == "")
            return recipes;

        return recipes.filter((recipe: GetRecipeResponse) => recipe.title && recipe.title.toLowerCase().includes(searchValue.toLowerCase()));
    }

    const filterByTag = (tags: string[]): Array<GetRecipeResponse> | null => {
        if (!recipes || recipes.length === 0 || tags?.length === 0)
            return recipes;

        return recipes.filter((recipe: GetRecipeResponse) => recipe.tags && recipeHasOneOfTags(recipe, tags))
    }

    const recipeHasOneOfTags = (recipe: GetRecipeResponse, selectedTagNames: string[]): boolean => {
        if (recipe.tags && recipe.tags.length > 0) {
            const tagNames = recipe.tags.map((t) => t.name);
            return tagNames.some((tag) => tag && selectedTagNames.includes(tag));
        }

        return false;
    }

    const loadTags = (recipes: Array<GetRecipeResponse>) => {
        setTags(collectUniqueTags(recipes))
    }

    const collectUniqueTags = (recipes: GetRecipeResponse[]): string[] => {
        const allTags: Set<string> = new Set<string>();

        recipes.forEach(recipe => {
            if (recipe.tags) {
                recipe.tags.forEach(tag => {
                    allTags.add(tag.name || "")
                });
            }
        });

        return Array.from(allTags);
    }

    useEffect(() => {
        // const token = "CfDJ8PZUcAXGx8xNtqXZMUFayepTvBB9l1pa5zv3-gnskvLV8olLnXSGqFL7u2v5Wgim4vLkDspT04p7wcxkWlmLGt8p_DjQSS1cc9p-kdDk1XGdoLtwqT1vr-h5ZXQMv0D1X5S9dT7IUjRBpry1mdNvdSRSDMH2YCb_o91w6eVRAN_JJC1qEm7bub7UF6Jak-S_RSKqOBCfyGQIbjVXPGnPLxR3ipGhtHiBF93AOzAAjW83QRHf7Y7HRc-a9xFT9GCtbp1D_y5qNJatRfFu5LADtQe3RVyYrZcqZiRM6PbxJwxdrlbFzVpGSzP9VmjIEYUPFY7niAKNsybWXhtWNjQIv7xCfP1LdHp2r3PagfV3ux6hdCOkpN8kL85CYNvoz-j9_mFWA1VbM5j6bC9LcXolDwFCKsLvfjMa8FMuH67-pg81Er5IRYzsK4jp0UPuflOh5Zg9NJYsYjQcGnwwOh4_hcSyUIbZ9w5i9Uj6dKasfEu1eEmcf-KXFcy4OALBSSHMfEZaBLY_bLwyaaNhBBEEQQ6Bfl1jbmdr0msCJGKIFs4TKG9ICM2Zc9WOkPgvqyugzVvYHqGtXTOIvk9YMHkcLVDW7m0Z-28T2pyRazjPXbgp389WsAsFv3bM6yCFSuJEiQ"

        // fetch("http://localhost:8080/api/recipes", {
        //     headers: { Authorization: `Bearer ${token}` }
        // })
        //     .then(resp => resp.json())
        //     .then(json => console.log(json))


        RecipesService.getRecipes()
            .then((result: GetRecipesResponse) => {
                // console.log(result)
                if (!result?.data)
                    return;

                setRecipes(result.data)
                setDisplayRecipes(result.data)
                loadTags(result.data)

            })
            .catch(() => {
                //  console.log(err)
            })

    }, [])
    return (
        <>
            <RecipeListHeader
                onSearchSubmit={handleSearchData}
                onSelectRecipeSubmit={handleSelectRecipe}
                mode={mode}
            />
            <RecipeListContent
                tags={tags}
                data={displayRecipes}
                mode={mode}
                onTagSelectionChange={handleTags}
                onDifficultyLevelChange={handleDifficultyLevel}
                onTimeRangeChange={handleTime}
                onEnergyRangeChange={handleEnergy}
                onRecipeSelect={setSelectedRecipe} />
        </>
    )
}

export default RecipeList
