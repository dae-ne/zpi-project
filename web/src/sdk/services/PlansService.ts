/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GetPlanResponse } from '../models/GetPlanResponse';
import type { GetPlansResponse } from '../models/GetPlansResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class PlansService {

  /**
   * @param day 
   * @returns GetPlanResponse Success
   * @throws ApiError
   */
  public static getPlan(
day: string,
): CancelablePromise<GetPlanResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/plans/{day}',
      path: {
        'day': day,
      },
    });
  }

  /**
   * @param from 
   * @param to 
   * @returns GetPlansResponse Success
   * @throws ApiError
   */
  public static getPlans(
from?: string,
to?: string,
): CancelablePromise<GetPlansResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/plans',
      query: {
        'from': from,
        'to': to,
      },
    });
  }

}
