import React, { useState } from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { AccountService, LoginRequest } from '../../sdk';

function Copyright(props: any) {
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

const LoginPage = () => {

    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        console.log(login)
        // console.log({
        //     email: data.get('email'),
        //     password: data.get('password'),
        // });
        let loginRequest: LoginRequest = {
            email: login,
            password: password,
        }
        const a = AccountService.login(true, false, loginRequest)
        console.log(a)
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