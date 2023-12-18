import React from "react"
import { DifficultyLevel } from "../../../sdk"
import { getDifficultyString } from "../../../sdk/models/DifficultyLevel";

interface RecipePreviewStatsInterface {
    difficultyLevel: DifficultyLevel,
    time: number,
    calories: number
}
const RecipePreviewStats = ({ difficultyLevel, time, calories }: RecipePreviewStatsInterface) => {

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
                <div className="recipe-preview-info-grid-element-text">{getDifficultyString(difficultyLevel)}</div>
            </div>
        </div>
        <div className="recipie-preview-info-tags">

            <div className="recipe-preview-item-tag">tagk1 </div>
            <div className="recipe-preview-item-tag">tagk3 </div>
            <div className="recipe-preview-item-tag">tagk41 </div>
            <div className="recipe-preview-item-tag">tagk1532 </div>

        </div>
    </>
    )
}

export default RecipePreviewStats