import React from "react"
import { CreateRecipeTagDto } from "../../../sdk";
import { RecipeEditDataInterface } from "./recipe-edit-content";
import DeleteIcon from '@mui/icons-material/Delete';

const RecipeEditTags = ({ data, onDataChange }: RecipeEditDataInterface<CreateRecipeTagDto>) => {
    const handleDelete = (index: number) => {
        if (!data || data.length - 1 < index) return;

        onDataChange(data.toSpliced(index, 1));
    }

    return (
        <>
            <div className="recipe-header recipe-edit-tag-header">Tags</div>

            {data && data.length > 0 ?
                <ul className="recipe-ingridients-list recipe-ingridients-edit-list">

                    {data.map((tag: CreateRecipeTagDto, index: number) => {
                        return (
                            <li className="recipe-ingridient recipe-ingridient-edit" key={"ingr" + index}>
                                <div className="recipe-ingridient-label">{tag.name}
                                </div>
                                <div className="recipe-ingridient-icons">
                                    <DeleteIcon onClick={() => handleDelete(index)} />
                                </div>
                            </li>
                        )
                    })}


                </ul>

                : <div className="recipe-no-arraydata" >No tags</div>
            }
        </>
    )
}

export default RecipeEditTags