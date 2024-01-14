import React from "react"
import { CreateRecipeIngredientDto, CreateRecipeDirectionDto, CreateRecipeTagDto } from "@dietly/sdk"

interface RecipePreviewContentInterface {
    title: string,
    description: string,
    imageUrl: string,
    ingredients: Array<CreateRecipeIngredientDto> | null | undefined
    directions: Array<CreateRecipeDirectionDto> | null | undefined,
    tags: Array<CreateRecipeTagDto> | null | undefined
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
                {ingredients?.map((ingredient: CreateRecipeIngredientDto, index: number) =>
                    (<li key={"ingr" + index} className="recipe-ingridient">{ingredient.name}</li>)
                )}
            </ul>

            <div className="recipe-header recipe-preview-header">
                Directions
            </div>

            <div className="recipe-directions">
                {directions?.sort((a, b) => (a.order || 0) - (b.order || 0))
                    .map((direction: CreateRecipeDirectionDto, index: number) => {
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
