
import React from "react"
import CustomTextField from "../../controls/custom-text-field"
import AddIcon from '@mui/icons-material/Add';
import RecipeEditIngredients from "./recipe-edit-ingredients";
import RecipeEditDirections from "./recipe-edit-directions";

const RecipeEditContent = () => {
    const inputStyleOne = { borderRadius: "5px", fontSize: "0.9em" }
    const inputStyleTwo = { borderRadius: "5px 0 0 5px", fontSize: "0.9em" }

    return (
        <div className="recipe-edit-container">

            <div className="recipe-edit-main-header">New recipe</div>

            <div className="recipe-edit-sub-header">Name</div>

            <CustomTextField
                name="recipe-name"
                placeholder="Recipe name..."
                inputStyles={inputStyleOne}
                fullWidth={true} />

            <div className="recipe-edit-sub-header">Description</div>

            <CustomTextField
                name="description"
                placeholder="Description (max 2000 letters)"
                inputStyles={inputStyleOne}
                fullWidth={true}
                rows={8}
                maxLength={2000} />

            <div className="recipe-edit-sub-header">Ingredients</div>

            <div className="recipe-edit-ingredients-add">
                <div className="recipe-edit-ingredients-textbox">
                    <CustomTextField
                        name="ingedient-add"
                        placeholder="Ingedient name"
                        inputStyles={inputStyleTwo}
                        fullWidth={true} />
                </div>
                <div className="button-std add-button recipe-edit-ingredients-button">
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
                maxLength={1000} />

            <div className="button-std add-button recipe-edit-direction-add">
                <AddIcon />
            </div>



            <RecipeEditIngredients />

            <RecipeEditDirections />

        </div>
    )
}

export default RecipeEditContent 
