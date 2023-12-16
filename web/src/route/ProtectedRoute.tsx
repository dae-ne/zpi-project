
import { useNavigate } from "react-router-dom";
import Cookies from "universal-cookie";
import { ACCESS_TOKEN_NAME } from "../constants/cookies";
import { SECURITY_LOGIN } from "../constants/app-route";
import { useEffect } from "react";


const ProtectedRoute = ({ children }: any) => {
    const cookies = new Cookies()
    const navigate = useNavigate();

    useEffect(() => {
        const accessToken: string | undefined = cookies.get(ACCESS_TOKEN_NAME)
        if (accessToken) {
            console.log("zalogowany")
        } else {
            navigate(SECURITY_LOGIN);
        }
    }, [children])

    return children;
};

export default ProtectedRoute