import { useContext } from "react";
import { AuthContext, User } from "../ContextProvider/AuthContext";
import Roles from "../Enums/Enums";
import { useNavigate } from "react-router-dom";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Button } from "antd";

const NurseLayoutPage = (props: any) => {
    const { childComp } = props;
    const { setAuthenticated } = useContext(AuthContext)
    const navigate = useNavigate()
    const handleLogout = () => {
        let user: User = {
            role: Roles.Guest
        }

        setAuthenticated(user)
        localStorage.removeItem("token")
        toast.info("Logged out")
        navigate("/login")
    }
    return (
        <div>
            <div className="header-layout" style={{ backgroundColor: "greenyellow", padding: "20px", display: "flex", justifyContent: "space-between", fontSize: "20px", fontWeight: "bold" }}>
                Nurse 
                <Button type="primary" danger={true} onClick={() => handleLogout()}>
                    Log out
                </Button>
            </div>
            <div>
                {childComp}
            </div>
        </div>
    )
}

export default NurseLayoutPage