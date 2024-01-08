import Cookies from "universal-cookie";
import { ROOT } from "../constants/app-route";
import { ACCESS_TOKEN_NAME, REFRESH_TOKEN_MAX_AGE, REFRESH_TOKEN_NAME } from "../constants/cookies";
import { AccessTokenResponse } from "../sdk";

const cookies = new Cookies()

export const setLoginCookies = (response: AccessTokenResponse) => {
    cookies.set(
        ACCESS_TOKEN_NAME,
        response.accessToken,
        {
            maxAge: response.expiresIn,
            sameSite: "strict",
            path: ROOT
        }
    )
    cookies.set(
        REFRESH_TOKEN_NAME,
        response.refreshToken,
        {
            maxAge: REFRESH_TOKEN_MAX_AGE,
            sameSite: "strict",
            path: ROOT
        }
    )
}

export const clearCookies = () => {
    cookies.remove(ACCESS_TOKEN_NAME)
    cookies.remove(REFRESH_TOKEN_NAME)
}

export const getSecurityCookies = (): { accessToken: string, refreshToken: string } => {
    return { accessToken: cookies.get(ACCESS_TOKEN_NAME), refreshToken: cookies.get(REFRESH_TOKEN_NAME) }
}