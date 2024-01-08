export interface CategoryAddModel {
  categoryName: string;
}

export interface ServiceTypeAddModel {
  name: string;
}

export interface ServiceAddModel {
  name: string;
}

export interface BaseModel {
  id: number;
  name: string;
  parentId?: number;
}

export interface CategoryResponseModel {
  categoryId: number;
  categoryName: string;
}

export interface ServiceTypeResponseModel {
    serviceTypeId: number;
    serviceTypeName: string;
}

export interface ServiceResponseModel {
    serviceId: number;
    serviceName: string;
    price: number;
}


export interface DoctorResponseModel {
    userId: number,
    userName: string,
}

export interface ExaminationsResultModel {
  medicalRecordId: number;
  examDetails: ExamDetail[];
  diagnosis?: string;
  conclusion?: string;
}
export interface ExamDetail {
  medicalRecordId: number;
  serviceId: number;
  serviceName: string;
  description: string;
  diagnose: string;
  price?: number;
  status?: boolean;
}

export interface ExamResultAddModel{
  medicalRecordId: number;
  diagnosis: string;
  conclusion: string;
}

export interface ExamResultGetModel{
  examResultId: number,
  diagnosis: string,
  conclusion: string,
  examDate?: string
}

export interface SupplyTypeResponseModel {
  suppliesTypeId: number
  suppliesTypeName: string
  supplies: SupplyResponseModel[]
}

export interface SupplyResponseModel {
  sId: number
  sName: string
  uses: string
  exp: string
  distributor: string
  unitInStock: number
  price: number
  inputday: string
  suppliesTypeId: number
}

export interface SuppliesPresAddModel {
  medicalRecordId: number
  supplyIds: SupplyIdPreAddModel[]
}

export interface SupplyIdPreAddModel {
  supplyId: number
  quantity: number
}

export interface SupplyPresSelectFormModel {
  medicalRecordId: number
  selectedSupplies: SupplyIdSelectFormModel[]
  selectedSupplyTypes: SupplyTypeIdSelectFormModel[]
}

export interface SupplyIdSelectFormModel {
  supplyId: number
  quantity: number
}

export interface SupplyTypeIdSelectFormModel {
  supplyId: number
  quantity: number
}

export interface SelectedSuppliesResponseModel {
  supplyId: number
  supplyName: string
  quantity: number
  price: number
}
