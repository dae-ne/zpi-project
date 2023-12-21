import React, { useEffect, useState } from "react"
import "./recipe-edit.scss"
import RecipeEditContent from "./recipe-edit-content"
import RecipeEditStats from "./recipe-edit-stats"
import Grid from "@mui/material/Grid"
import { CreateRecipeDirectionDto, CreateRecipeIngredientDto, CreateRecipeRequest, CreateRecipeTagDto, DifficultyLevel, ImagesService, OpenAPI, RecipesService } from "../../../sdk"
import { useLocation, useNavigate } from "react-router-dom"
import { RECIPE_EDIT, RECIPE_LIST, RECIPE_NEW } from "../../../constants/app-route"
import { getDifficultyId } from "../../../sdk/models/DifficultyLevel"

export enum Mode {
    Edit,
    New
}

export interface RecipeEditInterface {
    mode: Mode
}

const RecipeEdit = () => {
    const location = useLocation();
    const navigate = useNavigate();

    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [difficultyLevel, setDifficultyLevel] = useState<DifficultyLevel>(DifficultyLevel._0);
    const [imageUrl, setImageUrl] = useState<string>("");
    const [imageFile, setImageFile] = useState<File | null>(null);
    const [time, setTime] = useState<number>(0);
    const [calories, setCalories] = useState<number>(0);
    const [ingredients, setIngredients] = useState<Array<CreateRecipeIngredientDto>>([]);
    const [directions, setDirections] = useState<Array<CreateRecipeDirectionDto>>([]);
    const [tags, setTags] = useState<Array<CreateRecipeTagDto>>([]);

    const [mode, setMode] = useState<Mode>()
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
        console.log(JSON.stringify(recipe))

        mode === Mode.New ? addRecipe(recipe) : saveRecipe(recipe)
    }

    const addRecipe = (addRecipeRequest: CreateRecipeRequest) => {

        if (!imageFile) {
            alert("No selected image");
            return;
        }

        ImagesService.addFoodImage({ file: imageFile })
            .then((response) => {
                addRecipeRequest.imageUrl = ""//TODO link z response
                RecipesService.createRecipe(addRecipeRequest)
                    .then(() => {
                        alert("Recipe has been created.")
                        //    navigate(RECIPE_LIST)
                    })
                    .catch(() => {
                        alert("Not all fields was fulfilled")
                    })

            })
    }

    const saveRecipe = (addRecipeRequest: CreateRecipeRequest) => {
        //TODO   brak metody do edycji
        //   RecipesService.editRecipe(addRecipeRequest)
    }

    useEffect(() => {
        setMode(location.pathname == RECIPE_NEW ? Mode.New : Mode.Edit)
    }, [])

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
                    onImageChange={setImageFile}
                    onTimeChange={setTime}
                    onCaloriesChange={setCalories}
                    onTagsChange={setTags}
                />
            </Grid>
        </Grid>
    )
}

export default RecipeEdit