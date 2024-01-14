
import React, { useState } from "react"
import CustomTextField from "../../controls/custom-text-field"
import RecipeEditIngredients from "./recipe-edit-ingredients";
import RecipeEditDirections from "./recipe-edit-directions";
import { CreateRecipeDirectionDto, CreateRecipeIngredientDto } from "@dietly/sdk";
import AddIcon from '@mui/icons-material/Add';

const inputStyleOne = { borderRadius: "5px", fontSize: "0.9em" }
const inputStyleTwo = { borderRadius: "5px 0 0 5px", fontSize: "0.9em" }


interface RecipeEditContentInterface {
    title: string,
    description: string,
    ingredients: Array<CreateRecipeIngredientDto> | null,
    directions: Array<CreateRecipeDirectionDto> | null,
    onTitleChange: (value: string) => void,
    onDescriptionChange: (value: string) => void,
    onIngredientsChange: (value: Array<CreateRecipeIngredientDto>) => void,
    onDirectionsChange: (value: Array<CreateRecipeDirectionDto>) => void
    onSubmit: () => void
}

export interface RecipeEditDataInterface<T> {
    data: Array<T> | null,
    onDataChange: (value: Array<T>) => void
}


const RecipeEditContent = (props: RecipeEditContentInterface) => {

    const { title, description, ingredients, directions,
        onTitleChange, onDescriptionChange, onIngredientsChange, onDirectionsChange,
        onSubmit } = props

    const [ingredient, setIngredient] = useState<string>("")
    const [direction, setDirection] = useState<string>("")

    const handleAddIngredient = () => {
        if (!ingredient) return;

        const igredientsTmp: Array<CreateRecipeIngredientDto> = ingredients ? ingredients : new Array<CreateRecipeIngredientDto>();
        igredientsTmp.push({ name: ingredient } as CreateRecipeIngredientDto);
        onIngredientsChange(igredientsTmp);
        setIngredient("")
    }


    const handleAddDirection = () => {
        if (!direction) return;
        console.log(direction)
        const directionTmp: Array<CreateRecipeDirectionDto> = directions ? directions : new Array<CreateRecipeDirectionDto>();
        directionTmp.push({ description: direction, order: directionTmp.length } as CreateRecipeDirectionDto);
        onDirectionsChange(directionTmp);
        setDirection("")
    }


    return (
        <div className="recipe-edit-container">

            <div className="recipe-edit-main-header">New recipe</div>

            <div className="recipe-edit-sub-header">Name</div>

            <CustomTextField
                name="recipe-name"
                placeholder="Recipe name..."
                inputStyles={inputStyleOne}
                value={title}
                onChange={onTitleChange}
                fullWidth={true} />

            <div className="recipe-edit-sub-header">Description</div>

            <CustomTextField
                name="description"
                placeholder="Description (max 2000 letters)"
                inputStyles={inputStyleOne}
                fullWidth={true}
                rows={8}
                maxLength={2000}
                value={description}
                onChange={onDescriptionChange} />

            <div className="recipe-edit-sub-header">Ingredients</div>

            <div className="recipe-edit-ingredients-add">
                <div className="recipe-edit-ingredients-textbox">
                    <CustomTextField
                        name="ingedient-add"
                        placeholder="Ingedient name"
                        inputStyles={inputStyleTwo}
                        fullWidth={true}
                        value={ingredient}
                        onChange={setIngredient} />
                </div>
                <div className="button-std add-button recipe-edit-ingredients-button" onClick={handleAddIngredient} >
                    <AddIcon />
                </div>
            </div>


            <div className="recipe-edit-sub-header">Directions</div>

            <CustomTextField
                name="direction"
                placeholder="Step description (max 1000 letters)"
                inputStyles={inputStyleOne}
                fullWidth={true}
                rows={8}
                maxLength={1000}
                value={direction}
                onChange={setDirection} />

            <div className="button-std add-button recipe-edit-direction-add" onClick={handleAddDirection}>
                <AddIcon />
            </div>

            <RecipeEditIngredients
                data={ingredients}
                onDataChange={onIngredientsChange}
            />

            <RecipeEditDirections
                data={directions}
                onDataChange={onDirectionsChange}
            />

            <div className="button-std recipe-edit-save" onClick={onSubmit}>Save</div>

        </div>
    )
}

export default RecipeEditContent
