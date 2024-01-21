import React from "react"
import { RecipeEditDataInterface } from "./recipe-edit-content";
import DeleteIcon from '@mui/icons-material/Delete';
import { RecipePostTagDto } from "@dietly/sdk";

const RecipeEditTags = ({ data, onDataChange }: RecipeEditDataInterface<RecipePostTagDto>) => {
    const handleDelete = (index: number) => {
        if (!data || data.length - 1 < index) return;

        onDataChange(data.toSpliced(index, 1));
    }

    return (
        <>
            <div className="recipe-header recipe-edit-tag-header">Tags</div>

            {data && data.length > 0 ?
                <ul className="recipe-ingridients-list recipe-ingridients-edit-list">

                    {data.map((tag: RecipePostTagDto, index: number) => {
                        return (
                            <li className="recipe-ingridient recipe-ingridient-edit" key={"ingr" + index}>
                                <div className="recipe-ingridient-label">{tag.name}
                                </div>
                                <div className="recipe-edit-icon-group">
                                    <div className="recipe-edit-icon-wrapper">
                                        <DeleteIcon onClick={() => handleDelete(index)} />
                                    </div>
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
