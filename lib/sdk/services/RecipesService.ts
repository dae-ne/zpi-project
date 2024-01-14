/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { RecipeGetResponse } from '../models/RecipeGetResponse';
import type { RecipePostRequest } from '../models/RecipePostRequest';
import type { RecipePutRequest } from '../models/RecipePutRequest';
import type { RecipesGetResponse } from '../models/RecipesGetResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class RecipesService {

  /**
   * @param recipeId 
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static updateRecipe(
recipeId: number,
requestBody: RecipePutRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'PUT',
      url: '/api/recipes/{recipeId}',
      path: {
        'recipeId': recipeId,
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
   * @param recipeId 
   * @returns RecipeGetResponse Success
   * @throws ApiError
   */
  public static getRecipe(
recipeId: number,
): CancelablePromise<RecipeGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/recipes/{recipeId}',
      path: {
        'recipeId': recipeId,
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
   * @param recipeId 
   * @returns any Success
   * @throws ApiError
   */
  public static removeRecipe(
recipeId: number,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'DELETE',
      url: '/api/recipes/{recipeId}',
      path: {
        'recipeId': recipeId,
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
  public static createRecipe(
requestBody: RecipePostRequest,
): CancelablePromise<string> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/recipes',
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

  /**
   * @returns RecipesGetResponse Success
   * @throws ApiError
   */
  public static getRecipes(): CancelablePromise<RecipesGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/recipes',
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

}
