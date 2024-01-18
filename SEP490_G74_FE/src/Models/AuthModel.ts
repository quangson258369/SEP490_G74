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

export interface AccountResponseModel {
  userId: number;
  userName: string;
  email: string;
  roleId: number;
  roleName: string;
}

export interface AddAccountModel {
  password: string
  confirmPassword: string
  email: string
  status: boolean
  roleId: number
  categoryId: number
  roles?: RoleResponseModel[]
}

export interface UpdateAccountModel {
  userId: number
  password: string
  confirmPassword: string
  roleId: number
  categoryId: number
}

export interface RoleResponseModel {
  roleId: number;
  roleName: string;
}