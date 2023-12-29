import { useNavigate } from "react-router-dom"

const UnauthorizedPage = () => {
    const navigate = useNavigate()
    const handleBack = () => {
        navigate("/")
    }
    return (
        <div>
            <div>UnauthorizedPage</div>
            <button onClick={()=>handleBack()}>Back To Your Page</button>
        </div>
    )
}

export default UnauthorizedPage