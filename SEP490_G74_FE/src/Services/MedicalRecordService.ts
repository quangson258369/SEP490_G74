import { JWTTokenModel, UserLogin } from "../Models/AuthModel";
import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";
import { PatientTableResponseModel } from "../Models/PatientModel";
import { TOKEN } from "../Commons/Global";
import { jwtDecode } from "jwt-decode";
import { MedicalRecordAddModel, MedicalRecordTableModel } from "../Models/MedicalRecordModel";

const addMedicalRecord = async (params: MedicalRecordAddModel
) => {
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

        return response.status as number
      }
    }
    return undefined;
  } catch (e) {
    console.log(e);
    return undefined;
  }
};

const getMedicalRecordsByPatientId = async (patientId: number
  ): Promise<MedicalRecordTableModel[] | undefined> => {
    try {
      const token = localStorage.getItem(TOKEN);
      if (token !== null) {
        var uToken: JWTTokenModel = jwtDecode(token);
  
        if (uToken !== null) {
          var url = `${apiLinks.medicalRecords.getMedicalRecordsByPatientId}${patientId}?pageIndex=${1}&pageSize=${10}`;
          const response = await httpClient.get({
            url: url,
            authorization: `Bearer ${token}`,
          });
          return response.data.result as MedicalRecordTableModel[]
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
  getMedicalRecordsByPatientId: getMedicalRecordsByPatientId
};


export default medicalRecordService;
