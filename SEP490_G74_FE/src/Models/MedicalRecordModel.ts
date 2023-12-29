export interface Doctor {
    id: number
    name: string
    categoryId: number
    roleId: number
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
    serviceTypes: ServiceType[]
    selectedServices: Service[]
    doctors: Doctor[]
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
}