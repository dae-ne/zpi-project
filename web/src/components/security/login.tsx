import "./security.scss"

import React, { useState } from 'react';
import { AccessTokenResponse, AccountService, LoginRequest, OpenAPI } from '../../sdk';
import { useNavigate } from 'react-router-dom';
import { RECIPE_LIST, SECURITY_REGISTER } from '../../constants/app-route';
import Copyright from './copyright';
import { setLoginCookies } from '../../tools/security';

import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';

const LoginPage = () => {
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [errorMessage, setErrorMessage] = useState<string>("")

    const navigate = useNavigate();

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const loginRequest: LoginRequest = {
            email: login,
            password: password,
        }

        AccountService.login(false, false, loginRequest).then((response: AccessTokenResponse) => {
            setErrorMessage("")
            handleLoginSuccess(response)
        }).catch(() => {
            setErrorMessage("Wrong password or email")
        })
    };

    const handleLoginSuccess = (response: AccessTokenResponse) => {
        if (!response.accessToken) return;

        setLoginCookies(response)

        OpenAPI.TOKEN = response.accessToken;
        navigate(RECIPE_LIST);
    }

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
                    Log In
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
                        fullWidth
                        label="Password"
                        type="password"
                        color="primary"
                    />

                    {errorMessage != "" ? <div className="login-error">{errorMessage}</div> : null}

                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2, backgroundColor: 'primary' }} >
                        Log In
                    </Button>

                    <Link href={SECURITY_REGISTER} variant="body2" color="secondary">
                        {"Don't have an account? Sign Up"}
                    </Link>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />

        </Container >
    );
}

export default LoginPage