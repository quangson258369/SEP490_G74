export const TOKEN = "token";

//Routing
const HOME_PAGE = "/"

const LOGIN_PAGE = "/login"

const UNAUTHORIZED_PAGE = "/401"

const ERROR_PAGE = "*"

const ADMIN_HOME_PAGE = "/admin/patients";

const DOCTOR_HOME_PAGE = "/doctor/patients";

const NURSE_HOME_PAGE = "/nurse/patients";

const MEDICAL_RECORDS_PAGE = ":id/medical-records";

const ADMIN_SUB_PAGE = "/admin/sub-entities";

export const ROUTE_URLS = {
  ADMIN_HOME_PAGE: ADMIN_HOME_PAGE,
  DOCTOR_HOME_PAGE: DOCTOR_HOME_PAGE,
  NURSE_HOME_PAGE: NURSE_HOME_PAGE,
  MEDICAL_RECORDS_PAGE: MEDICAL_RECORDS_PAGE,
  ADMIN_SUB_PAGE: ADMIN_SUB_PAGE,

  HOME_PAGE: HOME_PAGE,
  LOGIN_PAGE: LOGIN_PAGE,
  ERROR_PAGE: ERROR_PAGE,
  UNAUTHORIZED_PAGE: UNAUTHORIZED_PAGE
};
