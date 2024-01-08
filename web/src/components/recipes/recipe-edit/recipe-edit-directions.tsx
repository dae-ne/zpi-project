import React from "react"
import { RecipeEditDataInterface } from "./recipe-edit-content";
import { CreateRecipeDirectionDto } from "@dietly/sdk";
import DeleteIcon from '@mui/icons-material/Delete';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const RecipeEditDirections = ({ data, onDataChange }: RecipeEditDataInterface<CreateRecipeDirectionDto>) => {

    const handleDelete = (index: number) => {
        if (!data || data.length - 1 < index) return;

        onDataChange(data.toSpliced(index, 1));
    }

    const handleSwapPlaces = (index: number, swapWithPrevious: boolean) => {
        if (!data || index < 0 || index >= data.length) return;

        const directions = [...data];
        if (swapWithPrevious && index > 0) {
            const temp = directions[index].order;
            directions[index].order = directions[index - 1].order;
            directions[index - 1].order = temp;
        } else if (!swapWithPrevious && index < directions.length - 1) {
            const temp = directions[index].order;
            directions[index].order = directions[index + 1].order;
            directions[index + 1].order = temp;
        }

        onDataChange(directions)
    };

    return (
        <>
            <div className="recipe-header recipe-edit-header">
                Directions
            </div>

            <div className="recipe-directions">
                {data && data.length > 0 ?
                    data
                        .sort((a, b) => (a.order || 0) - (b.order || 0))
                        .map((direction: CreateRecipeDirectionDto, index: number) => {
                            return (
                                <div key={"direc" + index}>
                                    <div className="recipe-direction-header recipe-direction-header-edit">
                                        <div>Step {index + 1}</div>
                                        <div>
                                            <ExpandLessIcon onClick={() => handleSwapPlaces(index, true)} />
                                            <ExpandMoreIcon onClick={() => handleSwapPlaces(index, false)} />
                                            <DeleteIcon onClick={() => handleDelete(index)} />
                                        </div>
                                    </div>
                                    <div className="recipe-direction-text">
                                        {direction.description}
                                    </div>
                                </div>
                            )
                        })
                    : <div className="recipe-no-arraydata" >No directions</div>
                }


            </div >
        </>
    )
}

export default RecipeEditDirections
