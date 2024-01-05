import "./variables.scss"
import './index.scss';

import React, { useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import RecipeEdit from './components/recipes/recipe-edit/recipe-edit';
import PlanList from './components/plans/plans-list';
import appTheme from './components/theme';
import Navigation from './components/navigation';
import GroceryList from './components/grocery/grocery-list';
import RecipeList from './components/recipes/recipe-list';

import { ThemeProvider } from '@mui/material/styles';
import RecipePreview from "./components/recipes/recipe-preview";
import LoginPage from "./components/security/login";
import { SECURITY_ROOT, SECURITY_LOGIN, SECURITY_REGISTER, SECURITY_DEFAULT, RECIPE_LIST, RECIPE_EDIT_RAW, RECIPE_PREVIEW, PLAN_LIST, GROCERY_LIST, ROOT_DEFAULT, ROOT, EMPTY, RECIPE_NEW, RECIPE_EDIT } from "./constants/app-route";


import RegisterPage from "./components/security/register";
import ProtectedRoute from "./route/ProtectedRoute";
import NoProtectedRoute from "./route/NoProtectedRoot";
import { RecipeListMode } from "./enums/recipe";
import { OpenAPI } from "./sdk";
import { ACCESS_TOKEN_NAME } from "./constants/cookies";
import Cookies from "universal-cookie";

const App = () => {

  const cookies = new Cookies()


  useEffect(() => {
    const accessToken: string = cookies.get(ACCESS_TOKEN_NAME)
    const location = window.location;
    console.log(location)

    if (!accessToken && location.pathname != SECURITY_LOGIN && location.pathname != SECURITY_REGISTER) {
      console.log("redirect")
      location.replace(SECURITY_LOGIN)
    }
    OpenAPI.TOKEN = accessToken
  }, [])

  return (
    <ThemeProvider theme={appTheme}>

      <BrowserRouter>
        <Routes>

          <Route path={EMPTY} element={<NoProtectedRoute><LoginPage /></NoProtectedRoute>} />

          <Route path={SECURITY_ROOT}>
            <Route path={SECURITY_LOGIN} element={<LoginPage />} />
            <Route path={SECURITY_REGISTER} element={<RegisterPage />} />
            <Route path={SECURITY_DEFAULT} element={<LoginPage />} />

          </Route>

          <Route path={ROOT} element={<Navigation />}>

            <Route path={RECIPE_LIST} element={<ProtectedRoute><RecipeList mode={RecipeListMode.View} /></ProtectedRoute>} />
            <Route path={RECIPE_NEW} element={<ProtectedRoute><RecipeEdit /></ProtectedRoute>} />
            <Route path={RECIPE_EDIT} element={<ProtectedRoute><RecipeEdit /></ProtectedRoute>} />
            <Route path={RECIPE_PREVIEW} element={<ProtectedRoute><RecipePreview /></ProtectedRoute>} />
            <Route path={PLAN_LIST} element={<PlanList />} />
            <Route path={GROCERY_LIST} element={<GroceryList />} />
            <Route path={ROOT_DEFAULT} element={<RecipeList mode={RecipeListMode.View} />} />

          </Route>

        </Routes>
      </BrowserRouter >

    </ThemeProvider>)
}

export default App;
