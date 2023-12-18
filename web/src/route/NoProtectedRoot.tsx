
import { useNavigate } from "react-router-dom";
import Cookies from "universal-cookie";
import { ACCESS_TOKEN_NAME } from "../constants/cookies";
import { RECIPE_LIST, SECURITY_LOGIN } from "../constants/app-route";
import { useEffect } from "react";
import { OpenAPI } from "../sdk";


const NoProtectedRoute = ({ children }: any) => {
    const cookies = new Cookies()
    const navigate = useNavigate();

    useEffect(() => {
        const accessToken: string | undefined = cookies.get(ACCESS_TOKEN_NAME)
        if (accessToken) {
            OpenAPI.TOKEN = accessToken
            navigate(RECIPE_LIST);
        }
    }, [children])

    return children;
};

export default NoProtectedRoute