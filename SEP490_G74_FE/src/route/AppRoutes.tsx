import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom"
import LoginPage from "../page/LoginPage"
import NursePage from "../page/NursePage"
import PatientTable from "../component/patient/PatientTable"
import HomePage from "../page/HomePage"
import ProtectedRoute from "../ProtectedRoute/ProtectedRoute"
import DoctorLayoutPage from "../LayoutPages/DoctorLayoutPage"
import Roles from "../Enums/Enums"
import DoctorPage from "../page/DoctorPage"
import AdminPage from "../page/AdminPage"
import AdminLayoutPage from "../LayoutPages/AdminLayoutPage"
import ErrorPage from "../page/ErrorPage"
import UnauthorizedPage from "../page/UnauthorizedPage"
import NurseLayoutPage from "../LayoutPages/NurseLayoutPage"
import MedicalRecordAddPage from "../page/MedicalRecordAddPage"

const AppRoutes = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/login" element={<LoginPage />} />

                <Route path="/admin/patients" element={<ProtectedRoute role={[Roles.Admin]} />}>
                    <Route path="" element={<AdminLayoutPage childComp={<AdminPage childComp={<PatientTable />} />} />} />
                    <Route path=":id/add-medical-record" element={<AdminLayoutPage childComp={<MedicalRecordAddPage/>}/>} />
                </Route>

                <Route path="/doctor/patients" element={<ProtectedRoute role={[Roles.Doctor]} />}>
                    <Route path="" element={<DoctorLayoutPage childComp={<DoctorPage childComp={<PatientTable />} />} />} />
                </Route>

                <Route path="/nurse/patients" element={<ProtectedRoute role={[Roles.Nurse]} />}>
                    <Route path="" element={<NurseLayoutPage childComp={<NursePage childComp={<PatientTable />} />} />} />
                </Route>

                <Route path="*" element={<ErrorPage />} />
                <Route path="/404" element={<UnauthorizedPage />} />
            </Routes>
        </BrowserRouter>
    )
}
export default AppRoutes;