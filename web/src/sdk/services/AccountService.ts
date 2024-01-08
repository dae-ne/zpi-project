/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AccessTokenResponse } from '../models/AccessTokenResponse';
import type { InfoRequest } from '../models/InfoRequest';
import type { InfoResponse } from '../models/InfoResponse';
import type { LoginRequest } from '../models/LoginRequest';
import type { RefreshRequest } from '../models/RefreshRequest';
import type { RegisterRequest } from '../models/RegisterRequest';
import type { ResendConfirmationEmailRequest } from '../models/ResendConfirmationEmailRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class AccountService {

  /**
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static register(
requestBody?: RegisterRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/account/register',
      body: requestBody,
      mediaType: 'application/json',
      errors: {
        400: `Bad Request`,
      },
    });
  }

  /**
   * @param useCookies 
   * @param useSessionCookies 
   * @param requestBody 
   * @returns AccessTokenResponse Success
   * @throws ApiError
   */
  public static login(
useCookies?: boolean,
useSessionCookies?: boolean,
requestBody?: LoginRequest,
): CancelablePromise<AccessTokenResponse> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/account/login',
      query: {
        'useCookies': useCookies,
        'useSessionCookies': useSessionCookies,
      },
      body: requestBody,
      mediaType: 'application/json',
    });
  }

  /**
   * @param requestBody 
   * @returns AccessTokenResponse Success
   * @throws ApiError
   */
  public static refresh(
requestBody?: RefreshRequest,
): CancelablePromise<AccessTokenResponse> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/account/refresh',
      body: requestBody,
      mediaType: 'application/json',
    });
  }

  /**
   * @param userId 
   * @param code 
   * @param changedEmail 
   * @returns any Success
   * @throws ApiError
   */
  public static confirmEmail(
userId?: string,
code?: string,
changedEmail?: string,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/account/confirmEmail',
      query: {
        'userId': userId,
        'code': code,
        'changedEmail': changedEmail,
      },
    });
  }

  /**
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static resendConfirmationEmail(
requestBody?: ResendConfirmationEmailRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/account/resendConfirmationEmail',
      body: requestBody,
      mediaType: 'application/json',
    });
  }

  /**
   * @returns InfoResponse Success
   * @throws ApiError
   */
  public static getAccountInfo(): CancelablePromise<InfoResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/account/manage/info',
      errors: {
        400: `Bad Request`,
        404: `Not Found`,
      },
    });
  }

  /**
   * @param requestBody 
   * @returns InfoResponse Success
   * @throws ApiError
   */
  public static updateAccountInfo(
requestBody?: InfoRequest,
): CancelablePromise<InfoResponse> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/account/manage/info',
      body: requestBody,
      mediaType: 'application/json',
      errors: {
        400: `Bad Request`,
        404: `Not Found`,
      },
    });
  }

}
