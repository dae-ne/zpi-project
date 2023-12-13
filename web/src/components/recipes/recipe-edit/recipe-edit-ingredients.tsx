import React from "react"
import DeleteIcon from '@mui/icons-material/Delete';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const RecipeEditIngredients = () => {
    return (
        <>
            <div className="recipe-header">Ingredients</div>
            <ul className="recipe-ingridients-list recipe-ingridients-edit-list">
                <li className="recipe-ingridient recipe-ingridient-edit">
                    <div className="recipe-ingridient-label">Ingredient 1
                    </div>
                    <div className="recipe-ingridient-icons">
                        <ExpandLessIcon />
                        <ExpandMoreIcon />
                        <DeleteIcon />
                    </div>
                </li>
                <li className="recipe-ingridient recipe-ingridient-edit">
                    <div className="recipe-ingridient-label">Ingredient 2
                    </div>
                    <div className="recipe-ingridient-icons">
                        <ExpandLessIcon />
                        <ExpandMoreIcon />
                        <DeleteIcon />
                    </div>
                </li>
                <li className="recipe-ingridient recipe-ingridient-edit">
                    <div className="recipe-ingridient-label">Ingredient 2
                    </div>
                    <div className="recipe-ingridient-icons">
                        <ExpandLessIcon />
                        <ExpandMoreIcon />
                        <DeleteIcon />
                    </div>
                </li>
            </ul>
        </>
    )
}

export default RecipeEditIngredients