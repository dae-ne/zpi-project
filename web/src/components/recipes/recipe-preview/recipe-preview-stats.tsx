import React from "react"
import { CreateRecipeTagDto, DifficultyLevel } from "../../../sdk"
import { getDifficultyName } from "../../../sdk/models/DifficultyLevel";

interface RecipePreviewStatsInterface {
    difficultyLevel: DifficultyLevel,
    time: number,
    calories: number,
    tags: Array<CreateRecipeTagDto> | null | undefined
}
const RecipePreviewStats = ({ difficultyLevel, time, calories, tags }: RecipePreviewStatsInterface) => {

    return (<>
        <div className="recipe-stat-info recipe-preview-info-grid">
            <div className="recipe-preview-info-grid-element">
                <div className="recipe-preview-info-grid-element-header">Energy</div>
                <div className="recipe-preview-info-grid-element-text">{calories} kcal</div>
            </div>
            <div className="recipe-preview-info-grid-element">
                <div className="recipe-preview-info-grid-element-header">Time:</div>
                <div className="recipe-preview-info-grid-element-text">{time} min.</div>
            </div>
            <div className="recipe-preview-info-grid-element">
                <div className="recipe-preview-info-grid-element-header">Difficulty level:</div>
                <div className="recipe-preview-info-grid-element-text">{getDifficultyName(difficultyLevel)}</div>
            </div>
        </div>
        <div className="recipie-preview-info-tags">
            {
                tags?.map((tag: CreateRecipeTagDto, index: number) => {
                    return (<div key={"tag" + index} className="recipe-preview-item-tag">{tag.name}</div>)
                })
            }
        </div>
    </>
    )
}

export default RecipePreviewStats