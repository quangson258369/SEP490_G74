import { Tag, Space, Button } from "antd";
import Table, { ColumnsType } from "antd/es/table";
import { PatientTableModel } from "../../Models/MedicalRecordModel";
import { useNavigate } from "react-router-dom";

const PatientTable = () => {
    const navigate = useNavigate()

    const handleViewPatient = (id: number) => {
        navigate(`${id}/add-medical-record`)
    }

    const handleViewMRs = (id: number) => {
        alert(id)
    }

    const columns: ColumnsType<PatientTableModel> = [
        {
            title: 'Mã bệnh nhân',
            dataIndex: 'id',
            key: 'id',
            render: (text) => <a>{text}</a>,
        },
        {
            title: 'Họ và tên',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Ngày sinh',
            dataIndex: 'dob',
            key: 'dob',
        },
        {
            title: 'Giới tính',
            dataIndex: 'gender',
            key: 'gender',
            render: (gender) => gender === true ? <span>Female</span> : <span>Male</span>
        },
        {
            title: 'Số điện thoại',
            dataIndex: 'phone',
            key: 'phone',
        },
        {
            title: 'Địa chỉ',
            dataIndex: 'address',
            key: 'address',
        },
        {
            title: '',
            key: 'action',
            render: (_, record) => (
                <Space size="middle">
                    <Button type="primary" onClick={() => handleViewPatient(record.id)}>Thêm bệnh án</Button>
                    <Button type="primary" onClick={() => handleViewMRs(record.id)}>Danh sách khám</Button>
                </Space>
            ),
        },
    ];

    const data: PatientTableModel[] = [
        {
            id: 1,
            name: "Nguyen Van A",
            dob: "12/12/2020",
            gender: true,
            phone: "0123456689",
            address: "q9",
            key: `1`
        },
        {
            id: 2,
            name: "Nguyen Van B",
            dob: "12/12/2020",
            gender: true,
            phone: "0123456689",
            address: "q9",
            key: "2"
        },
        {
            id: 3,
            name: "Nguyen Van C",
            dob: "12/12/2020",
            gender: true,
            phone: "0123456689",
            address: "q9",
            key: "3"
        },
    ];

    return (
        <>
            <Table columns={columns} dataSource={data} />
        </>
    )
}

export default PatientTable;