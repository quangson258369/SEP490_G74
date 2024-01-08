import { Button, Col, Divider, Form, Input, Row, message } from "antd";
import { ExaminationProps } from "../../Models/MedicalRecordModel";
import ExaminationService from "../../Services/ExaminationService";
import {
  ExamResultAddModel,
  ExamResultGetModel,
  ExaminationsResultModel,
} from "../../Models/SubEntityModel";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import dayjs from "dayjs";

const ExaminationForm = ({ medicalRecordId, isReload }: ExaminationProps) => {
  const [examAddForm] = Form.useForm();
  const [examDetailList, setExamDetailList] = useState<
    ExaminationsResultModel | undefined
  >(undefined);
  const [examResult, setExamResult] = useState<ExamResultGetModel | undefined>(
    undefined
  );
  const [isExamConclused, setIsExamConclused] = useState<boolean>(false);

  const fetchExamination = async () => {
    if (medicalRecordId === undefined) {
      message.error("Có lỗi xảy ra, vui lòng thử lại sau");
      return;
    }

    var examDetails: ExaminationsResultModel | undefined =
      await ExaminationService.getListExamServicesByMrId(medicalRecordId);
    if (examDetails !== undefined) {
      console.log(examDetails);
      setExamDetailList(examDetails);
      examAddForm.setFieldsValue({
        medicalRecordId: examDetails.medicalRecordId,
        examDetails: examDetails.examDetails,
      });
    }
  };

  const fetchExaminationResult = async () => {
    if (medicalRecordId === undefined) {
      message.error("Có lỗi xảy ra, vui lòng thử lại sau");
      return;
    }

    examAddForm.setFieldsValue({
      diagnosis: "",
      conclusion: "",
    });

    var examResult: ExamResultGetModel | undefined =
      await ExaminationService.getExamResultByMrId(medicalRecordId);
    if (examResult !== undefined) {
      console.log(examResult);

      if (examResult.conclusion !== "" && examResult.diagnosis !== "") {
        setExamResult(examResult);
        examAddForm.setFieldsValue({
          diagnosis: examResult.diagnosis,
          conclusion: examResult.conclusion,
        });
        setIsExamConclused(true);
      }
    }
  };

  const handleUpdateExaminationDetail = async () => {
    if (isExamConclused) {
      message.error("Khám đã kết luận, không thể cập nhật kết quả khám");
      return;
    }
    
    const examDetails = examAddForm.getFieldValue("examDetails");
    console.log(examDetails);
    var examDetail: ExaminationsResultModel = {
      medicalRecordId: medicalRecordId,
      examDetails: examDetails,
    };
    var response = await ExaminationService.putUpdateExaminationResult(
      medicalRecordId,
      examDetail
    );

    if (response !== undefined && response === 200) {
      message.success("Cập nhật thành công");
    } else {
      message.error("Có lỗi xảy ra, vui lòng thử lại sau");
    }
  };

  const handleUpdateExaminationResult = async (
    values: ExaminationsResultModel
  ) => {
    if (isExamConclused) {
      message.error("Khám đã kết luận, không thể cập nhật kết quả khám");
      return;
    }

    if (values.diagnosis === undefined || values.conclusion === undefined) {
      message.error("Vui lòng nhập đầy đủ thông tin");
      return;
    }

    var examResultAddModel: ExamResultAddModel = {
      medicalRecordId: medicalRecordId,
      diagnosis: values.diagnosis,
      conclusion: values.conclusion,
    };

    var response = await ExaminationService.postAddExaminationResult(
      examResultAddModel.medicalRecordId,
      examResultAddModel
    );
    if (response === undefined) {
      message.error("Có lỗi xảy ra, vui lòng thử lại sau", 2);
    } else {
      message
        .success("Cập nhật thành công", 2)
        .then(() => window.location.reload());
    }
  };

  useEffect(() => {
    fetchExamination();
    fetchExaminationResult();
  }, [isReload]);

  return examDetailList === undefined ? (
    <></>
  ) : (
    <div style={{ minWidth: "600px", width: "max-content" }}>
      <Form
        id="examAddForm"
        form={examAddForm}
        layout="vertical"
        name="basic exam"
        onFinish={handleUpdateExaminationResult}
      >
        <Row>
          <Col span={24} style={{display:"flex", justifyContent:"flex-end", alignItems:"center"}}>
            Ngày chỉnh sửa: {dayjs(examResult?.examDate).format("DD/MM/YYYY HH:mm:ss")}
          </Col>
        </Row><br/>
        {examDetailList.examDetails.map((examDetail, index) => (
          <div key={examDetail.serviceId}>
            <Row>
              <Col span={24}>
                <b> {examDetail.serviceName}</b>
              </Col>
            </Row>
            <Row key={examDetail.serviceId}>
              <Col span={12}>
                <Form.Item<ExaminationsResultModel>
                  label="Mô tả"
                  name={["examDetails", index, "description"]}
                >
                  <TextArea placeholder="Description" />
                </Form.Item>
              </Col>
            </Row>
            <Row>
              <Col span={24}>
                <Form.Item<ExaminationsResultModel>
                  label="Kết luận"
                  name={["examDetails", index, "diagnose"]}
                >
                  <TextArea placeholder="Diagnose" />
                </Form.Item>
              </Col>
            </Row>
            <Divider />
          </div>
        ))}
        <Row>
          <Col
            span={24}
            style={{ display: "flex", justifyContent: "flex-end" }}
          >
            <Button onClick={handleUpdateExaminationDetail} type="primary">
              Lưu kết quả
            </Button>
          </Col>
        </Row>
        <Divider />
        <Row style={{ display: "flex", alignItems: "center" }}>
          <Col span={24}>
            <Form.Item<ExaminationsResultModel>
              label="Chẩn đoán sơ bộ"
              name={"diagnosis"}
              rules={[
                { required: true, message: "Vui lòng nhập chẩn đoán sơ bộ" },
              ]}
            >
              <Input disabled={isExamConclused} placeholder="Diagnosis" />
            </Form.Item>
          </Col>
        </Row>
        <Row style={{ display: "flex", alignItems: "center" }}>
          <Col span={24}>
            <Form.Item<ExaminationsResultModel>
              label="Kết luận tổng"
              name={"conclusion"}
              rules={[
                { required: true, message: "Vui lòng nhập kết luận tổng" },
              ]}
            >
              <Input disabled={isExamConclused} placeholder="Conclusion" />
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </div>
  );
};

export default ExaminationForm;
