import React from "react"
import { useNavigate, useParams } from "react-router-dom";
import { RECIPE_EDIT_RAW } from "../../../constants/app-route";
import EditIcon from '@mui/icons-material/Edit';
import { getDifficultyName } from "../../../tools/enums";
import { DifficultyLevel, RecipePostTagDto, RecipesService } from "@dietly/sdk";
import DeleteIcon from '@mui/icons-material/Delete';
import swal from "sweetalert";
import { fitAlert } from "../../../tools/fit-alert";

interface RecipePreviewStatsInterface {
    difficultyLevel: DifficultyLevel,
    time: number,
    calories: number,
    tags: Array<RecipePostTagDto> | null | undefined
}
const RecipePreviewStats = ({ difficultyLevel, time, calories, tags }: RecipePreviewStatsInterface) => {
    const navigate = useNavigate();
    const params = useParams();

    const handleEditRecipe = () => {
        if (!params.id) {
            return;
        }
        navigate(RECIPE_EDIT_RAW + parseInt(params.id))
    }

    const handleDeleteRecipe = () => {
        if (!params.id) {
            return;
        }

        RecipesService.removeRecipe(parseInt(params.id))
            .then(() => {
                fitAlert("Success", "Recipe successfully removed", "success")
                setTimeout(() => navigate(RECIPE_EDIT_RAW), 1000)
            }).catch(() => {
                fitAlert("Success", "Error during removing recipe", "error")
            })
    }

    return (<>
        <div className="recipe-edit-button-wrapper">
            <div className="button-std add-button recipe-edit-button" onClick={handleEditRecipe}>
                <EditIcon />
            </div>
            <div className="button-std add-button recipe-edit-button delete-button" onClick={handleDeleteRecipe}>
                <DeleteIcon htmlColor="white" />
            </div>
        </div>
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
                tags?.map((tag: RecipePostTagDto, index: number) => {
                    return (<div key={"tag" + index} className="recipe-preview-item-tag">{tag.name}</div>)
                })
            }
        </div>
    </>
    )
}

export default RecipePreviewStats
