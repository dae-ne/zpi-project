/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CreateRecipeDirectionDto } from './CreateRecipeDirectionDto';
import type { CreateRecipeIngredientDto } from './CreateRecipeIngredientDto';
import type { CreateRecipeTagDto } from './CreateRecipeTagDto';
import type { DifficultyLevel } from './DifficultyLevel';

export type CreateRecipeRequest = {
  title?: string | null;
  description?: string | null;
  difficultyLevel?: DifficultyLevel;
  imageUrl?: string | null;
  time?: number;
  calories?: number;
  ingredients?: Array<CreateRecipeIngredientDto> | null;
  directions?: Array<CreateRecipeDirectionDto> | null;
  tags?: Array<CreateRecipeTagDto> | null;
};
