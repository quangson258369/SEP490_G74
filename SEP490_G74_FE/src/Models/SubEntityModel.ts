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
