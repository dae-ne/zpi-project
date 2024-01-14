import { DifficultyLevel } from "@dietly/sdk/models/DifficultyLevel";

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
