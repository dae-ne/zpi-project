/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

export enum DifficultyLevel {
  _0 = "Easy",
  _1 = "More effort",
  _2 = "Pro",
}

export const getDifficultyString = (difficulty: DifficultyLevel): string => {
  return getDifficultyStringByNumber(parseInt(difficulty))
}

export const getDifficultyStringByNumber = (value: number): string => {
  switch (value) {
    case 0: return "Easy";
    case 1: return "More effort";
    case 2: return "Pro";
  }
  return "";
}

