/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GetUserResponse } from '../models/GetUserResponse';
import type { UpdateUserRequest } from '../models/UpdateUserRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class UsersService {

  /**
   * @param userId 
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static updateUser(
userId: number,
requestBody: UpdateUserRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'PUT',
      url: '/api/users/{userId}',
      path: {
        'userId': userId,
      },
      body: requestBody,
      mediaType: 'application/json',
      errors: {
        403: `Forbidden`,
      },
    });
  }

  /**
   * @param userId 
   * @returns any Success
   * @throws ApiError
   */
  public static removeUser(
userId: number,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'DELETE',
      url: '/api/users/{userId}',
      path: {
        'userId': userId,
      },
      errors: {
        403: `Forbidden`,
      },
    });
  }

  /**
   * @param userId 
   * @returns GetUserResponse Success
   * @throws ApiError
   */
  public static getUser(
userId: number,
): CancelablePromise<GetUserResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/users/{userId}',
      path: {
        'userId': userId,
      },
      errors: {
        403: `Forbidden`,
      },
    });
  }

  /**
   * @returns GetUserResponse Success
   * @throws ApiError
   */
  public static getCurrentUser(): CancelablePromise<GetUserResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/users/me',
    });
  }

}
