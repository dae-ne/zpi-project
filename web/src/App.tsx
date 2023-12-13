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

const App = () => (
  <ThemeProvider theme={appTheme}>

    <BrowserRouter>
      <Routes>
        <Route path="/security">
          <Route path="/security/login" element={<LoginPage />} />
          <Route path="/security/register" element={<LoginPage />} />
          <Route path="/security/?*" element={<LoginPage />} />

        </Route>

        <Route path="/" element={<Navigation />}>

          <Route path="/recipe/list" element={<RecipeList />} />
          <Route path="/recipe/edit" element={<RecipeEdit />} />
          <Route path="/recipe/preview" element={<RecipePreview />} />
          <Route path="/plan/list" element={<PlanList />} />
          <Route path="/grocery/list" element={<GroceryList />} />
          <Route path="/*" element={<PlanList />} />

        </Route>

      </Routes>
    </BrowserRouter >

  </ThemeProvider>
)

export default App;
