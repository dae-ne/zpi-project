import React from "react"

const RecipePreviewContent = () => {

    return (
        <div className="recipe-preview-container">

            <div className="recipe-preview-main-header">
                Crispy shredded chicken
            </div>

            <div className="recipe-preview-icon">
                <img src="/static/images/tmp_image.png" />
            </div>

            <div className="recipe-preview-decription">
                Try this takeaway favourite served with rice, or simply on its own as part of a buffet-style meal.
                It can work as a main course or starter to share
            </div>

            <div className="recipe-header recipe-preview-header">Ingredients</div>
            {/* <DeleteIcon sx={{ cursor: "pointer" }} /> */}

            <ul className="recipe-ingridients-list">
                <li className="recipe-ingridient">Ingredient 1</li>
                <li className="recipe-ingridient">Ingredient 2</li>
                <li className="recipe-ingridient">Ingredient 3</li>
            </ul>

            <div className="recipe-header recipe-preview-header">
                Directions
            </div>

            <div className="recipe-directions">
                <div className="recipe-direction-header">
                    Step 1
                </div>
                <div className="recipe-direction-text">
                    Preheat the oven to 350 degrees F (175 degrees C).
                </div>
                <div className="recipe-direction-header">
                    Step 2
                </div>
                <div className="recipe-direction-text">
                    Place flour in a shallow plate or bowl and season with salt and pepper to taste.
                    Put bread crumbs in another shallow plate or bowl and beat eggs in another bowl.
                </div>
                <div className="recipe-preview-direction-header">
                    Step 3
                </div>
                <div className="recipe-direction-text">
                    Dredge chicken piece by piece in the flour, then the egg, then the bread crumbs, until all pieces are coated.
                </div>
            </div>

        </div>
    )
}

export default RecipePreviewContent