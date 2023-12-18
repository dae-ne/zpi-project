import React from 'react';
import "./main.scss"
import "./recipes/recipes.scss"
import { Outlet, useLocation, Link, useNavigate } from "react-router-dom";
import appTheme from './theme';
import { AppBar, Avatar, Box, Button, Container, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from '@mui/material';
import Cookies from 'universal-cookie';
import { ACCESS_TOKEN_NAME, REFRESH_TOKEN_NAME } from '../constants/cookies';
import { AccountService, OpenAPI } from '../sdk';
import { ROOT, SECURITY_LOGIN } from '../constants/app-route';

const logout: string = "Logout"
const settings = [logout];

const pages = [
    { name: 'RECIPES', address: "/recipe/list" },
    { name: 'PLAN', address: "/plan/list" },
    { name: 'GROCERY LIST', address: "/grocery/list" }];

const Navigation = () => {
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

    const navigate = useNavigate();
    const cookies = new Cookies()

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseUserMenu = (value: string) => {
        setAnchorElUser(null);

        if (value === logout)
            handleLogOut()
    };

    const handleLogOut = () => {
        cookies.remove(ACCESS_TOKEN_NAME, { path: ROOT });
        cookies.remove(REFRESH_TOKEN_NAME, { path: ROOT });
        OpenAPI.TOKEN = undefined;
        navigate(SECURITY_LOGIN)
    }


    return (
        <>
            <AppBar position="static" style={{ background: 'white', boxShadow: "none" }}>
                <Container maxWidth="lg">
                    <Toolbar disableGutters>
                        <Box sx={{ flexGrow: 1, display: 'flex' }}>
                            {pages.map(({ name, address }: { name: string, address: string }) => (
                                <Button
                                    key={name}
                                    sx={{ my: 2, mr: 4, color: 'secondary.main', display: 'block' }}
                                >
                                    <Link to={address}
                                        style={{ borderColor: location.pathname == address ? appTheme.palette.primary.main : "transparent" }}
                                    >{name}</Link>
                                </Button>
                            ))}
                        </Box>

                        <Box sx={{ flexGrow: 0 }}>
                            <Tooltip title="Open settings">
                                <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                                    <Avatar alt="Test" src="" />
                                </IconButton>
                            </Tooltip>
                            <Menu
                                sx={{ mt: '45px' }}
                                id="menu-appbar"
                                anchorEl={anchorElUser}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={Boolean(anchorElUser)}
                                onClose={handleCloseUserMenu}
                            >
                                {settings.map((setting) => (
                                    <MenuItem key={setting}
                                        onClick={() => handleCloseUserMenu(setting)}>
                                        <Typography textAlign="center">{setting}</Typography>
                                    </MenuItem>
                                ))}
                            </Menu>
                        </Box>

                    </Toolbar>
                </Container>
            </AppBar >

            <Container maxWidth={false} >
                <Container maxWidth="lg" >
                    <Outlet />
                </Container>
            </Container >

        </>
    )
};

export default Navigation