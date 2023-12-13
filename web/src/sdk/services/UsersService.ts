/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GetCurrentUserResponse } from '../models/GetCurrentUserResponse';
import type { UpdateUserRequest } from '../models/UpdateUserRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class UsersService {

  /**
   * @param id 
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static updateUser(
id: number,
requestBody: UpdateUserRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'PUT',
      url: '/api/users/{id}',
      path: {
        'id': id,
      },
      body: requestBody,
      mediaType: 'application/json',
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
    });
  }

  /**
   * @returns GetCurrentUserResponse Success
   * @throws ApiError
   */
  public static getCurrentUser(): CancelablePromise<GetCurrentUserResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/users/me',
    });
  }

}
