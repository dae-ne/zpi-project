/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GetRecipeResponse } from './GetRecipeResponse';

export type GetMealResponse = {
  id?: number;
  recipeId?: number;
  date?: string | null;
  completed?: boolean;
  recipe?: GetRecipeResponse;
};
