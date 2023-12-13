import React from "react"
import DeleteIcon from '@mui/icons-material/Delete';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const RecipeEditDirections = () => {
    return (
        <>
            <div className="recipe-header recipe-edit-header">
                Directions
            </div>

            <div className="recipe-directions">
                <div className="recipe-direction-header recipe-direction-header-edit">
                    <div>Step 1</div>
                    <div>
                        <ExpandLessIcon />
                        <ExpandMoreIcon />
                        <DeleteIcon />
                    </div>
                </div>
                <div className="recipe-direction-text">
                    Preheat the oven to 350 degrees F (175 degrees C).
                </div>
                <div className="recipe-direction-header recipe-direction-header-edit">
                    <div>Step 2</div>
                    <div>
                        <ExpandLessIcon />
                        <ExpandMoreIcon />
                        <DeleteIcon />
                    </div>
                </div>
                <div className="recipe-direction-text">
                    Place flour in a shallow plate or bowl and season with salt and pepper to taste.
                    Put bread crumbs in another shallow plate or bowl and beat eggs in another bowl.
                </div>
                <div className="recipe-direction-header recipe-direction-header-edit">
                    <div>Step 3</div>
                    <div>
                        <ExpandLessIcon />
                        <ExpandMoreIcon />
                        <DeleteIcon />
                    </div>
                </div>
                <div className="recipe-direction-text">
                    Dredge chicken piece by piece in the flour, then the egg, then the bread crumbs, until all pieces are coated.
                </div>
            </div>
        </>
    )
}

export default RecipeEditDirections