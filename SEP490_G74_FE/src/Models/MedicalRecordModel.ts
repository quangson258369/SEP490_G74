export interface Doctor {
    id: number
    name: string
    categoryId: number
    roleId: number
}

export interface Category{
    categoryId: number
    name: string
}

export interface ServiceType{
    id: number
    name: string
    services: Service[]
}

export interface Service {
    id: number
    name: string
    serviceTypeId: number
}

export interface MedicalRecord extends PatientTableModel{
    patientId: number
    height: number
    weight: number
    blood: string
    bloodPressure: number
    editDate: string
    description: string
    selectedCategoryId: number
    selectedServiceTypeIds: number[]
    selectedServiceIds: number[]
    selectedDoctorId: number
}

export interface MedicalRecordTableModel{
    medicalRecordId: number
    medicalRecordDate: string
    categoryName: string
    name: string
    patientId: number
    key: string
}

export interface PatientTableModel{
    id :number
    name: string
    dob: string
    gender: boolean
    phone: string
    address: string
    key: string
}

export interface PatientProps{
    patientId : number
    role?: string
}

export interface MedicalRecordAddModel {
    medicalRecordDate: string;
    examReason: string;
    examCode: string;
    categoryId: number;
    patientId: number;
    doctorId: number;
    prescriptionId: number;
  }