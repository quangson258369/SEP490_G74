import { useContext, useEffect } from "react"
import Roles from "../Enums/Enums";
import { useNavigate } from "react-router-dom";
import { AuthContext, User } from "../ContextProvider/AuthContext";
import { JWTTokenModel } from "../Models/AuthModel";
import { jwtDecode } from "jwt-decode";
import { Flex, Spin } from 'antd';

const HomePage = () => {
    const navigate = useNavigate()
    const { setAuthenticated } = useContext(AuthContext)

    const authorizedNavigate = (role: string) => {
        switch (role) {
            case Roles.Admin: {
                navigate("/admin/patients")
                break;
            }
            case Roles.Doctor: {
                navigate("/doctor/patients")
                break;
            }
            case Roles.Nurse: {
                navigate("/nurse/patients")
                break;
            }
            case Roles.Cashier: {
                navigate("/nurse/patients")
                break;
            }
            default: {
                navigate("/login")
                break;
            }
        }
    }

    const authorizeAccess = () => {
        var token = localStorage.getItem("token")

        if (token !== null) {
            var jwtUser: JWTTokenModel = jwtDecode(token)
            let newUser: User = {
                role: jwtUser.role
            }

            setAuthenticated(newUser)
            authorizedNavigate(newUser.role)

        } else {
            navigate("/login")
        }
    }

    useEffect(() => {
        authorizeAccess()
    })

    return (
        <Flex align="center" gap="middle">
            <Spin size="large" />
        </Flex>
    )
}

export default HomePage