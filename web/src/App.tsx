import "./variables.scss"
import './index.scss';

import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import RecipeEdit from './components/recipes/recipe-edit/recipe-edit';
import PlanList from './components/plans/plan-list';
import appTheme from './components/theme';
import Navigation from './components/navigation';
import GroceryList from './components/grocery/grocery-list';
import RecipeList from './components/recipes/recipe-list';

import { ThemeProvider } from '@mui/material/styles';
import RecipePreview from "./components/recipes/recipe-preview";
import LoginPage from "./components/security/login";
import { SECURITY_ROOT, SECURITY_LOGIN, SECURITY_REGISTER, SECURITY_DEFAULT, RECIPE_LIST, RECIPE_EDIT, RECIPE_PREVIEW, PLAN_LIST, GROCERY_LIST, ROOT_DEFAULT, ROOT, EMPTY } from "./constants/app-route";
import Cookies from "universal-cookie";
import { ACCESS_TOKEN_NAME } from "./constants/cookies";
import { OpenAPI } from "./sdk";
import RegisterPage from "./components/security/register";

const App = () => {
  const cookies = new Cookies()
  OpenAPI.TOKEN = cookies.get(ACCESS_TOKEN_NAME)

  return (
    <ThemeProvider theme={appTheme}>

      <BrowserRouter>
        <Routes>

          <Route path={EMPTY} element={<LoginPage />} />


          <Route path={SECURITY_ROOT}>
            <Route path={SECURITY_LOGIN} element={<LoginPage />} />
            <Route path={SECURITY_REGISTER} element={<RegisterPage />} />
            <Route path={SECURITY_DEFAULT} element={<LoginPage />} />

          </Route>

          <Route path={ROOT} element={<Navigation />}>

            <Route path={RECIPE_LIST} element={<RecipeList />} />
            <Route path={RECIPE_EDIT} element={<RecipeEdit />} />
            <Route path={RECIPE_PREVIEW} element={<RecipePreview />} />
            <Route path={PLAN_LIST} element={<PlanList />} />
            <Route path={GROCERY_LIST} element={<GroceryList />} />
            <Route path={ROOT_DEFAULT} element={<RecipeList />} />

          </Route>

        </Routes>
      </BrowserRouter >

    </ThemeProvider>)
}

export default App;
