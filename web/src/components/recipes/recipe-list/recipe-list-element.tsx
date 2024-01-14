import React from "react"
import { GetRecipeResponse, GetRecipeTagResponse } from "@dietly/sdk"
import { Box } from "@mui/material"

interface RecipeListElementInterface {
    data: GetRecipeResponse,
    index: number,
    isOutlined?: boolean,
    onTileClick: (index: number) => void
}
const MAX_DESCRIPTION_LENGTH: number = 250
const RecipeListElement = ({ data, index, onTileClick, isOutlined }: RecipeListElementInterface) => {


    return (
        <Box height={220} className="recipe-list-item" sx={{ outline: isOutlined ? "2px #50C655 solid" : "none" }} onClick={() => onTileClick(index)}>

            <Box width={600} className="recipe-list-item-image">
                <img src={data.imageUrl || "/static/images/empty-image.png"} />
            </Box>

            <Box className="recipe-list-item-content">
                <div className="recipe-list-item-header">
                    {data.title}
                </div>
                <div className="recipe-list-item-energy">{data.calories} kcal.</div>
                <div className="recipe-list-item-description">
                    {data?.description && data?.description?.length > MAX_DESCRIPTION_LENGTH
                        ? data.description.substring(0, MAX_DESCRIPTION_LENGTH) + "..." : data.description}
                </div>


                <div className="recipe-list-item-tags">
                    {data.tags?.map((tag: GetRecipeTagResponse, index: number) =>
                        (<div key={"tag" + index} className="recipe-list-item-tag">{tag.name}</div>))}
                </div>
            </Box>
        </Box>
    )
}

export default RecipeListElement
