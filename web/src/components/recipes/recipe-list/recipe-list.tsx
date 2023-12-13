import React from "react"
import RecipeListHeader from "./recipe-list-header"
import RecipeListContent from "./recipe-list-content"
import "./recipe-list.scss"

const RecipeList = () => {
    return (
        <>
            <RecipeListHeader />
            <RecipeListContent />
        </>
    )
}

export default RecipeList