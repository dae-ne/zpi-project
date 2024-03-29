/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { UserGetResponse } from '../models/UserGetResponse';
import type { UserPutRequest } from '../models/UserPutRequest';

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
requestBody: UserPutRequest,
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
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

  /**
   * @param userId 
   * @returns UserGetResponse Success
   * @throws ApiError
   */
  public static getUser(
userId: number,
): CancelablePromise<UserGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/users/{userId}',
      path: {
        'userId': userId,
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
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

  /**
   * @returns UserGetResponse Success
   * @throws ApiError
   */
  public static getCurrentUser(): CancelablePromise<UserGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/users/me',
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

}
