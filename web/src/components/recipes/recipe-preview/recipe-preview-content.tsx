import { RecipePostDirectionDto, RecipePostIngredientDto, RecipePostTagDto } from "@dietly/sdk"
import React from "react"

interface RecipePreviewContentInterface {
    title: string,
    description: string,
    imageUrl: string,
    ingredients: Array<RecipePostIngredientDto> | null | undefined
    directions: Array<RecipePostDirectionDto> | null | undefined,
    tags: Array<RecipePostTagDto> | null | undefined
}
const RecipePreviewContent = ({ title, description, imageUrl, ingredients, directions, tags }: RecipePreviewContentInterface) => {

    return (
        <div className="recipe-preview-container">

            <div className="recipe-preview-main-header">
                {title}
            </div>

            <div className="recipe-preview-icon">
                <img src={imageUrl || "/static/images/empty-image.png"} />
            </div>

            <div className="recipe-preview-decription">
                {description}
            </div>

            <div className="recipe-header recipe-preview-header">Ingredients</div>

            <ul className="recipe-ingridients-list">
                {ingredients?.map((ingredient: RecipePostIngredientDto, index: number) =>
                    (<li key={"ingr" + index} className="recipe-ingridient">{ingredient.name}</li>)
                )}
            </ul>

            <div className="recipe-header recipe-preview-header">
                Directions
            </div>

            <div className="recipe-directions">
                {directions?.sort((a, b) => (a.order || 0) - (b.order || 0))
                    .map((direction: RecipePostDirectionDto, index: number) => {
                        return (
                            <div key={"direct" + index}>
                                <div className="recipe-direction-header">
                                    Step {index + 1}
                                </div>
                                <div className="recipe-direction-text">
                                    {direction.description}
                                </div>
                            </div>
                        )
                    })}
            </div>

        </div>
    )
}

export default RecipePreviewContent
