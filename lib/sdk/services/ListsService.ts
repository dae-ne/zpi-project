/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GetListResponse } from '../models/GetListResponse';
import type { SendEmailWithListRequest } from '../models/SendEmailWithListRequest';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class ListsService {

  /**
   * @param requestBody 
   * @returns any Success
   * @throws ApiError
   */
  public static sendEmailWithList(
requestBody: SendEmailWithListRequest,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/api/lists/sendEmail',
      body: requestBody,
      mediaType: 'application/json',
    });
  }

  /**
   * @param from 
   * @param to 
   * @returns GetListResponse Success
   * @throws ApiError
   */
  public static getList(
from: string,
to: string,
): CancelablePromise<GetListResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/lists',
      query: {
        'From': from,
        'To': to,
      },
    });
  }

}
