
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from "@mui/material"

import appTheme from "../theme"
import React, { useState } from "react"

interface CustomSelect {
    name: string,
    placeholder: string,
    fullWidth?: boolean
}

const CustomSelect = (props: CustomSelect) => {
    const [value, setValue] = useState<string>("");

    const handleChange = (event: SelectChangeEvent) => {
        setValue(event.target.value as string);
    };

    return (
        <FormControl fullWidth={props.fullWidth} size="small">
            {/* <InputLabel id={"selectbox-" + props.name} sx={{ fontSize: "1em" }}>{props.placeholder}</InputLabel> */}
            <Select
                labelId={"selectbox-" + props.name}
                color="primary"
                value={value}
                onChange={handleChange}
                name={props.name}
                sx={{
                    input: { fontSize: "1em" },
                    "& fieldset": { border: 'none' },
                    backgroundColor: "white"
                }}
            >

                <MenuItem value={10}>Ten</MenuItem>
                <MenuItem value={20}>Twenty</MenuItem>
                <MenuItem value={30}>Thirty</MenuItem>
            </Select>
        </FormControl>

    )
}

export default CustomSelect