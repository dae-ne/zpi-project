/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class ImagesService {

  /**
   * @param fileName 
   * @returns any Success
   * @throws ApiError
   */
  public static removeFoodImage(
fileName: string,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'DELETE',
      url: '/images/food/{fileName}',
      path: {
        'fileName': fileName,
      },
    });
  }

  /**
   * @param fileName 
   * @returns string Success
   * @throws ApiError
   */
  public static getFoodImage(
fileName: string,
): CancelablePromise<string> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/images/food/{fileName}',
      path: {
        'fileName': fileName,
      },
    });
  }

  /**
   * @param fileName 
   * @returns any Success
   * @throws ApiError
   */
  public static removeAvatar(
fileName: string,
): CancelablePromise<any> {
    return __request(OpenAPI, {
      method: 'DELETE',
      url: '/images/avatar/{fileName}',
      path: {
        'fileName': fileName,
      },
    });
  }

  /**
   * @param fileName 
   * @returns string Success
   * @throws ApiError
   */
  public static getAvatar(
fileName: string,
): CancelablePromise<string> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/images/avatar/{fileName}',
      path: {
        'fileName': fileName,
      },
    });
  }

  /**
   * @param formData 
   * @returns string Created
   * @throws ApiError
   */
  public static addFoodImage(
formData?: {
file: Blob;
},
): CancelablePromise<string> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/images/food',
      formData: formData,
      mediaType: 'multipart/form-data',
      responseHeader: 'location',
    });
  }

  /**
   * @param formData 
   * @returns string Created
   * @throws ApiError
   */
  public static addAvatar(
formData?: {
file: Blob;
},
): CancelablePromise<string> {
    return __request(OpenAPI, {
      method: 'POST',
      url: '/images/avatar',
      formData: formData,
      mediaType: 'multipart/form-data',
      responseHeader: 'location',
    });
  }

}
