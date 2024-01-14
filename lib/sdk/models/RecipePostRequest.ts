/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { DifficultyLevel } from './DifficultyLevel';
import type { RecipePostDirectionDto } from './RecipePostDirectionDto';
import type { RecipePostIngredientDto } from './RecipePostIngredientDto';
import type { RecipePostTagDto } from './RecipePostTagDto';

export type RecipePostRequest = {
  title?: string | null;
  description?: string | null;
  difficultyLevel?: DifficultyLevel;
  imageUrl?: string | null;
  time?: number;
  calories?: number;
  ingredients?: Array<RecipePostIngredientDto> | null;
  directions?: Array<RecipePostDirectionDto> | null;
  tags?: Array<RecipePostTagDto> | null;
};
