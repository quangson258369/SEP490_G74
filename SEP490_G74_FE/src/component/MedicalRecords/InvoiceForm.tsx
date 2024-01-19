import { Button, Col, Divider, Form, Row, message } from "antd";
import { ExaminationProps } from "../../Models/MedicalRecordModel";
import ExaminationService from "../../Services/ExaminationService";
import { ExamDetail } from "../../Models/SubEntityModel";
import { useEffect, useState } from "react";
import { PatientAddModel } from "../../Models/PatientModel";
import patientService from "../../Services/PatientService";
import dayjs from "dayjs";
import generatePDF from "react-to-pdf";
import { JWTTokenModel } from "../../Models/AuthModel";
import { TOKEN } from "../../Commons/Global";
import { jwtDecode } from "jwt-decode";

const InvoiceForm = ({
  medicalRecordId,
  isReload,
  patientId,
  isPaid,
}: ExaminationProps) => {
  const [invoice, setInvoice] = useState<ExamDetail[] | undefined>(undefined);
  const [patient, setPatient] = useState<PatientAddModel | undefined>(
    undefined
  );
  const [total, setTotal] = useState<number>(0);
  const [invoiceForm] = Form.useForm();

  const fetchInvoice = async (medicalRecordId: number) => {
    var response = await ExaminationService.getListExamServicesByMrId(
      medicalRecordId
    );
    if (response === undefined) {
      message.error("Có lỗi xảy ra, vui lòng thử lại sau", 2);
    } else {
      console.log(response);
      setInvoice(response.examDetails);

      const totalPrice = response.examDetails.reduce(
        (accumulator, examDetail) => accumulator + examDetail.price!,
        0
      );
      setTotal(totalPrice);
      invoiceForm.setFieldsValue({
        totalPrice: totalPrice,
        examDetails: response.examDetails,
      });
    }
  };

  const fetchPatient = async () => {
    if (patientId === undefined) return;
    //PatientAddModel
    var response: PatientAddModel | undefined =
      await patientService.getPatientById(patientId);
    if (response === undefined) {
      message.error("Get Patient Failed", 2);
    } else {
      console.log(response);
      setPatient(response);
    }
  };

  const [name, setName] = useState<string>("");

  const handlePayServiceMr = async (
    medicalRecordId: number,
    serviceId: number
  ) => {
    if(isPaid){
      message.info("Hóa đơn đã thanh toán không thể chỉnh sửa", 2)
      return;
    }
    var response = await ExaminationService.patchPayServiceMr(
      medicalRecordId,
      serviceId
    );
    if (response === undefined && response !== 200) {
      message.error("Có lỗi xảy ra, vui lòng thử lại sau", 2);
    } else {
      message.success("Thanh toán thành công", 2).then(() => {
        window.location.reload();
      });
    }
  };

  useEffect(() => {
    var token = localStorage.getItem(TOKEN);
    if (token !== null) {
      var user: JWTTokenModel | undefined = jwtDecode(token);
      if (user !== undefined) {
        if (user.unique_name === undefined) user.unique_name = "";
        setName(user.unique_name);
      }
    }

    fetchPatient();
    fetchInvoice(medicalRecordId);
  }, [isReload]);
  const getTargetElement = () => document.getElementById("printInvoice");
  return invoice === undefined ? (
    <></>
  ) : (
    <div>
      <Row>
        <Col
          span={24}
          style={{
            display: "flex",
            justifyContent: "flex-end",
            alignItems: "flex-end",
          }}
        >
          <Button type="primary" onClick={() => generatePDF(getTargetElement)}>
            In hóa đơn
          </Button>
        </Col>
      </Row>
      <div id="printInvoice" style={{ padding: "20px" }}>
        {patient !== undefined ? (
          <div>
            <div
              style={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center",
              }}
            >
              <Row>
                <Col span={12}>
                  <h1>
                    <b>Phòng khám HCS</b>
                  </h1>
                </Col>
                <Col span={12}>
                  <h1 style={{ textWrap: "nowrap" }}>
                    <b>Hóa đơn khám sức khỏe</b>
                  </h1>
                </Col>
              </Row>
            </div>
            <br />
            <br />

            <Row>
              <Col span={24}>
                <h2>
                  <b>Thông tin bệnh nhân</b>
                </h2>
              </Col>
            </Row>
            <br />
            <Row>
              <Col span={8}>
                <b>Họ tên bệnh nhân</b>
              </Col>
              <Col span={16}>{patient?.name}</Col>
            </Row>
            <Row>
              <Col span={8}>
                <b>Địa chỉ</b>
              </Col>
              <Col span={16}>{patient?.address}</Col>
            </Row>
            <Row>
              <Col span={8}>
                <b>Số điện thoại</b>
              </Col>
              <Col span={16}>{patient?.phone}</Col>
            </Row>
            <Row>
              <Col span={8}>
                <b>Ngày sinh</b>
              </Col>
              <Col span={16}>
                {dayjs(patient.dob).format("YYYY-MM-DD HH:mm:ss")}
              </Col>
            </Row>
            <br />
            <br />
          </div>
        ) : (
          <></>
        )}
        <div style={{ minWidth: "1000px", width: "max-content" }}>
          <Row>
            <Col span={8}>
              <b>Tên dịch vụ</b>
            </Col>
            <Col span={4}>
              <b>Trạng thái</b>
            </Col>
            <Col span={6}>
              <b>Giá</b>
            </Col>
            <Col span={6}></Col>
          </Row>
          <Divider />
          {invoice.map((examDetail) => (
            <div key={examDetail.serviceId}>
              <Row>
                <Col span={8}>{examDetail.serviceName}</Col>
                <Col span={4}>
                  {examDetail.isPaid === true
                    ? "Đã thanh toán"
                    : "Chưa thanh toán"}
                </Col>
                <Col span={6}>{examDetail.price?.toLocaleString()} VND</Col>
                <Col span={6}>
                  <Button
                    type="primary"
                    disabled={examDetail.isPaid === true ? true : false}
                    onClick={() =>
                      handlePayServiceMr(
                        examDetail.medicalRecordId,
                        examDetail.serviceId
                      )
                    }
                  >
                    Thanh toán
                  </Button>
                </Col>
              </Row>
              <Divider />
            </div>
          ))}
          <Row>
            <Col span={8}>
              <h3>
                <b>Tổng tiền</b>
              </h3>
            </Col>
            <Col span={6} />
            <Col span={10}>
              <h3>
                <b>{total.toLocaleString()} VND</b>
              </h3>
            </Col>
          </Row>
          <br />
          <br />
          <br />
          <Row>
            <Col span={24}>
              <b>Nhân viên thu ngân</b>
            </Col>
          </Row>
          <Row>
            <Col span={24}>{name}</Col>
          </Row>
        </div>
      </div>
    </div>
  );
};

export default InvoiceForm;
