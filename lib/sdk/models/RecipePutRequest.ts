/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { DifficultyLevel } from './DifficultyLevel';
import type { RecipePutDirectionDto } from './RecipePutDirectionDto';
import type { RecipePutIngredientDto } from './RecipePutIngredientDto';
import type { RecipePutTagDto } from './RecipePutTagDto';

export type RecipePutRequest = {
  id?: number;
  title?: string | null;
  description?: string | null;
  difficultyLevel?: DifficultyLevel;
  imageUrl?: string | null;
  time?: number;
  calories?: number;
  ingredients?: Array<RecipePutIngredientDto> | null;
  directions?: Array<RecipePutDirectionDto> | null;
  tags?: Array<RecipePutTagDto> | null;
};
