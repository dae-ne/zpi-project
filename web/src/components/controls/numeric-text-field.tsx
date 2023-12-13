import TextField from "@mui/material/TextField"
import appTheme from "../theme"
import React from "react"

interface NumericTextField {
    name: string,
    placeholder: string,
    fullWidth?: boolean,
}

const NumericTextField = (props: NumericTextField) => {
    const fontColor = appTheme.palette.secondary.main

    return (
        <TextField
            margin="normal"
            placeholder={props.placeholder}
            name={props.name}
            color="primary"
            fullWidth={props.fullWidth}
            size="small"
            inputProps={{ type: 'number' }}
            sx={{
                input: {
                    height: 25, backgroundColor: 'white',
                    "::placeholder": { color: fontColor, opacity: 0.6 }
                },
                "& fieldset": { border: 'none' },
                m: 0,
                backgroundColor: "white"
            }} />
    )
}

export default NumericTextField