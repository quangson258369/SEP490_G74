import { Space, Button, Modal, message } from "antd";
import Table, { ColumnsType } from "antd/es/table";
import { MedicalRecordTableModel } from "../../Models/MedicalRecordModel";
import { useContext, useEffect, useState } from "react";
import MedicalRecordDetailForm from "./MedicalRecordDetailForm";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import medicalRecordService from "../../Services/MedicalRecordService";
import { useNavigate, useParams } from "react-router-dom";

const MedicalRecordTable = () => {
  const [open, setOpen] = useState<boolean>(false);
  const [selectedPatientId, setSelectedPatientId] = useState<number>(1);
  const { authenticated } = useContext(AuthContext);
  const { id } = useParams<{ id: string }>();
  const handleViewMedicalRecord = (id: number) => {
    console.log(id);
    setOpen(true);
    setSelectedPatientId(id);
  };
  const [medicalRecords, setMedicalRecords] = useState<
    MedicalRecordTableModel[]
  >([]);
  const navigate = useNavigate();

  const handleOk = () => {
    setOpen(false);
  };

  const handleCancel = () => {
    setOpen(false);
  };

  const handleCheckout = (id: number) => {
    if (
      authenticated?.role !== Roles.Cashier &&
      authenticated?.role !== Roles.Admin
    ) {
      message.error("Chức năng chỉ dành cho thu ngân", 2);
    } else {
      message.success("Đã thanh toán hồ sơ: " + id, 2);
    }
  };

  const columns: ColumnsType<MedicalRecordTableModel> = [
    {
      title: "Mã hồ sơ",
      dataIndex: "medicalRecordId",
      key: "medicalRecordId",
      render: (text) => <a>{text}</a>,
    },
    {
      title: "Mã bệnh nhân",
      dataIndex: "patientId",
      key: "patientId",
      render: (text) => <a>{text}</a>,
    },
    {
      title: "Tên bệnh nhân",
      dataIndex: "name",
      key: "name",
      render: (text) => <a>{text}</a>,
    },
    {
      title: "Ngày tháng",
      dataIndex: "medicalRecordDate",
      key: "medicalRecordDate",
    },
    {
      title: "Khoa khám",
      dataIndex: "categoryName",
      key: "categoryName",
    },
    {
      title: "",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Button
            type="primary"
            onClick={() => handleViewMedicalRecord(record.medicalRecordId)}
          >
            Xem hồ sơ
          </Button>
          <Button
            type="primary"
            onClick={() => handleCheckout(record.medicalRecordId)}
          >
            Thanh toán
          </Button>
        </Space>
      ),
    },
  ];

  // const data: MedicalRecordTableModel[] = [
  //   {
  //     id: 1,
  //     patientId: 1,
  //     name: "Nguyen Van A",
  //     editDate: "12/12/2020",
  //     category: "Nội khoa",
  //     key: `1`,
  //   },
  //   {
  //     id: 2,
  //     patientId: 2,
  //     name: "Nguyen Van B",
  //     editDate: "12/12/2020",
  //     category: "Ngoại khoa",
  //     key: `2`,
  //   },
  //   {
  //     id: 3,
  //     patientId: 3,
  //     name: "Nguyen Van C",
  //     editDate: "12/12/2020",
  //     category: "Khoa thần kinh",
  //     key: `3`,
  //   },
  // ];

  const fetchMedicalRecords = async () => {
    if (id !== undefined) {
      const patientId = parseInt(id);
      if (patientId === undefined || patientId === null || isNaN(patientId)) {
        message
          .error("Lỗi khi lấy dữ liệu hồ sơ bệnh nhân", 2)
          .then(() => navigate("/"));
      } else {
        var response: MedicalRecordTableModel[] | undefined =
          await medicalRecordService.getMedicalRecordsByPatientId(patientId);
        if (response === undefined) {
          message.error("Lỗi khi lấy dữ liệu hồ sơ bệnh nhân", 2);
        } else {
          console.log(response);
          const mappedResponse: MedicalRecordTableModel[] = response.map(
            (item) => ({ ...item, key: item.medicalRecordId + "" })
          );
          setMedicalRecords(mappedResponse);
        }
      }
    } else {
      message
        .error("Lỗi khi lấy dữ liệu hồ sơ bệnh nhân", 2)
        .then(() => navigate("/"));
    }
  };

  useEffect(() => {
    fetchMedicalRecords();
  }, []);

  return (
    <div style={{ minHeight: "100vh", height: "auto" }}>
      <Table columns={columns} dataSource={medicalRecords} />
      <Modal
        title="Hồ sơ bệnh nhân"
        open={open}
        onOk={handleOk}
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
            form="medicalRecordDetailForm"
            htmlType="submit"
          >
            Lưu
          </Button>,
        ]}
      >
        <MedicalRecordDetailForm patientId={selectedPatientId} />
      </Modal>
    </div>
  );
};

export default MedicalRecordTable;
