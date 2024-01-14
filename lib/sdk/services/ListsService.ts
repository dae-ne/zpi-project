/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ListGetResponse } from '../models/ListGetResponse';
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
   * @returns ListGetResponse Success
   * @throws ApiError
   */
  public static getList(
from?: string,
to?: string,
): CancelablePromise<ListGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/lists',
      query: {
        'From': from,
        'To': to,
      },
      errors: {
        400: `Bad Request`,
        403: `Forbidden`,
        404: `Not Found`,
        500: `Server Error`,
      },
    });
  }

}
