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
  SelectedSuppliesResponseModel,
  ServiceResponseModel,
  ServiceTypeResponseModel,
  SuppliesPresAddModel,
  SupplyIdPreAddModel,
  SupplyResponseModel,
  SupplyTypeResponseModel,
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

const getLeastAssignedDoctorByCategoryId = async (
  id: number
): Promise<DoctorResponseModel | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        if (id <= 0) return undefined;
        var url = `${apiLinks.doctor.getLeastAssignedDoctorByCategoryId}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });

        return response.data.result as DoctorResponseModel;
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

const getAllSupplyTypes = async (
): Promise<SupplyTypeResponseModel[] | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${apiLinks.supply.getAllSupplyTypes}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });

        return response.data.result.items as SupplyTypeResponseModel[];
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getSuppliesBySupplyTypeId = async (
  id: number
): Promise<SupplyResponseModel[] | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        if (id <= 0) return undefined;
        var url = `${apiLinks.supply.getSuppliesBySupplyTypeId}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result.supplies as SupplyResponseModel[];
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const addSuppliesPrescriptionsByMrId = async (
  id: number,
  params: SuppliesPresAddModel
) => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        const response = await httpClient.post({
          url: `${apiLinks.supply.postSuppliesPrescriptionByMrId}${id}`,
          authorization: `Bearer ${token}`,
          data: JSON.stringify(params),
        });

        return response.status as number;
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getSelectedSuppliesByMrId = async (
  id: number
): Promise<SelectedSuppliesResponseModel[] | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        if (id <= 0) return undefined;
        var url = `${apiLinks.supply.getSelectedSuppliesByMrId}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as SelectedSuppliesResponseModel[];
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
  getLeastAssignedDoctorByCategoryId: getLeastAssignedDoctorByCategoryId,
  getAllSupplyTypes: getAllSupplyTypes,
  getSuppliesBySupplyTypeId: getSuppliesBySupplyTypeId,
  addSuppliesPrescriptionsByMrId: addSuppliesPrescriptionsByMrId,
  getSelectedSuppliesByMrId: getSelectedSuppliesByMrId,
};

export default subService;
