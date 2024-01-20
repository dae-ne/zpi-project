import React from "react";
import { RecipeEditDataInterface } from "./recipe-edit-content";
import DeleteIcon from '@mui/icons-material/Delete';
import { RecipePostIngredientDto } from "@dietly/sdk";

const RecipeEditIngredients = ({ data, onDataChange }: RecipeEditDataInterface<RecipePostIngredientDto>) => {

    const handleDelete = (index: number) => {
        if (!data || data.length - 1 < index) return;

        onDataChange(data.toSpliced(index, 1));
    }

    return (
        <>
            <div className="recipe-header">Ingredients</div>

            {data && data.length > 0 ?
                <ul className="recipe-ingridients-list recipe-ingridients-edit-list">

                    {data.map((ingredient: RecipePostIngredientDto, index: number) => {
                        return (
                            <li className="recipe-ingridient recipe-ingridient-edit" key={"ingr" + index}>
                                <div className="recipe-ingridient-label">{ingredient.name}
                                </div>
                                <div className="recipe-edit-icon-group">
                                    {/* <ExpandLessIcon onClick={() => onPositionChange(index, -1)} />
                                    <ExpandMoreIcon onClick={() => onPositionChange(index, 1)} /> */}
                                    <div className="recipe-edit-icon-wrapper">
                                        <DeleteIcon onClick={() => handleDelete(index)} />
                                    </div>
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
