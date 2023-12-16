import React, { useState } from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { AccessTokenResponse, AccountService, LoginRequest, OpenAPI } from '../../sdk';
import Cookies from 'universal-cookie';
import "./security.scss"
import { useNavigate } from 'react-router-dom';
import { ACCESS_TOKEN_NAME, REFRESH_TOKEN_NAME } from '../../constants/cookies';
import { RECIPE_LIST, ROOT } from '../../constants/app-route';


const Copyright = (props: any) => {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="https://mui.com/">
                Your Website
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}
const REFRESH_TOKEN_MAX_AGE = 604800
const LoginPage = () => {
    const [login, setLogin] = useState<string>("a@a.pl")
    const [password, setPassword] = useState<string>("Dupa123!")
    const [isLoginError, setIsLoginError] = useState<boolean>(false)

    const navigate = useNavigate();
    const cookies = new Cookies()

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let loginRequest: LoginRequest = {
            email: login,
            password: password,
        }

        AccountService.login(false, false, loginRequest).then((response: AccessTokenResponse) => {
            setIsLoginError(false)
            handleLoginSuccess(response)
        }).catch(() => {
            setIsLoginError(true)
        })

    };

    const handleLoginSuccess = (response: AccessTokenResponse) => {
        if (!response.accessToken) return;

        cookies.set(
            ACCESS_TOKEN_NAME,
            response.accessToken,
            {
                maxAge: response.expiresIn,
                sameSite: "strict",
                path: ROOT
            }
        )
        cookies.set(
            REFRESH_TOKEN_NAME,
            response.refreshToken,
            {
                maxAge: REFRESH_TOKEN_MAX_AGE,
                sameSite: "strict",
                path: ROOT
            }
        )

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
                    Zaloguj się
                </Typography>
                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        value={login}
                        onChange={(e) => setLogin(e.target.value)}
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email Address"
                        name="email"
                        autoComplete="email"
                        autoFocus
                        color="primary"
                    />
                    <TextField
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                        color="primary"
                    />

                    {isLoginError && <div className="login-error">Wrong password or email</div>}

                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2, backgroundColor: 'primary' }} >
                        Zaloguj się
                    </Button>



                    <Link href="#" variant="body2" color="secondary">
                        {"Don't have an account? Sign Up"}
                    </Link>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />

        </Container >


    );
}

export default LoginPage

function jwt(accessToken: string | null | undefined) {
    throw new Error('Function not implemented.');
}
