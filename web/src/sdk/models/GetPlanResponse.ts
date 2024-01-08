/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GetMealResponse } from './GetMealResponse';

export type GetPlanResponse = {
  meals?: Array<GetMealResponse> | null;
  date?: string | null;
  totalCalories?: number;
  consumedCalories?: number;
};
