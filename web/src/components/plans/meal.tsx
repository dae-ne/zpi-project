import React, { useEffect, useState } from "react"
import { MealGetResponse, MealsService, MealPutRequest } from "@dietly/sdk";
import { DefaultDateTimeFormat, DefaultShortTimeFormat } from "../../constants/time";
import moment from "moment";
import { Box } from "@mui/material"
import DeleteIcon from '@mui/icons-material/Delete';
import CheckIcon from '@mui/icons-material/Check';
import RemoveIcon from '@mui/icons-material/Remove';

const MAX_DESCRIPTION_LENGTH: number = 180
interface MealInterface {
    data: MealGetResponse,
    onReload: () => void
}

const Meal = ({ data, onReload }: MealInterface) => {
    const [mealCompleted, setMealCompleted] = useState<boolean>(data?.completed || false)

    const handleMealUpdate = (completed: boolean) => {
        if (!data?.id)
            return;

        const commitMealData: MealPutRequest = {
            id: data.id,
            recipeId: data?.recipeId,
            date: moment.utc(data.date).format(DefaultDateTimeFormat),
            completed: completed
        }
        MealsService.updateMeal(data?.id, commitMealData)
            .then(() => {
                setMealCompleted(value => !value)
                onReload()
            })
            .catch(() => {
                alert("bład")
            })
    }

    const handleMealDelete = () => {
        if (!data?.id) return;

        MealsService.removeMeal(data.id)
            .then(() => {
                onReload()
            })
    }

    useEffect(() => {
        setMealCompleted(data?.completed || false)
    }, [data])

    return (
        <Box height={220} className="recipe-list-item-plans recipe-list-item" style={{ cursor: "auto" }}>

            <Box width={600} className="recipe-list-item-image">
                <img src={data?.recipe?.imageUrl || "/static/images/empty-image.png"} />
            </Box>

            <Box className="recipe-list-item-content">
                <div className="recipe-list-item-header">
                    {data?.recipe?.title}
                </div>
                <div className="recipe-list-item-description">
                    {
                        data?.recipe?.description && data?.recipe?.description?.length > MAX_DESCRIPTION_LENGTH
                            ? data?.recipe?.description.substring(0, MAX_DESCRIPTION_LENGTH) + "..." : data?.recipe?.description
                    }
                </div>
            </Box>
            <Box className="recipe-list-item-additional">
                <div className="recipe-list-item-time">
                    {moment.utc(data?.date).format(DefaultShortTimeFormat)}
                </div>
                <div>
                    <div className="recipe-list-item-energy">{data?.recipe?.calories} kcal.</div>
                    <div className="recipe-list-item-energy">{data?.recipe?.time} min.</div>
                </div>
            </Box>

            <Box className="recipe-list-item-managment">
                {mealCompleted ?
                    <RemoveIcon fontSize="small" htmlColor="orange" onClick={() => handleMealUpdate(false)} />
                    : <CheckIcon fontSize="small" onClick={() => handleMealUpdate(true)} />
                }
                <DeleteIcon fontSize="small" onClick={handleMealDelete} />
            </Box>
        </Box>
    )
}

export default Meal
