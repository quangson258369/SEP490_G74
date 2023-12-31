import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "../Pages/LoginPage";
import NursePage from "../Pages/NursePage";
import PatientTable from "../component/Patient/PatientTable";
import HomePage from "../Pages/HomePage";
import ProtectedRoute from "../ProtectedRoute/ProtectedRoute";
import DoctorLayoutPage from "../LayoutPages/DoctorLayoutPage";
import Roles from "../Enums/Enums";
import DoctorPage from "../Pages/DoctorPage";
import AdminPage from "../Pages/AdminPage";
import AdminLayoutPage from "../LayoutPages/AdminLayoutPage";
import ErrorPage from "../Pages/ErrorPage";
import UnauthorizedPage from "../Pages/UnauthorizedPage";
import NurseLayoutPage from "../LayoutPages/NurseLayoutPage";
import MedicalRecordsPage from "../Pages/MedicalRecordsPage";
import { ROUTE_URLS } from "../Commons/Global";
import SubEntityTable from "../component/Admin/SubEntityTable";

const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path={ROUTE_URLS.HOME_PAGE} element={<HomePage />} />
        <Route path={ROUTE_URLS.LOGIN_PAGE} element={<LoginPage />} />

        <Route
          path={ROUTE_URLS.ADMIN_HOME_PAGE}
          element={<ProtectedRoute role={[Roles.Admin]} />}
        >
          <Route
            path=""
            element={
              <AdminLayoutPage
                childComp={<AdminPage childComp={<PatientTable />} />}
              />
            }
          />

          <Route
            path={ROUTE_URLS.MEDICAL_RECORDS_PAGE}
            element={
              <AdminLayoutPage
                childComp={<AdminPage childComp={<MedicalRecordsPage />} />}
              />
            }
          />
        </Route>
        <Route
          path={ROUTE_URLS.ADMIN_SUB_PAGE}
          element={<ProtectedRoute role={[Roles.Admin]} />}
        >
          <Route
            path=""
            element={
              <AdminLayoutPage
                childComp={<AdminPage childComp={<SubEntityTable />} />}
              />
            }
          />
        </Route>

        <Route
          path={ROUTE_URLS.DOCTOR_HOME_PAGE}
          element={<ProtectedRoute role={[Roles.Doctor]} />}
        >
          <Route
            path=""
            element={
              <DoctorLayoutPage
                childComp={<DoctorPage childComp={<PatientTable />} />}
              />
            }
          />

          <Route
            path={ROUTE_URLS.MEDICAL_RECORDS_PAGE}
            element={
              <DoctorLayoutPage
                childComp={<DoctorPage childComp={<MedicalRecordsPage />} />}
              />
            }
          />
        </Route>

        <Route
          path={ROUTE_URLS.NURSE_HOME_PAGE}
          element={<ProtectedRoute role={[Roles.Nurse]} />}
        >
          <Route
            path=""
            element={
              <NurseLayoutPage
                childComp={<NursePage childComp={<PatientTable />} />}
              />
            }
          />

          <Route
            path={ROUTE_URLS.MEDICAL_RECORDS_PAGE}
            element={
              <NurseLayoutPage
                childComp={<NursePage childComp={<MedicalRecordsPage />} />}
              />
            }
          />
        </Route>

        <Route path={ROUTE_URLS.ERROR_PAGE} element={<ErrorPage />} />
        <Route
          path={ROUTE_URLS.UNAUTHORIZED_PAGE}
          element={<UnauthorizedPage />}
        />
      </Routes>
    </BrowserRouter>
  );
};
export default AppRoutes;
