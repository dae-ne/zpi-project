import "./recipe-list.scss"
import React, { useEffect, useState } from "react"
import RecipeListHeader from "./recipe-list-header"
import RecipeListContent from "./recipe-list-content"
import { RecipeListMode } from "../../../enums/recipe"
import { DifficultyLevel, OpenAPI, RecipeGetResponse, RecipesGetResponse, RecipesService } from "@dietly/sdk"

interface RecipeListInterface {
    mode: RecipeListMode,
    onAccept?: (value: Date, recipe: RecipeGetResponse) => void
}
const RecipeList = ({ mode, onAccept }: RecipeListInterface) => {
    const [recipes, setRecipes] = useState<Array<RecipeGetResponse> | null>(null)
    const [displayRecipes, setDisplayRecipes] = useState<Array<RecipeGetResponse> | null>(null)
    const [tags, setTags] = useState<Array<string> | null>(null)
    const [selectedRecipe, setSelectedRecipe] = useState<RecipeGetResponse | null>(null)

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
        if (!onAccept || !selectedRecipe)
            return;

        onAccept(time, selectedRecipe)
    }

    const filterByDifficultyLevel = (levels: DifficultyLevel[] | undefined): Array<RecipeGetResponse> | null => {
        if (!recipes || recipes.length == 0 || !levels)
            return recipes;

        return recipes.filter((recipe: RecipeGetResponse) => {
            return recipe.difficultyLevel && levels.indexOf((recipe.difficultyLevel)) >= 0;
        });
    }

    const filterByTime = (min: number, max: number): Array<RecipeGetResponse> | null => {
        if (!recipes || recipes.length == 0)
            return recipes;

        return recipes.filter((recipe: RecipeGetResponse) => recipe.time && recipe.time >= min && recipe.time <= max);
    }

    const filterByEnergy = (min: number, max: number): Array<RecipeGetResponse> | null => {
        if (!recipes || recipes.length == 0)
            return recipes;

        return recipes.filter((recipe: RecipeGetResponse) => recipe.calories && recipe.calories >= min && recipe.calories <= max);
    }

    const filterByName = (searchValue: string): Array<RecipeGetResponse> | null => {
        if (!recipes || recipes.length == 0 || searchValue == "")
            return recipes;

        return recipes.filter((recipe: RecipeGetResponse) => recipe.title && recipe.title.toLowerCase().includes(searchValue.toLowerCase()));
    }

    const filterByTag = (tags: string[]): Array<RecipeGetResponse> | null => {
        if (!recipes || recipes.length === 0 || tags?.length === 0)
            return recipes;

        return recipes.filter((recipe: RecipeGetResponse) => recipe.tags && recipeHasOneOfTags(recipe, tags))
    }

    const recipeHasOneOfTags = (recipe: RecipeGetResponse, selectedTagNames: string[]): boolean => {
        if (recipe.tags && recipe.tags.length > 0) {
            const tagNames = recipe.tags.map((t) => t.name);
            return tagNames.some((tag) => tag && selectedTagNames.includes(tag));
        }

        return false;
    }

    const loadTags = (recipes: Array<RecipeGetResponse>) => {
        setTags(collectUniqueTags(recipes))
    }

    const collectUniqueTags = (recipes: RecipeGetResponse[]): string[] => {
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
        if (!OpenAPI.TOKEN)
            return;

        RecipesService.getRecipes()
            .then((result: RecipesGetResponse) => {
                if (!result?.data || result?.data.length == 0)
                    return;

                const sortedData = result.data.sort((a, b) => (b.id || 0) - (a.id || 0))
                setRecipes(sortedData)
                setDisplayRecipes(sortedData)
                loadTags(sortedData)
                setSelectedRecipe(sortedData[0])
            })
            .catch(() => { })
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
