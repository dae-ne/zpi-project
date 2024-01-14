import "./recipe-edit.scss"
import React, { useEffect, useState } from "react"
import RecipeEditContent from "./recipe-edit-content"
import RecipeEditStats from "./recipe-edit-stats"
import { useLocation, useNavigate, useParams } from "react-router-dom"
import { RECIPE_LIST, RECIPE_NEW, RECIPE_PREVIEW_RAW } from "../../../constants/app-route"
import {
    DifficultyLevel, ImagesService, RecipeGetResponse, RecipePostDirectionDto,
    RecipePostIngredientDto, RecipePostRequest, RecipePostTagDto,
    RecipePutRequest,
    RecipesService
} from "@dietly/sdk"
import Grid from "@mui/material/Grid"

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
    const [imageChanged, setImageChanged] = useState<boolean>(false);
    const [imageUrl, setImageUrl] = useState<string>("");
    const [imageFile, setImageFile] = useState<File | null>(null);
    const [time, setTime] = useState<number>(0);
    const [calories, setCalories] = useState<number>(0);
    const [ingredients, setIngredients] = useState<Array<RecipePostIngredientDto>>([]);
    const [directions, setDirections] = useState<Array<RecipePostDirectionDto>>([]);
    const [tags, setTags] = useState<Array<RecipePostTagDto>>([]);

    const [mode, setMode] = useState<Mode>()

    const submitImage = (file: File) => {
        setImageFile(file)
        setImageChanged(true)
    }

    const sendImage = async (file: File) => {
        return await ImagesService.addFoodImage({ file: file })
            .then((response: string) => response)
            .catch(() => imageUrl)
    }

    const submitForm = () => {
        if (mode === Mode.New)
            addRecipe();
        else
            saveRecipe()
    }

    const addRecipe = async () => {
        if (!imageFile) {
            alert("No selected image");
            return;
        }

        const recipe: RecipePostRequest = {
            title: title,
            description: description,
            difficultyLevel: difficultyLevel,
            imageUrl: await sendImage(imageFile),
            time: time,
            calories: calories,
            ingredients: ingredients,
            directions: directions,
            tags: tags
        }

        RecipesService.createRecipe(recipe)
            .then(() => {
                alert("Recipe has been created.")
                navigate(RECIPE_LIST)
            })
            .catch(() => {
                alert("Not all fields was fulfilled")
            })
    }

    const saveRecipe = async () => {
        if (!params.id) return;

        const recipeId = parseInt(params.id)
        if (!recipeId) return;

        const recipe: RecipePutRequest = {
            id: recipeId,
            title: title,
            description: description,
            difficultyLevel: difficultyLevel,
            imageUrl: imageUrl,
            time: time,
            calories: calories,
            ingredients: ingredients,
            directions: directions,
            tags: tags
        };

        if (imageChanged && imageFile)
            recipe.imageUrl = await sendImage(imageFile)

        RecipesService.updateRecipe(recipeId, recipe)
            .then(() => {
                alert("pomyślnie zapisano przepis")
                navigate(RECIPE_PREVIEW_RAW + recipeId)
            }).catch((err) => {
                //tu jest błąd - 500, ale łatwno go ukryć, bo występuje tylko jak obiekt nie uległ zmianie - więc mozna udawać że tak miało być :D
                alert("przepis nie uległ zmianie")
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
                    onImageChange={submitImage}
                    onTimeChange={setTime}
                    onCaloriesChange={setCalories}
                    onTagsChange={setTags}
                />
            </Grid>
        </Grid>
    )
}

export default RecipeEdit
