import { JWTTokenModel } from "../Models/AuthModel";
import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";
import { TOKEN } from "../Commons/Global";
import { jwtDecode } from "jwt-decode";
import {
  MedicalRecordAddModel,
  MedicalRecordDetailModel,
  MedicalRecordTableModel,
  MedicalRecordUpdateModel,
} from "../Models/MedicalRecordModel";
import { ApiResponseModel } from "../Models/PatientModel";

const addMedicalRecord = async (params: MedicalRecordAddModel) => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        const response = await httpClient.post({
          url: apiLinks.medicalRecords.postMedicalRecord,
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

const updateMedicalRecord = async (
  id: number,
  params: MedicalRecordUpdateModel
) => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        const response = await httpClient.patch({
          url: `${apiLinks.medicalRecords.pathUpdateMr}${id}`,
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

const getMedicalRecordsByPatientId = async (
  patientId: number,
  pageIndex: number,
  pageSize: number
): Promise<ApiResponseModel | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${
          apiLinks.medicalRecords.getMedicalRecordsByPatientId
        }${patientId}?pageIndex=${pageIndex}&pageSize=${pageSize}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as ApiResponseModel;
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getMedicalRecordsUnCheckByPatientId = async (
  patientId: number,
  pageIndex: number,
  pageSize: number
): Promise<ApiResponseModel | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${
          apiLinks.medicalRecords.getMedicalRecordsUnCheckByPatientId
        }${patientId}?pageIndex=${pageIndex}&pageSize=${pageSize}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as ApiResponseModel;
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getMedicalRecordDetailById = async (
  id: number
): Promise<MedicalRecordDetailModel | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${apiLinks.medicalRecords.getMedicalRecordById}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as MedicalRecordDetailModel;
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const updateMrPaidStatus = async (id: number): Promise<number | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${apiLinks.medicalRecords.patchMrStatusPaid}${id}`;
        const response = await httpClient.patch({
          url: url,
          authorization: `Bearer ${token}`,
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

const updateMrCheckUpStatus = async (
  id: number
): Promise<number | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${apiLinks.medicalRecords.patchMrStatusCheckUp}${id}`;
        const response = await httpClient.patch({
          url: url,
          authorization: `Bearer ${token}`,
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

const getReCheckUpByPrevMrId = async (
  id: number
): Promise<number | undefined> => {
  try {
    const token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var uToken: JWTTokenModel = jwtDecode(token);

      if (uToken !== null) {
        var url = `${apiLinks.medicalRecords.getReCheckUpMrByPrevMrId}${id}`;
        const response = await httpClient.get({
          url: url,
          authorization: `Bearer ${token}`,
        });
        return response.data.result as number;
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const medicalRecordService = {
  addMedicalRecord: addMedicalRecord,
  getMedicalRecordsByPatientId: getMedicalRecordsByPatientId,
  getMedicalRecordDetailById: getMedicalRecordDetailById,
  updateMrPaidStatus: updateMrPaidStatus,
  updateMrCheckUpStatus: updateMrCheckUpStatus,
  updateMedicalRecord: updateMedicalRecord,
  getReCheckUpByPrevMrId: getReCheckUpByPrevMrId,
  getMedicalRecordsUnCheckByPatientId: getMedicalRecordsUnCheckByPatientId,
};

export default medicalRecordService;