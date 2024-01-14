/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { RecipeGetResponse } from './RecipeGetResponse';

export type MealGetResponse = {
  id?: number;
  recipeId?: number;
  date?: string | null;
  completed?: boolean;
  recipe?: RecipeGetResponse;
};
