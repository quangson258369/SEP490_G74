import { useContext, useEffect, useState } from "react"
import { Outlet, useNavigate } from "react-router-dom"
import { AuthContext } from "../ContextProvider/AuthContext"

type ProtectedRouteProps = {
    role: string[]
}

const ProtectedRoute = ({ role }: ProtectedRouteProps) => {
    const { authenticated } = useContext(AuthContext)
    const navigate = useNavigate()
    const [isLoading, setIsLoading] = useState<boolean>(true)

    useEffect(() => {
        handleGetMe()
    }, [])

    const handleGetMe = () => {
        try {
            console.log(authenticated);
            if(authenticated === undefined){
                navigate("/login")
            }else{
                if(!role.includes(authenticated.role)){
                    navigate("/404")
                }else{
                    setIsLoading(false)
                }
            }
        }catch{
            navigate("/error")
        }
    }
    return isLoading ? <></> : <Outlet/>
}

export default ProtectedRoute