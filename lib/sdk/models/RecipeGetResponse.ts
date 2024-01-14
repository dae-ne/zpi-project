/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { DifficultyLevel } from './DifficultyLevel';
import type { RecipeGetDirectionDto } from './RecipeGetDirectionDto';
import type { RecipeGetIngredientDto } from './RecipeGetIngredientDto';
import type { RecipeGetTagDto } from './RecipeGetTagDto';

export type RecipeGetResponse = {
  id?: number;
  userId?: number;
  title?: string | null;
  description?: string | null;
  difficultyLevel?: DifficultyLevel;
  imageUrl?: string | null;
  time?: number;
  calories?: number;
  ingredients?: Array<RecipeGetIngredientDto> | null;
  directions?: Array<RecipeGetDirectionDto> | null;
  tags?: Array<RecipeGetTagDto> | null;
};
