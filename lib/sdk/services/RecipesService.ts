/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateRecipeRequest } from '../models/CreateRecipeRequest';
import type { GetRecipeResponse } from '../models/GetRecipeResponse';
import type { GetRecipesResponse } from '../models/GetRecipesResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class RecipesService {

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
    });
  }

  /**
   * @param recipeId 
   * @returns GetRecipeResponse Success
   * @throws ApiError
   */
  public static getRecipe(
recipeId: number,
): CancelablePromise<GetRecipeResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/recipes/{recipeId}',
      path: {
        'recipeId': recipeId,
      },
    });
  }

  /**
   * @returns GetRecipesResponse Success
   * @throws ApiError
   */
  public static getRecipes(): CancelablePromise<GetRecipesResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/recipes',
    });
  }

  /**
   * @param requestBody 
   * @returns any Created
   * @throws ApiError
   */
  public static createRecipe(
requestBody: CreateRecipeRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/recipes',
      body: requestBody,
      mediaType: 'application/json',
    });
  }

}