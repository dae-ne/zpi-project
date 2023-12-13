import { FormGroup, FormControlLabel, Checkbox, Box, Slider } from "@mui/material"

import React from "react"

const RecipeListMenu = () => {

    const [value, setValue] = React.useState<number[]>([20, 37]);
    const handleChange = (event: Event, newValue: number | number[]) => {
        setValue(newValue as number[]);
    };
    const checkBoxStyles = { stroke: "#ffffff", strokeWidth: 0.7 }

    return (
        <div className="recipe-search-menu">
            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Difficulty level
            </Box>

            <FormGroup sx={{ mb: 3 }}>
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="Easy" />
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="More effort" />
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="Pro" />
            </FormGroup>


            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Time
            </Box>
            <Slider
                size="small"
                valueLabelDisplay="auto"
                value={value}
                onChange={handleChange}
                sx={{ mb: 3 }}
            />

            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Energy
            </Box>
            <Slider
                size="small"
                valueLabelDisplay="auto"
                value={value}
                onChange={handleChange}
                sx={{ mb: 3 }}
            />
            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Tags
            </Box>
            <FormGroup sx={{ mb: 3 }}>
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="Breakfast" />
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="Lunch" />
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="Italian" />
                <FormControlLabel control={<Checkbox sx={checkBoxStyles} />} label="Soup" />
            </FormGroup>
        </div>
    )
}

export default RecipeListMenu