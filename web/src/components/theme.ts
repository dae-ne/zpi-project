import createTheme from "@mui/material/styles/createTheme";

const appTheme = createTheme({
    palette: {
        background: {
            paper: '#F0F0F0',
        },
        primary: {
            main: '#50C655',
            contrastText: "##ffffff",
        },
        secondary: {
            main: '#101210'
        },

    },
    typography: {
        allVariants: {
            fontFamily: "'Montserrat', sans-serif",
            textTransform: "none",
        },
        "fontFamily": `"Montserrat", "Helvetica", "Arial", sans-serif`,
        "fontWeightRegular": 400,
        "fontSize": 16
    }
})

export default appTheme 