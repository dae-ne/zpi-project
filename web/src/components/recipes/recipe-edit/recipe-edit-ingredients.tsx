import React from "react"
import { CreateRecipeIngredientDto } from "@dietly/sdk";
import { RecipeEditDataInterface } from "./recipe-edit-content";
import DeleteIcon from '@mui/icons-material/Delete';

const RecipeEditIngredients = ({ data, onDataChange }: RecipeEditDataInterface<CreateRecipeIngredientDto>) => {

    const handleDelete = (index: number) => {
        if (!data || data.length - 1 < index) return;

        onDataChange(data.toSpliced(index, 1));
    }

    return (
        <>
            <div className="recipe-header">Ingredients</div>

            {data && data.length > 0 ?
                <ul className="recipe-ingridients-list recipe-ingridients-edit-list">

                    {data.map((ingredient: CreateRecipeIngredientDto, index: number) => {
                        return (
                            <li className="recipe-ingridient recipe-ingridient-edit" key={"ingr" + index}>
                                <div className="recipe-ingridient-label">{ingredient.name}
                                </div>
                                <div className="recipe-ingridient-icons">
                                    {/* <ExpandLessIcon onClick={() => onPositionChange(index, -1)} />
                                    <ExpandMoreIcon onClick={() => onPositionChange(index, 1)} /> */}
                                    <DeleteIcon onClick={() => handleDelete(index)} />
                                </div>
                            </li>
                        )
                    })}


                </ul>

                : <div className="recipe-no-arraydata" >No ingredients</div>
            }
        </>
    )
}

export default RecipeEditIngredients
