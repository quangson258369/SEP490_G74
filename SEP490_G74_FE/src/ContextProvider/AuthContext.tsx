import { createContext, useState } from "react"
import Roles from "../Enums/Enums"
import { jwtDecode } from "jwt-decode"
import { JWTTokenModel } from "../Models/AuthModel"
type Props = {
    children?: React.ReactNode
}

export interface User {
    role: string
}

var currAuthenticated: User = {
    role: Roles.Guest
}

const getCurrentUser = () =>{
    var tokenString = localStorage.getItem("token")
    if(tokenString!==null){
        var jwtUser = jwtDecode<JWTTokenModel>(tokenString)
        if(jwtUser!==null){
            currAuthenticated.role = jwtUser.role
            console.log(currAuthenticated.role)
        }
    }
}

type IAuthContext = {
    authenticated: User | undefined,
    setAuthenticated: (newState: User | undefined) => void
}

getCurrentUser()

const initialValue: IAuthContext = {
    authenticated: currAuthenticated,
    setAuthenticated: () => { }
}

const AuthContext = createContext<IAuthContext>(initialValue)

const AuthProvider = ({ children }: Props) => {
    const [authenticated, setAuthenticated] = useState(initialValue.authenticated)

    return (
        <AuthContext.Provider value={{ authenticated, setAuthenticated }}>
            {children}
        </AuthContext.Provider>
    )
}

export { AuthContext, AuthProvider }