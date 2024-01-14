import { AccountResponseModel, UserLogin } from "../Models/AuthModel";
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

const getAccounts = async () => {
  try{
    const response = await httpClient.get({
      url: `${apiLinks.auth.getAccounts}`,
    });
    return response.data.result as AccountResponseModel[];
  }catch(e){
    console.log(e)
    return undefined
  }
};

const authService = {
  login: login,
  getAccounts: getAccounts,
};

export default authService;
