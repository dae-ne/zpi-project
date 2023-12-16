import React, { useState } from "react"
import "./recipe-edit.scss"
import RecipeEditContent from "./recipe-edit-content"
import RecipeEditStats from "./recipe-edit-stats"
import Grid from "@mui/material/Grid"
import { CreateRecipeDirectionDto, CreateRecipeIngredientDto, CreateRecipeRequest, CreateRecipeTagDto, DifficultyLevel, OpenAPI, RecipesService } from "../../../sdk"

export enum Mode {
    Edit,
    New
}

export interface RecipeEditInterface {
    mode: Mode
}

const RecipeEdit = () => {

    const mode = Mode.New;

    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [difficultyLevel, setDifficultyLevel] = useState<DifficultyLevel>(DifficultyLevel._0);
    const [imageUrl, setImageUrl] = useState<string>("");
    const [time, setTime] = useState<number>(60);
    const [calories, setCalories] = useState<number>(0);
    const [ingredients, setIngredients] = useState<Array<CreateRecipeIngredientDto> | null>(null);
    const [directions, setDirections] = useState<Array<CreateRecipeDirectionDto> | null>(null);
    const [tags, setTags] = useState<Array<CreateRecipeTagDto> | null>(null);
    console.log(console.log(OpenAPI))
    const submitForm = () => {

        const recipe: CreateRecipeRequest = {
            title: title,
            description: description,
            difficultyLevel: difficultyLevel,
            imageUrl: imageUrl,
            time: time,
            calories: calories,
            ingredients: ingredients,
            directions: directions,
            tags: tags
        }
        console.log(recipe)

        mode === Mode.New ? addRecipe(recipe) : saveRecipe(recipe)
    }

    const addRecipe = (addRecipeRequest: CreateRecipeRequest) => {
        console.log(OpenAPI)
        RecipesService.createRecipe(addRecipeRequest)
    }

    const saveRecipe = (addRecipeRequest: CreateRecipeRequest) => {
        //TODO   brak metody do edycji
        //   RecipesService.editRecipe(addRecipeRequest)
    }

    return (
        <Grid container sx={{ my: 4 }}>
            <Grid item xs={7.5} >
                <RecipeEditContent
                    title={title}
                    description={description}
                    ingredients={ingredients}
                    directions={directions}
                    onTitleChange={setTitle}
                    onDescriptionChange={setDescription}
                    onIngredientsChange={setIngredients}
                    onDirectionsChange={setDirections}
                    onSubmit={submitForm} />
            </Grid>

            <Grid item xs={4.5} >
                <RecipeEditStats
                    difficultyLevel={difficultyLevel}
                    imageUrl={imageUrl}
                    time={time}
                    calories={calories}
                    tags={tags}
                    onDifficultyLevelChange={setDifficultyLevel}
                    onImageUrlChange={setImageUrl}
                    onTimeChange={setTime}
                    onCaloriesChange={setCalories}
                    onTagsChange={setTags}
                />
            </Grid>
        </Grid>
    )
}

export default RecipeEdit