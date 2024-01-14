import { Box } from "@mui/material"
import React, { useState } from "react"
import DeleteIcon from '@mui/icons-material/Delete';
import CheckIcon from '@mui/icons-material/Check';
import RemoveIcon from '@mui/icons-material/Remove';
import { GetMealResponse, MealsService, UpdateMealRequest } from "@dietly/sdk";
import moment from "moment";

const MAX_DESCRIPTION_LENGTH: number = 180
interface MealInterface {
    data: GetMealResponse,
    onDelete: () => void
}
const Meal = ({ data, onDelete }: MealInterface) => {
    //{ data, onTitleClick }: RecipeListElementInterface
    const [mealCompleted, setMealCompleted] = useState<boolean>(data?.completed || false)

    const handleMealComplete = () => {
        if (!data?.id)
            return;

        const commitMealData: UpdateMealRequest = {
            recipeId: data?.recipeId,
            date: moment(data.date, "YYYY-DD-MM").format("DD.MM.YYYY"),
            completed: true
        }

        MealsService.updateMeal(data?.id, commitMealData)
            .then((response: any) => {
                console.log(response)
            })
            .catch((err) => {
                console.log(err)
            })
    }

    const handleMealRollbackComplete = () => {
        //todo
    }

    const handleMealDelete = () => {
        if (!data?.id) return;

        MealsService.removeMeal(data.id)
            .then(() => {
                onDelete()
            })
    }

    return (
        <Box height={220} className="recipe-list-item-plans recipe-list-item" style={{ cursor: "auto" }}>

            <Box width={600} className="recipe-list-item-image">
                <img src={data?.recipe?.imageUrl || "/static/images/empty-image.png"} />
            </Box>

            <Box className="recipe-list-item-content">
                <div className="recipe-list-item-header">
                    {data?.recipe?.title}
                </div>
                <div className="recipe-list-item-energy">{data?.recipe?.calories} kcal.</div>
                <div className="recipe-list-item-energy">{data?.recipe?.time} min.</div>
                <div className="recipe-list-item-description">
                    {
                        data?.recipe?.description && data?.recipe?.description?.length > MAX_DESCRIPTION_LENGTH
                            ? data?.recipe?.description.substring(0, MAX_DESCRIPTION_LENGTH) + "..." : data?.recipe?.description
                    }
                </div>
            </Box>
            <Box className="recipe-list-item-managment">
                {mealCompleted ?
                    <RemoveIcon fontSize="small" htmlColor="orange" onClick={handleMealRollbackComplete} />
                    : <CheckIcon fontSize="small" onClick={handleMealComplete} />
                }
                <DeleteIcon fontSize="small" onClick={handleMealDelete} />
            </Box>
        </Box>
    )
}

export default Meal
