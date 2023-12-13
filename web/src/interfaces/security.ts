export enum Sex {
    Male = "Male",
    Female = "Female"
}

/**
 * email - address email
 * password - password
 */
export interface Login {
    email: string,
    password: string
}

/**
 * email - address email
 * name - name
 * password - password again to verification
 * rePassword: - password again to verification
 * age - age
 */
export interface Register {
    email: string,
    name: string,
    surname: string,
    password: string,
    rePassword: string,
    age: number
}

/**
 * email - address email
 * password - password again to verification
 * rePassword: - password again to verification
 */
export interface PasswordChange {
    email: string,
    password: string,
    rePassword: string
}