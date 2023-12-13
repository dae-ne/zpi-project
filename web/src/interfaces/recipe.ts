/**
 Recipie difficulties 
 */
export enum DifficultyLevel {
    Easy = 0,
    MoreEffort = 1,
    Pro = 2
}

/**
 *  name - name of recipie
 *  description - description of recipie
 *  image - image in base64
 *  difficultyLevel - enum with int values
 *  time - time in munutes of making recipie, values between 15 - 210  --to moja propozycja, można to zmienić
 *  energy - calories of meal
 *  tags - array of tags
 */
export interface BaseRecipe {
    name: string
    description: string,
    image: string,
    difficultyLevel: DifficultyLevel,
    time: number,
    energy: number,
    tags: Array<string>
}

/** extends BaseRecipe
 *  ingredients - array of ingredients
 *  directions - array of directions
 */
export interface FullRecipe extends BaseRecipe {
    ingredients: Array<string>,
    directions: Array<string>
}


