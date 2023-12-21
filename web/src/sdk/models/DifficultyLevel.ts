/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

export enum DifficultyLevel {
  _0 = 0,
  _1 = 1,
  _2 = 2,
}

export const getDifficultyName = (difficulty: DifficultyLevel): string => {
  return getDifficultyNameByNumber((difficulty))
}

export const getDifficultyNameByNumber = (value: number): string => {
  switch (value) {
    case 0: return "Easy";
    case 1: return "More effort";
    case 2: return "Pro";
  }
  return "Easy";
}

export const getDifficultyId = (value: string): number => {
  switch (value) {
    case "Easy": return 0;
    case "More effort": return 1;
    case "Pro": return 2;
  }
  return 0;
}


