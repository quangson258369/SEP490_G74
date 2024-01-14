import {
  Button,
  Col,
  Divider,
  Form,
  Image,
  Input,
  Row,
  Upload,
  UploadFile,
  UploadProps,
  message,
} from "antd";
import { ExaminationProps } from "../../Models/MedicalRecordModel";
import ExaminationService from "../../Services/ExaminationService";
import {
  ExamResultAddModel,
  ExamResultGetModel,
  ExaminationsResultModel,
} from "../../Models/SubEntityModel";
import { useContext, useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import dayjs from "dayjs";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import { UploadOutlined } from "@ant-design/icons";
import { RcFile } from "antd/es/upload";
import axios from "axios";

const ExaminationForm = ({ medicalRecordId, isReload }: ExaminationProps) => {
  const { authenticated } = useContext(AuthContext);
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
      //setExamDetailList(examDetails);
      examAddForm.setFieldsValue({
        medicalRecordId: examDetails.medicalRecordId,
        examDetails: examDetails.examDetails,
      });
      return examDetails;
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
      return examResult;
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

  //create a function use Promise.all to wait for fetchExamination and fetchExaminationResult
  const [isLoaded, setIsLoaded] = useState<boolean>(false);

  const fetchExam = async () => {
    var response = await Promise.all([
      fetchExamination(),
      fetchExaminationResult(),
    ]);
    console.log(response);
    var examDetails = response[0];
    if (examDetails !== undefined) {
      var isAddImageDon = await Promise.all(
        examDetails.examDetails.map(async (detail) => {
          var image = await getImage(detail.medicalRecordId, detail.serviceId);
          if (image !== undefined) {
            detail.image = image;
          }
        })
      );
      if(isAddImageDon !== undefined){
        console.log(examDetails);
        setExamDetailList(examDetails);
        setIsLoaded(true);
      }
    }
  };

  useEffect(() => {
    const getExamData = async () => {
      await fetchExam();
    };
    getExamData();
  }, [isReload, isLoaded]);

  //======== upload file============
  const [fileList, setFileList] = useState<UploadFile[]>([]);
  const [uploading, setUploading] = useState(false);

  const uploadImage = async (file: File) => {
    try {
      const formData = new FormData();
      formData.append("image", file);

      const response = await axios.post(
        "https://localhost:7021/api/ExaminationResult/upload",
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      if (response.status === 200) {
        console.log("Image uploaded successfully:", response.data);
        return true;
      }
      return false;
    } catch (error) {
      console.error("Failed to upload image:", error);
    }
  };

  const getImage = async (medicalRecordId: number, serviceId: number) => {
    try {
      const response = await axios.get(
        `https://localhost:7021/api/ExaminationResult/image/${medicalRecordId}_${serviceId}`,
        {
          responseType: "blob", // Important for dealing with images
          headers: {
            "Content-Type": "image/jpeg",
          },
        }
      );
      if (response.status === 200) {
        const imageUrl = URL.createObjectURL(response.data);
        return imageUrl;
      } else {
        return undefined;
      }
    } catch (error) {
      console.error("Failed to fetch image:", error);
      return undefined;
    }
  };

  const handleUpload = async (medicalRecordId: number, serviceId: number) => {
    console.log(fileList);
    var file = fileList[0] as unknown as File;
    const newFile = new File([file], `${medicalRecordId}_${serviceId}`);
    file = newFile;

    var response = await uploadImage(file);
    console.log(response);
    if (response === true) {
      message.success("Upload thành công");
    } else {
      message.error("Upload thất bại");
    }
  };
  const [imageUrl, setImageUrl] = useState<string | ArrayBuffer | null>(null);
  const uploadProps: UploadProps = {
    multiple: false,
    onRemove: (file) => {
      const index = fileList.indexOf(file);
      const newFileList = fileList.slice();
      newFileList.splice(index, 1);
      setFileList(newFileList);
    },
    beforeUpload: (file) => {
      setFileList([file]);
      const reader = new FileReader();
      reader.onloadend = () => {
        setImageUrl(reader.result);
      };
      reader.readAsDataURL(file);
      return false;
    },
    fileList,
  };

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
          <Col
            span={24}
            style={{
              display: "flex",
              justifyContent: "flex-end",
              alignItems: "center",
            }}
          >
            Ngày chỉnh sửa:{" "}
            {dayjs(examResult?.examDate).format("DD/MM/YYYY HH:mm:ss")}
          </Col>
        </Row>
        <br />
        {examDetailList.examDetails.map((examDetail, index) => (
          <div key={examDetail.serviceId}>
            <Row>
              <Col span={24}>
                Dịch vụ: <b>{examDetail.serviceName}</b>
              </Col>
            </Row>
            <Row>
              <Col span={24}>
                <div style={{ marginBottom: "5px" }}>
                  <div>Hình ảnh</div>
                  {
                    <Image
                      src={examDetail.image}
                      style={{ maxWidth: "300px", maxHeight: "300px" }}
                    />
                  }
                </div>
                <Upload {...uploadProps}>
                  <Button icon={<UploadOutlined />}>Chọn hình ảnh</Button>
                </Upload>
                <Button
                  type="primary"
                  onClick={() =>
                    handleUpload(
                      examDetail.medicalRecordId,
                      examDetail.serviceId
                    )
                  }
                  disabled={fileList.length === 0}
                  loading={uploading}
                  style={{ marginTop: 16 }}
                >
                  {uploading ? "Đang tải" : "Tải ảnh lên"}
                </Button>
              </Col>
            </Row>
            <Row key={examDetail.serviceId}>
              <Col span={24}>
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
            <Divider dashed />
          </div>
        ))}
        <Row>
          <Col
            span={24}
            style={{ display: "flex", justifyContent: "flex-end" }}
          >
            {authenticated?.role !== Roles.Admin &&
            authenticated?.role !== Roles.Doctor ? (
              <></>
            ) : (
              <Button onClick={handleUpdateExaminationDetail} type="primary">
                Lưu kết quả
              </Button>
            )}
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
              <TextArea disabled={isExamConclused} placeholder="Diagnosis" />
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
              <TextArea disabled={isExamConclused} placeholder="Conclusion" />
            </Form.Item>
          </Col>
        </Row>
      </Form>
      <Row>
        <Col span={18} />
        <Col span={6}>
          {authenticated?.role !== Roles.Admin &&
          authenticated?.role !== Roles.Doctor ? (
            <></>
          ) : (
            <Button
              key="submitExam"
              type="primary"
              form="examAddForm"
              htmlType="submit"
            >
              Lưu kết luận tổng
            </Button>
          )}
        </Col>
      </Row>
    </div>
  );
};

export default ExaminationForm;
