export interface LoginResponse {
    statusCode: number
    isSuccess: boolean
    errorMessage: string
    result: string
  }

export interface FieldType {
    email?: string;
    password?: string;
    remember?: string;
};

export interface UserLogin {
    email: string
    password: string
}

export interface LoginResponse {
    nameid: string
    email: string
    unique_name: string
    role: string
    nbf: number
    exp: number
    iat: number
  }

  export interface JWTTokenModel {
    role: string
    unique_name: string
  }