import "./variables.scss"
import './index.scss';

import React, { useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { GroceryList, LoginPage, PlansList, RecipeEdit, RecipeList, RecipePreview, RegisterPage, Navigation } from "./components";

import ProtectedRoute from "./route/ProtectedRoute";
import NoProtectedRoute from "./route/NoProtectedRoot";

import appTheme from './components/theme';
import { ThemeProvider } from '@mui/material/styles';
import { RecipeListMode } from "./enums/recipe";
import { AccessTokenResponse, AccountService, OpenAPI } from "@dietly/sdk";
import { clearCookies, getSecurityCookies, setLoginCookies } from "./tools/security";
import {
  SECURITY_LOGIN, SECURITY_REGISTER, EMPTY, SECURITY_ROOT,
  SECURITY_DEFAULT, ROOT, RECIPE_LIST, RECIPE_NEW, RECIPE_EDIT,
  RECIPE_PREVIEW, PLAN_LIST, GROCERY_LIST, ROOT_DEFAULT, SERVICE_URL
} from "./constants/app-route";

const App = () => {

  const checkPrivilages = (accessToken: string) => {
    const location = window.location;
    if (!accessToken && location.pathname != SECURITY_LOGIN && location.pathname != SECURITY_REGISTER) {
      location.replace(SECURITY_LOGIN)
    }
  }

  useEffect(() => {
    OpenAPI.BASE = process.env.REACT_APP_SERVICE_URL ?? SERVICE_URL

    const { accessToken, refreshToken } = getSecurityCookies()
    if (!refreshToken) return;

    AccountService.refresh({ refreshToken: refreshToken })
      .then((response: AccessTokenResponse) => {
        if (!response.accessToken || !response.refreshToken) {
          clearCookies()
        } else {
          setLoginCookies(response)
          OpenAPI.TOKEN = accessToken
          checkPrivilages(accessToken)
        }
      }).catch(() => {
        clearCookies()
        checkPrivilages("")
      })
  }, [])

  return (
    <ThemeProvider theme={appTheme}>

      <BrowserRouter basename={process.env.REACT_APP_PUBLIC_URL}>
        <Routes>

          <Route path={EMPTY} element={<NoProtectedRoute><LoginPage /></NoProtectedRoute>} />

          <Route path={SECURITY_ROOT}>
            <Route path={SECURITY_LOGIN} element={<NoProtectedRoute><LoginPage /></NoProtectedRoute>} />
            <Route path={SECURITY_REGISTER} element={<NoProtectedRoute><RegisterPage /></NoProtectedRoute>} />
            <Route path={SECURITY_DEFAULT} element={<NoProtectedRoute><LoginPage /></NoProtectedRoute>} />

          </Route>

          <Route path={ROOT} element={<Navigation />}>

            <Route path={RECIPE_LIST} element={<ProtectedRoute><RecipeList mode={RecipeListMode.View} /></ProtectedRoute>} />
            <Route path={RECIPE_NEW} element={<ProtectedRoute><RecipeEdit /></ProtectedRoute>} />
            <Route path={RECIPE_EDIT} element={<ProtectedRoute><RecipeEdit /></ProtectedRoute>} />
            <Route path={RECIPE_PREVIEW} element={<ProtectedRoute><RecipePreview /></ProtectedRoute>} />
            <Route path={PLAN_LIST} element={<ProtectedRoute><PlansList /></ProtectedRoute>} />
            <Route path={GROCERY_LIST} element={<ProtectedRoute><GroceryList /></ProtectedRoute>} />
            <Route path={ROOT_DEFAULT} element={<ProtectedRoute><RecipeList mode={RecipeListMode.View} /></ProtectedRoute>} />

          </Route>

        </Routes>
      </BrowserRouter >

    </ThemeProvider>)
}

export default App;
