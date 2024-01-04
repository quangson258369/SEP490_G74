export interface LoginResponse {
  statusCode: number;
  isSuccess: boolean;
  errorMessage: string;
  result: string;
}

export interface LoginFieldsType {
  email?: string;
  password?: string;
  remember?: string;
}

export interface UserLogin {
  email: string;
  password: string;
}

export interface JWTTokenModel {
  role: string;
  unique_name: string;
  nameid?: string;
}
