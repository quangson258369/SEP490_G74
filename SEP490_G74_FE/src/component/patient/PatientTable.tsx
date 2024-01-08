import { Space, Button, Modal, message } from "antd";
import Table, { ColumnsType } from "antd/es/table";
import { PatientTableModel } from "../../Models/MedicalRecordModel";
import { useNavigate } from "react-router-dom";
import MedicalRecordAddForm from "../MedicalRecords/MedicalRecordAddForm";
import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import patientService from "../../Services/PatientSerivce";
import { ApiResponseModel, PatientTableResponseModel } from "../../Models/PatientModel";

const PatientTable = () => {
  const navigate = useNavigate();
  const [open, setOpen] = useState<boolean>(false);
  const [selectedPatientId, setSelectedPatientId] = useState<
    number | undefined
  >(undefined);

  const [patients, setPatients] = useState<PatientTableResponseModel[]>([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 5,
    total: 0,
  });

  const { authenticated } = useContext(AuthContext);

  const handleCancel = () => {
    setOpen(false);
  };

  const handleViewPatient = (id: number) => {
    if (authenticated?.role === Roles.Doctor) {
      message.error("Chức năng chỉ dành cho y tá", 2);
    } else {
      setOpen(true);
      setSelectedPatientId(id);
    }
  };

  const handleViewMRs = (id: number) => {
    navigate(`${id}/medical-records`);
  };

  const columns: ColumnsType<PatientTableResponseModel> = [
    {
      title: "Mã bệnh nhân",
      dataIndex: "patientId",
      key: "patientId",
      render: (text) => <a>{text}</a>,
    },
    {
      title: "Họ và tên",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Ngày sinh",
      dataIndex: "dob",
      key: "dob",
      render: (text) => <span>{new Date(text).toLocaleDateString()}</span>,
    },
    {
      title: "Giới tính",
      dataIndex: "gender",
      key: "gender",
      render: (gender) =>
        gender === true ? <span>Nam</span> : <span>Nữ</span>,
    },
    {
      title: "Số điện thoại",
      dataIndex: "phone",
      key: "phone",
    },
    {
      title: "Địa chỉ",
      dataIndex: "address",
      key: "address",
    },
    {
      title: "",
      key: "action",
      render: (record: PatientTableResponseModel) => (
        <Space size="middle">
          <Button
            type="primary"
            onClick={() => handleViewPatient(record.patientId)}
          >
            Thêm bệnh án
          </Button>
          <Button
            type="primary"
            onClick={() => handleViewMRs(record.patientId)}
          >
            Danh sách khám
          </Button>
        </Space>
      ),
    },
  ];

  const fetchPatient = async () => {
    // var result: PatientTableResponseModel[] | undefined =
    //   await patientService.getPatients(pagination.current, pagination.pageSize);
    // if (result === undefined) {
    //   message.error("Lỗi lấy danh sách bệnh nhân", 2);
    // } else {
    //   var pats: PatientTableResponseModel[] = result.map((item) => ({
    //     ...item,
    //     key: item.patientId + "key",
    //   }));
    //   setPatients(pats);
    //   console.log(result);
    //   setPagination({
    //     ...pagination,
    //     total: result.length, // Update total count
    //   });
    // }
    var result: ApiResponseModel | undefined =
      await patientService.getPatients(pagination.current, pagination.pageSize);
    if (result === undefined) {
      message.error("Lỗi lấy danh sách bệnh nhân", 2);
    } else {
      var pats: PatientTableResponseModel[] = result.items.map((item) => ({
        ...item,
        key: item.patientId + "key",
      }));
      setPatients(pats);
      console.log(result);
      setPagination({
        ...pagination,
        total: result.totalCount, // Update total count
      });
    }
  };

  const handleTableChange = (pagination: any) => {
    setPagination(pagination);
  };

  useEffect(() => {
    fetchPatient();
  }, [authenticated, pagination.current, pagination.pageSize]);

  return (
    <div style={{ minHeight: "100vh", height: "auto" }}>
      {patients !== undefined && (
        <Table
          columns={columns}
          dataSource={patients}
          rowKey={(record) => record.patientId}
          pagination={pagination}
          onChange={handleTableChange}
        />
      )}
      <Modal
        title="Thêm hồ sơ bệnh án"
        open={open}
        onCancel={handleCancel}
        maskClosable={false}
        width="max-content"
        footer={[
          <Button key="back" onClick={handleCancel}>
            Hủy
          </Button>,
          <Button
            key="submit"
            type="primary"
            form="medicalRecordAddForm"
            htmlType="submit"
          >
            Tạo hồ sơ
          </Button>,
        ]}
      >
        {selectedPatientId !== undefined && (
          <MedicalRecordAddForm patientId={selectedPatientId} />
        )}
      </Modal>
    </div>
  );
};

export default PatientTable;
