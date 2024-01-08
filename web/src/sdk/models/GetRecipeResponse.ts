/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { DifficultyLevel } from './DifficultyLevel';
import type { GetRecipeDirectionResponse } from './GetRecipeDirectionResponse';
import type { GetRecipeIngredientResponse } from './GetRecipeIngredientResponse';
import type { GetRecipeTagResponse } from './GetRecipeTagResponse';

export type GetRecipeResponse = {
  id?: number;
  userId?: number;
  title?: string | null;
  description?: string | null;
  difficultyLevel?: DifficultyLevel;
  imageUrl?: string | null;
  time?: number;
  calories?: number;
  ingredients?: Array<GetRecipeIngredientResponse> | null;
  directions?: Array<GetRecipeDirectionResponse> | null;
  tags?: Array<GetRecipeTagResponse> | null;
};
