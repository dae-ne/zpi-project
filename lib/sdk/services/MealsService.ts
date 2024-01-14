/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddMealRequest } from '../models/AddMealRequest';
import type { GetMealResponse } from '../models/GetMealResponse';
import type { GetMealsResponse } from '../models/GetMealsResponse';
import type { UpdateMealRequest } from '../models/UpdateMealRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class MealsService {

  /**
   * @param mealId 
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static updateMeal(
mealId: number,
requestBody: UpdateMealRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'PUT',
      url: '/api/meals/{mealId}',
      path: {
        'mealId': mealId,
      },
      body: requestBody,
      mediaType: 'application/json',
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

  /**
   * @param mealId 
   * @returns any Success
   * @throws ApiError
   */
  public static removeMeal(
mealId: number,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'DELETE',
      url: '/api/meals/{mealId}',
      path: {
        'mealId': mealId,
      },
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

  /**
   * @param mealId 
   * @returns GetMealResponse Success
   * @throws ApiError
   */
  public static getMeal(
mealId: number,
): CancelablePromise<GetMealResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/meals/{mealId}',
      path: {
        'mealId': mealId,
      },
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

  /**
   * @param from 
   * @param to 
   * @returns GetMealsResponse Success
   * @throws ApiError
   */
  public static getMeals(
from?: string,
to?: string,
): CancelablePromise<GetMealsResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/meals',
      query: {
        'from': from,
        'to': to,
      },
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

  /**
   * @param requestBody 
   * @returns string Created
   * @throws ApiError
   */
  public static addMeal(
requestBody: AddMealRequest,
): CancelablePromise<string> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/meals',
      body: requestBody,
      mediaType: 'application/json',
      responseHeader: 'location',
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

}
