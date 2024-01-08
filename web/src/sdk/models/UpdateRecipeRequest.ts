/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { DifficultyLevel } from './DifficultyLevel';
import type { UpdateRecipeDirectionDto } from './UpdateRecipeDirectionDto';
import type { UpdateRecipeIngredientDto } from './UpdateRecipeIngredientDto';
import type { UpdateRecipeTagDto } from './UpdateRecipeTagDto';

export type UpdateRecipeRequest = {
  id?: number;
  title?: string | null;
  description?: string | null;
  difficultyLevel?: DifficultyLevel;
  imageUrl?: string | null;
  time?: number;
  calories?: number;
  ingredients?: Array<UpdateRecipeIngredientDto> | null;
  directions?: Array<UpdateRecipeDirectionDto> | null;
  tags?: Array<UpdateRecipeTagDto> | null;
};
