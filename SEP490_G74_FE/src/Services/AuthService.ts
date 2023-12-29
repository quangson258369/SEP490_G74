import axios from "axios"
import { LoginResponse, UserLogin } from "../Models/AuthModel";

const login = async (user: UserLogin) => {
    try {
        var response = await axios.post<LoginResponse>("https://localhost:7021/api/Users/login", JSON.stringify(user), {
            headers: {
                "Content-Type": "application/json"
            }
        })
        if (response.status == 200) {
            return response.data.result
        }
        return undefined
    } catch (error) {
        return undefined
    }
}

export const authService = {
    login: login
}