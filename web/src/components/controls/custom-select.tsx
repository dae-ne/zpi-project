
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from "@mui/material"

import appTheme from "../theme"
import React, { useState } from "react"
import { DifficultyLevel } from "../../sdk"

interface CustomSelect<T> {
    name: string,
    placeholder: string,
    values: Array<T>,
    fullWidth?: boolean,
    value: T,
    setValue: (e: T) => void
}

const CustomSelect = (props: CustomSelect<DifficultyLevel>) => {
    const { name, values, fullWidth, value, setValue } = props

    const handleChange = (event: SelectChangeEvent) => {
        setValue(event.target.value as unknown as DifficultyLevel);
    };

    return (
        <FormControl fullWidth={fullWidth} size="small">
            {/* <InputLabel id={"selectbox-" + props.name} sx={{ fontSize: "1em" }}>{props.placeholder}</InputLabel> */}
            <Select
                labelId={"selectbox-" + name}
                color="primary"
                value={value.toString()}
                onChange={handleChange}
                name={name}
                sx={{
                    input: { fontSize: "1em" },
                    "& fieldset": { border: 'none' },
                    backgroundColor: "white"
                }}
            >
                {
                    values.map((value) => {
                        return (
                            <MenuItem key={value} value={value}>{value}</MenuItem>
                        )
                    })
                }
            </Select>
        </FormControl>

    )
}

export default CustomSelect