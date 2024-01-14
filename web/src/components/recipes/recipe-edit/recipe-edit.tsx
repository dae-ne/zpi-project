import React, { useEffect, useState } from "react"
import "./recipe-edit.scss"
import RecipeEditContent from "./recipe-edit-content"
import RecipeEditStats from "./recipe-edit-stats"
import Grid from "@mui/material/Grid"
import { useLocation, useNavigate, useParams } from "react-router-dom"
import { RECIPE_LIST, RECIPE_NEW } from "../../../constants/app-route"
import { DifficultyLevel, ImagesService, RecipeGetResponse, RecipePostDirectionDto, RecipePostIngredientDto, RecipePostRequest, RecipePostTagDto, RecipesService } from "@dietly/sdk"

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
    const params = useParams();

    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [difficultyLevel, setDifficultyLevel] = useState<DifficultyLevel>(DifficultyLevel._0);
    const [imageUrl, setImageUrl] = useState<string>("");
    const [imageFile, setImageFile] = useState<File | null>(null);
    const [time, setTime] = useState<number>(0);
    const [calories, setCalories] = useState<number>(0);
    const [ingredients, setIngredients] = useState<Array<RecipePostIngredientDto>>([]);
    const [directions, setDirections] = useState<Array<RecipePostDirectionDto>>([]);
    const [tags, setTags] = useState<Array<RecipePostTagDto>>([]);

    const [mode, setMode] = useState<Mode>()
    const submitForm = () => {
        const recipe: RecipePostRequest = {
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
        mode === Mode.New ? addRecipe(recipe) : saveRecipe(recipe)
    }

    const addRecipe = async (addRecipeRequest: RecipePostRequest) => {

        if (!imageFile) {
            alert("No selected image");
            return;
        }

        addRecipeRequest.imageUrl = await ImagesService.addFoodImage({ file: imageFile })
            .then((response: string) => response)
            .catch(() => "")

        RecipesService.createRecipe(addRecipeRequest)
            .then(() => {
                alert("Recipe has been created.")
                navigate(RECIPE_LIST)
            })
            .catch(() => {
                alert("Not all fields was fulfilled")
            })
    }

    const saveRecipe = (recipeData: RecipePostRequest) => {
        //TODO   brak metody do edycji
        // UpdateRecipeRequest = {
        //     id?: number;
        //     title?: string | null;
        //     description?: string | null;
        //     difficultyLevel?: DifficultyLevel;
        //     imageUrl?: string | null;
        //     time?: number;
        //     calories?: number;
        //     ingredients?: Array<UpdateRecipeIngredientDto> | null;
        //     directions?: Array<UpdateRecipeDirectionDto> | null;
        //     tags?: Array<UpdateRecipeTagDto> | null;
        //   };
        const recipe: RecipePostRequest = {
            title: title,
            description: description,
            //difficultyLevel: difficultyLevel,
            // imageUrl: imageUrl,
            // time: time,
            // calories: calories,
            // ingredients: ingredients,
            // directions: directions,
            // tags: tags
        }
        RecipesService.updateRecipe(parseInt(params.id || ""), recipe)
            .then((response) => {
                console.log(response)
            }).catch((err) => {
                console.log(err)

            })
    }

    const loadRecipeData = () => {
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
            .then((result: RecipeGetResponse) => {
                console.log(result)
                setTitle(result.title || "")
                setDescription(result.description || "")
                setIngredients(result.ingredients || [])
                setDirections(result.directions || [])
                setImageUrl(result.imageUrl || "")
                setDifficultyLevel(result.difficultyLevel || DifficultyLevel._0)
                setTime(result.time || 0)
                setCalories(result.calories || 0)
                setTags(result.tags || [])
            })
    }

    useEffect(() => {
        setMode(location.pathname == RECIPE_NEW ? Mode.New : Mode.Edit)
    }, [])

    useEffect(() => {
        if (mode === Mode.Edit) loadRecipeData()
    }, [mode])

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
