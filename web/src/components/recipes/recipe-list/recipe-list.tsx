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

    const [filterSearchName, setFilterSearchName] = useState<string | null>(null)
    const [filterDifficultyLevel, setFilterDifficultyLevel] = useState<Array<DifficultyLevel> | null>(null)
    const [filterTime, setFilterTime] = useState<{ min: number, max: number } | null>(null)
    const [filterEnergy, setFilterEnergy] = useState<{ min: number, max: number } | null>(null)
    const [filterTags, setFilterTags] = useState<Array<string> | null>(null)


    const handleSearchName = (searchValue: string) => {
        setFilterSearchName(searchValue)
    }

    const handleDifficultyLevel = (levels: DifficultyLevel[] | undefined) => {
        setFilterDifficultyLevel(levels || null)
    }

    const handleTime = (min: number, max: number) => {
        setFilterTime({ min: min, max: max })
    }

    const handleEnergy = (min: number, max: number) => {
        setFilterEnergy({ min: min, max: max })
    }

    const handleTags = (tags: string[] | undefined) => {
        setFilterTags(tags || null)
    }

    const handleSelectRecipe = (time: Date) => {
        if (!onAccept || !selectedRecipe)
            return;

        onAccept(time, selectedRecipe)
    }

    const refreshFilters = async () => {
        if (!recipes || recipes.length == 0)
            return;
        let tmpRecipies: Array<RecipeGetResponse> = recipes

        if (filterSearchName && filterSearchName != "")
            tmpRecipies = filterByName(tmpRecipies, filterSearchName)

        if (tmpRecipies.length > 0 && filterDifficultyLevel && filterDifficultyLevel.length > 0) {
            tmpRecipies = filterByDifficultyLevel(tmpRecipies, filterDifficultyLevel)
        }

        if (tmpRecipies.length > 0 && filterTime) {
            tmpRecipies = filterByTime(tmpRecipies, filterTime.min, filterTime.max)
        }

        if (tmpRecipies.length > 0 && filterEnergy) {
            tmpRecipies = filterByEnergy(tmpRecipies, filterEnergy.min, filterEnergy.max)
        }

        if (tmpRecipies.length > 0 && filterTags && filterTags.length > 0) {
            tmpRecipies = filterByTags(tmpRecipies, filterTags)
        }

        setDisplayRecipes(tmpRecipies)
    }

    const loadTags = (recipes: Array<RecipeGetResponse>) => {
        setTags(collectUniqueTags(recipes))
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

    useEffect(() => {
        refreshFilters()

    }, [filterSearchName, filterDifficultyLevel, filterTime, filterEnergy, filterTags])

    return (
        <>
            <RecipeListHeader
                onSearchSubmit={handleSearchName}
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


const filterByDifficultyLevel = (recipes: Array<RecipeGetResponse>, levels: DifficultyLevel[]): Array<RecipeGetResponse> => {
    return recipes.filter((recipe: RecipeGetResponse) => {
        return recipe.difficultyLevel != undefined && levels.indexOf(recipe.difficultyLevel) > -1;
    });
}

const filterByTime = (recipes: Array<RecipeGetResponse>, min: number, max: number): Array<RecipeGetResponse> => {
    return recipes.filter((recipe: RecipeGetResponse) => recipe.time && recipe.time >= min && recipe.time <= max);
}

const filterByEnergy = (recipes: Array<RecipeGetResponse>, min: number, max: number): Array<RecipeGetResponse> => {
    return recipes.filter((recipe: RecipeGetResponse) => recipe.calories && recipe.calories >= min && recipe.calories <= max);
}

const filterByName = (recipes: Array<RecipeGetResponse>, searchValue: string): Array<RecipeGetResponse> => {
    return recipes.filter((recipe: RecipeGetResponse) => recipe.title && recipe.title.toLowerCase().includes(searchValue.toLowerCase()));
}

const filterByTags = (recipes: Array<RecipeGetResponse>, tags: string[]): Array<RecipeGetResponse> => {
    return recipes.filter((recipe: RecipeGetResponse) => recipe.tags && recipeHasOneOfTags(recipe, tags))
}

const recipeHasOneOfTags = (recipe: RecipeGetResponse, selectedTagNames: string[]): boolean => {
    if (recipe.tags && recipe.tags.length > 0) {
        const tagNames = recipe.tags.map((t) => t.name);
        return tagNames.some((tag) => tag && selectedTagNames.includes(tag));
    }

    return false;
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
