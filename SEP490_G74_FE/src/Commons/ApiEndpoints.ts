const vsPort = "7021"
const vsCodePort = "5160"
const baseUrls = `https://localhost:${vsPort}/api/`
//const baseUrls = `http://localhost:${vsCodePort}/api/`

const apiLinks = {
    auth: {
        postLogin: `${baseUrls}Users/login`
    },
    patients:{
        getPatients:`${baseUrls}Users/patients`,
        gtPatientById:`${baseUrls}Patient/`,
        postPatient:`${baseUrls}Users/patient-contact`
    },
    medicalRecords:{
        getMedicalRecords:`${baseUrls}MedicalRecords`,
        // getMedicalRecordsByPatientId:`${baseUrls}MedicalRecords/id/:patientId?pageIndex=1&pageSize=5`,
        getMedicalRecordsByPatientId:`${baseUrls}MedicalRecords/id/`,
        postMedicalRecord:`${baseUrls}MedicalRecords`,
        getMedicalRecordById:`${baseUrls}MedicalRecords/detail/id/`,
        patchMrStatusPaid:`${baseUrls}MedicalRecords/payment/id/`,
        patchMrStatusCheckUp:`${baseUrls}MedicalRecords/check-up/id/`,
        pathUpdateMr:`${baseUrls}MedicalRecords/id/`,
        getReCheckUpMrByPrevMrId:`${baseUrls}MedicalRecords/recheck-up/id/`,
    },
    category:{
        getCategories:`${baseUrls}Category`,
        postCategory:`${baseUrls}Category`,
    },
    serviceType:{
        getServicesType:`${baseUrls}ServiceType/`,
    },
    service:{
        getServices:`${baseUrls}ServiceType/service/`,
    }
    ,
    doctor:{
        getDoctorByCategoryId:`${baseUrls}Users/doctor/id/`,
        getLeastAssignedDoctorByCategoryId: `${baseUrls}Users/doctor/least-assigned/id/`
    },
    examination:{
        postAddExamResult:`${baseUrls}ExaminationResult`,
        getListServicesByCategoryId:`${baseUrls}ExaminationResult/detail/id/`,
        putUpdateExaminationResult:`${baseUrls}ExaminationResult/detail/id/`,
        getExamResultByMedicalRecordId:`${baseUrls}ExaminationResult/id/`,
    },
    supply:{
        getAllSupplyTypes:`${baseUrls}SuppliesType`,
        getSuppliesBySupplyTypeId:`${baseUrls}SuppliesType/supplies/`,
        postSuppliesPrescriptionByMrId:`${baseUrls}SuppliesType/supplies-prescription/`,
        getSelectedSuppliesByMrId:`${baseUrls}SuppliesType/selected-supplies/`,
    }
}

export default apiLinks