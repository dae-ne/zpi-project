import TextField from "@mui/material/TextField"
import appTheme from "../theme"
import React from "react"

interface CustomTextField {
    name: string,
    placeholder: string,
    fullWidth?: boolean,
    inputStyles?: any,
    rows?: number,
    maxLength?: number,
    onChange?: (e: any) => void,
    value?: string
}
const fontColor = appTheme.palette.secondary.main

const CustomTextField = (props: CustomTextField) => {
    return (
        <TextField
            margin="normal"
            placeholder={props.placeholder}
            name={props.name}
            color="primary"
            fullWidth={props.fullWidth}
            size="small"
            value={props.value}
            onChange={(e) => props.onChange && props.onChange(e.target.value)}
            multiline={(props.rows && props.rows > 0) ? true : false}
            rows={props.rows}
            inputProps={{ maxLength: props.maxLength ? props.maxLength : 2000 }}
            sx={{
                input: {
                    height: 25, backgroundColor: 'white',
                    "::placeholder": { color: fontColor, opacity: 0.6 },
                    ...props.inputStyles
                },
                textarea: {
                    height: 25, backgroundColor: 'white',
                    "::placeholder": { color: fontColor, opacity: 0.6 },
                    ...props.inputStyles
                },
                "& fieldset": { border: 'none' },
                m: 0,
                backgroundColor: "white",
                ...props.inputStyles
            }} />
    )
}

export default CustomTextField