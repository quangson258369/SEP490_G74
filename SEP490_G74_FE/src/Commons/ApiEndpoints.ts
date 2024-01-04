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
    }
}

export default apiLinks