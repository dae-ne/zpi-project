/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { MealGetResponse } from './MealGetResponse';

export type PlanGetResponse = {
  meals?: Array<MealGetResponse> | null;
  date?: string | null;
  totalCalories?: number;
  consumedCalories?: number;
};
