import { Space, Button, Modal, message, Row, Col, Divider } from "antd";
import Table, { ColumnsType } from "antd/es/table";
import { MedicalRecordTableModel } from "../../Models/MedicalRecordModel";
import { useContext, useEffect, useRef, useState } from "react";
import MedicalRecordDetailForm from "./MedicalRecordDetailForm";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import medicalRecordService from "../../Services/MedicalRecordService";
import { useNavigate, useParams } from "react-router-dom";
import dayjs from "dayjs";
import GenericModal from "../Generic/GenericModal";
import ExaminationForm from "./ExaminationForm";
import ExaminationService from "../../Services/ExaminationService";
import InvoiceForm from "./InvoiceForm";
import SupplyPrescriptionDetailForm from "./SupplyPrescriptionDetailForm";
import { ApiResponseModel } from "../../Models/PatientModel";

const MedicalRecordTable = () => {
  const { id } = useParams<{ id: string }>();
  const [open, setOpen] = useState<boolean>(false);
  const [openExaminate, setOpenExaminate] = useState<boolean>(false);
  const [selectedPatientId, setSelectedPatientId] = useState<number>(1);
  const [selectedMrId, setSelectedMrId] = useState<number>(1);
  const { authenticated } = useContext(AuthContext);
  const [isReOpen, setIsReOpen] = useState<boolean>(false);
  const [isExamReload, setIsExamReload] = useState<boolean>(false);
  const [isInvoiceReload, setIsInvoiceReload] = useState<boolean>(false);
  const [isSupplyPresReload, setIsSupplyPresReload] = useState<boolean>(false);
  const [openInvoice, setOpenInvoice] = useState<boolean>(false);
  const [openSupplyPres, setOpenSupplyPres] = useState<boolean>(false);

  //Pagination
  const [pagination, setPagination] = useState({ current: 1, pageSize: 5, total: 0 });

  const handleViewMedicalRecord = (id: number, mrId: number) => {
    setOpen(true);
    setSelectedPatientId(id);
    setSelectedMrId(mrId);
    setIsReOpen(!isReOpen);
  };

  const [medicalRecords, setMedicalRecords] = useState<
    MedicalRecordTableModel[]
  >([]);

  const navigate = useNavigate();

  const handleCancel = () => {
    setOpen(false);
  };

  const handleCancelExaminate = () => {
    setOpenExaminate(false);
  };

  const handleCancelInvoice = () => {
    setOpenInvoice(false);
  };

  const handleCancelSupplyPres = () => {
    setOpenSupplyPres(false);
  };

  const handlePaid = async () => {
    if (
      medicalRecords.find((item) => item.medicalRecordId === selectedMrId)
        ?.isCheckUp === true
    ) {
      message.info("Hồ sơ đã khám", 2);
      return;
    }

    var statusCode = await medicalRecordService.updateMrCheckUpStatus(
      selectedMrId
    );
    if (statusCode === 200) {
      message.success("Đã khám hồ sơ " + selectedMrId, 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error(`Hồ sơ ${selectedMrId} phải thanh toán trước khi khám`, 2);
    }
  };

  const handleCheckout = async (id: number) => {
    if (
      authenticated?.role !== Roles.Cashier &&
      authenticated?.role !== Roles.Admin
    ) {
      message.error("Chức năng chỉ dành cho thu ngân", 2);
    } else {
      if (
        medicalRecords.find((item) => item.medicalRecordId === id)?.isPaid ===
        true
      ) {
        message.info("Hồ sơ đã thanh toán", 2);
        return;
      }
      var statusCode = await medicalRecordService.updateMrPaidStatus(id);
      if (statusCode === 200) {
        message.success("Đã thanh toán hồ sơ: " + id, 2).then(() => {
          window.location.reload();
        });
      } else {
        message.error("Lỗi khi thanh toán hồ sơ: " + id, 2);
      }
    }
  };

  const handleExaminate = (mrId: number, isCheckUp: boolean) => {
    if (isCheckUp === false) {
      message.info("Hồ sơ chưa khám", 2);
      return;
    }
    setIsExamReload(!isExamReload);
    setOpenExaminate(true);
    setSelectedMrId(mrId);
  };

  const handleInvoice = (mrId: number) => {
    setIsInvoiceReload(!isInvoiceReload);
    setOpenInvoice(true);
    setSelectedMrId(mrId);
  };

  const handleSupplyPres = (mrId: number) => {
    setIsSupplyPresReload(!isSupplyPresReload);
    setOpenSupplyPres(true);
    setSelectedMrId(mrId);
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
      render: (text) => <a>{dayjs(text).format("YYYY-MM-DD HH:mm:ss")}</a>,
    },
    {
      title: "Thanh toán",
      dataIndex: "isPaid",
      key: "isPaid",
      render: (record) => (
        <a>{record === true ? "Đã thanh toán" : "Chưa thanh toán"}</a>
      ),
    },
    {
      title: "Khám",
      dataIndex: "isCheckUp",
      key: "isCheckUp",
      render: (record) => <a>{record === true ? "Đã khám" : "Chưa khám"}</a>,
    },
    {
      title: "",
      key: "action-1",
      render: (_, record) => (
        <div>
          <Row gutter={[5, 5]}>
            <Col>
              <Button
                key="view"
                type="primary"
                onClick={() =>
                  handleViewMedicalRecord(
                    record.patientId,
                    record.medicalRecordId
                  )
                }
              >
                Xem hồ sơ
              </Button>
            </Col>
            <Col>
              <Button
                key="examinate"
                type="primary"
                onClick={() =>
                  handleExaminate(record.medicalRecordId, record.isCheckUp)
                }
              >
                Kết luận
              </Button>
            </Col>
          </Row>
        </div>
      ),
    },
    {
      title: "",
      key: "action",
      render: (_, record) => (
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            flexDirection: "column",
          }}
        >
          <Row gutter={[5, 5]}>
            <Col>
              <Button
                key="checkout"
                type="primary"
                onClick={() => handleCheckout(record.medicalRecordId)}
              >
                Thanh toán
              </Button>
            </Col>
          </Row>
          <div style={{ height: "5px" }} />
          <Row gutter={[5, 5]}>
            <Col>
              <Button
                key="checkout"
                type="primary"
                onClick={() => handleSupplyPres(record.medicalRecordId)}
              >
                Đơn thuốc
              </Button>
            </Col>
            <Col>
              <Button
                key="checkout"
                type="primary"
                onClick={() => handleInvoice(record.medicalRecordId)}
              >
                Hóa đơn
              </Button>
            </Col>
          </Row>
        </div>
      ),
    },
  ];

  const fetchMedicalRecords = async () => {
    if (id !== undefined) {
      const patientId = parseInt(id);
      if (patientId === undefined || patientId === null || isNaN(patientId)) {
        message
          .error("Lỗi khi lấy dữ liệu hồ sơ bệnh nhân", 2)
          .then(() => navigate("/"));
      } else {
        var response: ApiResponseModel | undefined =
          await medicalRecordService.getMedicalRecordsByPatientId(patientId, pagination.current, pagination.pageSize);
        if (response === undefined) {
          message.error("Lỗi khi lấy dữ liệu hồ sơ bệnh nhân", 2);
        } else {
          console.log(response);
          const mappedResponse: MedicalRecordTableModel[] = response.items.map(
            (item) => ({ ...item, key: item.medicalRecordId + "" })
          );
          setMedicalRecords(mappedResponse);
          setPagination({
            ...pagination,
            total: response.totalCount, // Update total count
          });
        }
      }
    } else {
      message
        .error("Lỗi khi lấy dữ liệu hồ sơ bệnh nhân", 2)
        .then(() => navigate("/"));
    }
  };

  const handleTableChange = (pagination:any) => {
    setPagination(pagination);
  };

  useEffect(() => {
    fetchMedicalRecords();
  }, [pagination.current, pagination.pageSize]);

  return (
    <div style={{ minHeight: "100vh", height: "auto" }}>
      <Table columns={columns} dataSource={medicalRecords} rowKey={record => record.medicalRecordId} pagination={pagination} onChange={handleTableChange}/>

      <GenericModal
        title="Hồ sơ bệnh nhân"
        isOpen={open}
        onClose={handleCancel}
        childComponent={
          <div>
            <MedicalRecordDetailForm
              patientId={selectedPatientId}
              medicalRecordId={selectedMrId}
              isReload={isReOpen}
            />
          </div>
        }
        footer={[
          <Button key="back" onClick={handleCancel}>
            Hủy
          </Button>,
          <Button key="paid" onClick={handlePaid}>
            Đã khám
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
      />

      {/* =============== Modal Examination Result */}
      <GenericModal
        isOpen={openExaminate}
        onClose={handleCancelExaminate}
        title={`Kết luận hồ sơ ${selectedMrId}`}
        childComponent={
          <ExaminationForm
            isReload={isExamReload}
            medicalRecordId={selectedMrId}
          />
        }
        footer={[
          <Button key="back" onClick={handleCancelExaminate}>
            Hủy
          </Button>,
          <Button
            key="submit"
            type="primary"
            form="examAddForm"
            htmlType="submit"
          >
            Lưu kết luận tổng
          </Button>,
        ]}
      />

      {/* =============== Modal Invoice */}
      <GenericModal
        isOpen={openInvoice}
        onClose={handleCancelInvoice}
        title={`Hóa đơn hồ sơ ${selectedMrId}`}
        childComponent={
          <InvoiceForm
            isReload={isInvoiceReload}
            medicalRecordId={selectedMrId}
            patientId={selectedPatientId}
          />
        }
        footer={[
          <Button key="back" onClick={handleCancelInvoice}>
            Hủy
          </Button>,
        ]}
      />

      {/* =============== Modal Supply Prescription */}
      <GenericModal
        isOpen={openSupplyPres}
        onClose={handleCancelSupplyPres}
        title={`Đơn thuốc hồ sơ ${selectedMrId}`}
        childComponent={
          <SupplyPrescriptionDetailForm
            isReload={isSupplyPresReload}
            medicalRecordId={selectedMrId}
          />
        }
        footer={[
          <Button key="back" onClick={handleCancelSupplyPres}>
            Hủy
          </Button>,
          <Button
            key="submit"
            type="primary"
            form="supplyPresDetailForm"
            htmlType="submit"
          >
            Lưu
          </Button>,
        ]}
      />
    </div>
  );
};

export default MedicalRecordTable;
