import { useNavigate, useParams } from "react-router-dom"
import MedicalRecordAddForm from "../component/MedicalRecordAddForm"
import { useEffect, useState } from "react"

const MedicalRecordAddPage = () => {
    const navigate = useNavigate()
    const { id } = useParams()
    const [patientId, setPatientId] = useState<number>(0)
    const numberRegex = /^\d+$/;

    const validateUserId = () => {
        if (id === undefined) {
            navigate("/")
        } else {
            if (!numberRegex.test(id)) {
                navigate("/")
            } else {
                setPatientId(Number.parseInt(id))
            }
        }
    }

    useEffect(() => {
        validateUserId()
    }, [patientId])

    return (
        <div>
            MR Add Page | Patient ID: {id}
            <br />
            <MedicalRecordAddForm patientId={patientId} />
        </div>
    )
}

export default MedicalRecordAddPage