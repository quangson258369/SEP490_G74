import { JWTTokenModel, UserLogin } from "../Models/AuthModel";
import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";
import {
  PatientAddModel,
  PatientTableResponseModel,
} from "../Models/PatientModel";
import { TOKEN } from "../Commons/Global";
import { jwtDecode } from "jwt-decode";
import {
  DoctorResponseModel,
  ServiceResponseModel,
  ServiceTypeResponseModel,
} from "../Models/SubEntityModel";

const getDoctorsByCategoryId = async (
  id: number
): Promise<DoctorResponseModel[] | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        if (id <= 0) return undefined;
        var url = `${apiLinks.doctor.getDoctorByCategoryId}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });

        return response.data.result as DoctorResponseModel[];
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getServicesType = async (
  id: number
): Promise<ServiceTypeResponseModel[] | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);
      if (uToken !== null) {
        var url = `${apiLinks.serviceType.getServicesType}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as ServiceTypeResponseModel[];
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getServices = async (
  id: number
): Promise<ServiceResponseModel[] | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);
      if (uToken !== null) {
        var url = `${apiLinks.service.getServices}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as ServiceResponseModel[];
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const subService = {
  getDoctorsByCategoryId: getDoctorsByCategoryId,
  getServicesType: getServicesType,
  getServices: getServices,
};

export default subService;
