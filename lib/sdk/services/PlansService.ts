/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { PlanGetResponse } from '../models/PlanGetResponse';
import type { PlansGetResponse } from '../models/PlansGetResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class PlansService {

  /**
   * @param day 
   * @returns PlanGetResponse Success
   * @throws ApiError
   */
  public static getPlan(
day: string,
): CancelablePromise<PlanGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/plans/{day}',
      path: {
        'day': day,
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
   * @returns PlansGetResponse Success
   * @throws ApiError
   */
  public static getPlans(
from?: string,
to?: string,
): CancelablePromise<PlansGetResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/plans',
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

}
