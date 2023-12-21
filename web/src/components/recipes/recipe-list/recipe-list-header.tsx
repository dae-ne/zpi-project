import { Grid } from "@mui/material"
import React, { useState } from "react"

import AddIcon from '@mui/icons-material/Add';
import CustomTextField from "../../controls/custom-text-field";
import { useNavigate } from "react-router-dom";
import { RECIPE_NEW } from "../../../constants/app-route";

interface RecipeListHeaderInterface {
    onSearchSubmit: (value: string) => void
}

const RecipeListHeader = ({ onSearchSubmit }: RecipeListHeaderInterface) => {
    const navigate = useNavigate();

    const [searchValue, setSearchValue] = useState<string>("")

    const handleNewRecipe = () => {
        navigate(RECIPE_NEW);
    }

    return (
        <Grid container>
            <Grid item xs={3} />

            <Grid item xs={8} sx={{ py: 3, flexGrow: 1, display: 'flex' }}>
                <CustomTextField
                    name="email"
                    placeholder="Recipe name..."
                    inputStyles={{ borderRadius: "5px 0 0 5px" }}
                    value={searchValue}
                    onChange={setSearchValue}
                />

                <div className="button-std search-button" onClick={() => onSearchSubmit(searchValue)}>Search</div>
            </Grid>

            <Grid item xs={1} sx={{ pt: 3 }} display="flex" justifyContent="flex-end" color="white">
                <div className="button-std add-button" onClick={handleNewRecipe}>
                    <AddIcon />
                </div>
            </Grid>
        </Grid>

    )
}

export default RecipeListHeader