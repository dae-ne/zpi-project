import "./security.scss"
import React, { useState } from 'react';
import { AccountService, ApiError, RegisterRequest } from '@dietly/sdk';
import { useNavigate } from 'react-router-dom';
import { SECURITY_LOGIN } from '../../constants/app-route';
import Copyright from './copyright';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { fitAlert, fitAlertShort } from "../../tools/fit-alert";

const RegisterPage = () => {
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [rePassword, setRePassword] = useState<string>("")
    const [errorMessage, setErrorMessage] = useState<string>("")

    const navigate = useNavigate();

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (password !== rePassword) {
            setErrorMessage("Passwords do not match")
            return;
        }

        const registerRequest: RegisterRequest = {
            email: login,
            password: password,
        }

        AccountService.register(registerRequest)
            .then(() => {
                fitAlertShort("Account created with success", "success")
                setErrorMessage("")
                navigate(SECURITY_LOGIN)
            })
            .catch((err: ApiError) => {
                const error = err?.body?.errors
                const firstKey = Object.keys(error)[0];
                const errorMessage = error[firstKey] || "Unexpected error"
                setErrorMessage(errorMessage)
                fitAlert("Error", errorMessage, "error")
            })

    };

    return (
        <Container component="div" maxWidth="xs">
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 25,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}>

                <Avatar sx={{ m: 2, width: 70, height: 70 }} src="/static/images/logo.png" />

                <Typography component="h1" variant="h5">
                    Sign Up
                </Typography>
                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        value={login}
                        onChange={(e) => setLogin(e.target.value)}
                        margin="normal"
                        fullWidth
                        name="email"
                        label="Email Address"
                        autoFocus
                        color="primary"
                    />
                    <TextField
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        margin="normal"
                        label="Password"
                        fullWidth
                        type="password"
                        color="primary"
                    />

                    <TextField
                        value={rePassword}
                        onChange={(e) => setRePassword(e.target.value)}
                        margin="normal"
                        label="Repeat Password"
                        fullWidth
                        type="password"
                        color="primary"
                    />

                    {errorMessage != "" ? <div className="login-error">{errorMessage}</div> : null}

                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2, backgroundColor: 'primary' }} >
                        Sign Up
                    </Button>

                    <Link href={SECURITY_LOGIN} variant="body2" color="secondary">
                        {"Do you have an account? Log In"}
                    </Link>

                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />

        </Container>

    );
}

export default RegisterPage
