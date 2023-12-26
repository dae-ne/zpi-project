import { Box } from "@mui/material"
import React from "react"
import DeleteIcon from '@mui/icons-material/Delete';

const MAX_DESCRIPTION_LENGTH: number = 180
const PlansListElement = () => {
    //{ data, onTitleClick }: RecipeListElementInterface

    return (
        <Box height={220} className="recipe-list-item recipe-list-item-plans">

            <Box width={600} className="recipe-list-item-image">
                {/* <img src={data.imageUrl || "/static/images/empty-image.png"} /> */}
                <img src={"/static/images/empty-image.png"} />
            </Box>

            <Box className="recipe-list-item-content">
                <div className="recipe-list-item-header">
                    {/* {data.title} */}Tytul przepisu
                </div>
                <div className="recipe-list-item-energy">{/*data.calories*/}150 kcal.</div>
                <div className="recipe-list-item-energy">{/*data.calories*/}60 min.</div>
                <div className="recipe-list-item-description">
                    {"jakis testowy opis do podgladu dwdiaomosci dlugi dugi glodu kasdojsa dsa dsad sasa dasd sad sadasdas asd lorem ipsum et eceter dolor"
                        // data?.description && data?.description?.length > MAX_DESCRIPTION_LENGTH
                        //     ? data.description.substring(0, MAX_DESCRIPTION_LENGTH) + "..." : data.description
                    }
                </div>
            </Box>
            <Box className="recipe-list-item-managment">
                {/* <CheckIcon fontSize="small" /> */}
                <DeleteIcon fontSize="small" />
            </Box>
        </Box>
    )
}

export default PlansListElement