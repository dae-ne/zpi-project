import { Box } from "@mui/material"
import React from "react"


const RecipeListElement = () => {

    return (
        <Box height={220} className="recipe-list-item">   {/* tutaj ewnutalie cos zmienić */}

            <Box width={600} className="recipe-list-item-image">
                <img src="/static/images/tmp_image.png" />
            </Box>

            <Box className="recipe-list-item-content">
                <div className="recipe-list-item-header">
                    Crispy shredded chicken
                </div>
                <div className="recipe-list-item-energy">430kcal.</div>
                <div className="recipe-list-item-description">
                    Try this takeaway favourite served with rice, or simply on its own as part of a buffet-style meal.
                    It can work as a main course or starter to share
                </div>


                <div className="recipe-list-item-tags">
                    <div className="recipe-list-item-tag">Lunch</div>
                    <div className="recipe-list-item-tag">Tag 2</div>
                    <div className="recipe-list-item-tag">Tag 3</div>
                    <div className="recipe-list-item-tag">Tag 3</div>
                    <div className="recipe-list-item-tag">Tag23432 3</div>
                </div>
            </Box>
        </Box>
    )
}

export default RecipeListElement