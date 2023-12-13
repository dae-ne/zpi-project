import React from 'react';
import "./main.scss"
import "./recipes/recipes.scss"
import { Outlet, useLocation, Link } from "react-router-dom";
import appTheme from './theme';
import { AppBar, Avatar, Box, Button, Container, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from '@mui/material';

const Navigation = () => {
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);
    const location = useLocation()

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const pages = [
        { name: 'RECIPES', address: "/recipe/list" },
        { name: 'PLAN', address: "/plan/list" },
        { name: 'GROCERY LIST', address: "/grocery/list" }];

    const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

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
                                    <MenuItem key={setting} onClick={handleCloseUserMenu}>
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