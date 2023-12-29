import React, { useContext } from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ProtectedRoute from './ProtectedRoute/ProtectedRoute';
import NurseLayoutPage from './LayoutPages/NurseLayoutPage';
import NursePage from './page/NursePage';
import AdminPage from './page/AdminPage';
import AdminLayoutPage from './LayoutPages/AdminLayoutPage';
import DoctorLayoutPage from './LayoutPages/DoctorLayoutPage';
import DoctorPage from './page/DoctorPage';
import Roles from './Enums/Enums';
import LoginPage from './page/LoginPage';
import ErrorPage from './page/ErrorPage';
import HomePage from './page/HomePage';
import PatientTable from './component/patient/PatientTable';
import AppRoutes from './route/AppRoutes';
import { AuthContext, AuthProvider } from './ContextProvider/AuthContext';
import { ToastContainer } from 'react-toastify';

function App() {
  const { authenticated, setAuthenticated } = useContext(AuthContext)
  return (
    <>
      <ToastContainer limit={1} autoClose={1000} position='bottom-right'/>
      <AuthProvider children={<AppRoutes />} />
    </>
  );
}

export default App;