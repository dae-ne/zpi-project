import { Grid, TextField } from "@mui/material"
import React, { useState } from "react"

import AddIcon from '@mui/icons-material/Add';
import CustomTextField from "../../controls/custom-text-field";

interface RecipeListHeaderInterface {
    //  inputValue: string,
    //   onInput: (value: string) => void,
    onSearchSubmit: (value: string) => void

}

const RecipeListHeader = ({ onSearchSubmit }: RecipeListHeaderInterface) => {

    const [searchValue, setSearchValue] = useState<string>("")

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
                <div className="button-std add-button">
                    <AddIcon />
                </div>
            </Grid>
        </Grid>

    )
}

export default RecipeListHeader