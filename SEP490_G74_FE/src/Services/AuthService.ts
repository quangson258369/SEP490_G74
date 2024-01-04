import { UserLogin } from "../Models/AuthModel";
import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";

const login = async (params: UserLogin): Promise<string|undefined> => {
  try{
    const response = await httpClient.post({
      url: `${apiLinks.auth.postLogin}`,
      data: params,
    });
    return response.data.result as string;
  }catch(e){
    console.log(e)
    return undefined
  }
};

const authService = {
  login: login,
};

export default authService;
