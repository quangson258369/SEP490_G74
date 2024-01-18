import {
  Form,
  Input,
  Radio,
  DatePicker,
  message,
  Select,
  Row,
  Col,
  SelectProps,
  Button,
  FormInstance,
  Divider,
  CollapseProps,
  Collapse,
} from "antd";
import {
  Doctor,
  MedicalRecord,
  MedicalRecordAddModel,
  MedicalRecordDetailModel,
  MedicalRecordUpdateModel,
  PatientProps,
} from "../../Models/MedicalRecordModel";
import dayjs from "dayjs";
import TextArea from "antd/es/input/TextArea";
import { useContext, useEffect, useRef, useState } from "react";
import {
  BaseModel,
  CategoryResponseModel,
  DoctorResponseModel,
  ServiceResponseModel,
  ServiceTypeResponseModel,
} from "../../Models/SubEntityModel";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import { PatientAddModel } from "../../Models/PatientModel";
import patientService from "../../Services/PatientService";
import categoryService from "../../Services/CategoryService";
import subService from "../../Services/SubService";
import medicalRecordService from "../../Services/MedicalRecordService";
import generatePDF, { usePDF } from "react-to-pdf";
import ExaminationForm from "./ExaminationForm";
import { useNavigate } from "react-router-dom";

const MedicalRecordDetailForm = ({
  patientId,
  medicalRecordId,
  isReload,
}: PatientProps) => {
  const [mrDetailform] = Form.useForm();
  const today = dayjs();
  const navigate = useNavigate();

  const { authenticated } = useContext(AuthContext);
  const [isServiceTypeDisable, setIsServiceTypeDisable] =
    useState<boolean>(false);
  const [isDoctorDisable, setIsDoctorDisable] = useState<boolean>(false);
  const [isCheckUp, setIsCheckUp] = useState<boolean>(false);
  const [isPaid, setIsPaid] = useState<boolean>(false);
  const [prevMrId, setPrevMrId] = useState<number | undefined>(undefined);
  const [patient, setPatient] = useState<PatientAddModel | undefined>(
    undefined
  );

  const [doctorOptions, setOptions] = useState<SelectProps["options"]>([]);
  const [serviceTypeOptions, setServiceTypeOptions] = useState<
    SelectProps["options"]
  >([]);
  const [serviceOptions, setServiceOptions] = useState<SelectProps["options"]>(
    []
  );
  const [, setDoctorIds] = useState<number[]>([]);

  //const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  //const [docs, setDocs] = useState<DoctorResponseModel[]>([]);
  //===================
  const [mrDetail, setMrDetail] = useState<
    MedicalRecordDetailModel | undefined
  >(undefined);
  const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  const [doctors, setDoctors] = useState<DoctorResponseModel[]>([]);
  const [types, setTypes] = useState<ServiceTypeResponseModel[]>([]);
  const [services, setServices] = useState<ServiceResponseModel[]>([]);
  const [selectedServices, setSelectedServices] = useState<
    ServiceResponseModel[]
  >([]);

  const [isExamReload, setIsExamReload] = useState<boolean>(false);

  const onFinish = async (values: MedicalRecord) => {
    if (isCheckUp === true) {
      message.info("Hồ sơ đã khám, không thể chỉnh sửa", 2);
      return;
    }

    if (
      values.selectedServiceTypeIds === undefined ||
      values.selectedServiceIds === undefined ||
      values.selectedDoctorIds === undefined ||
      values.selectedCategoryIds.length === 0 ||
      values.selectedDoctorIds.length === 0 ||
      values.selectedServiceIds.length === 0
    ) {
      message.error("Chưa chọn dịch vụ khám", 2);
      return;
    }

    if (values.isCheckUp === true) {
      message.info("Hồ sơ đã khám, không thể chỉnh sửa", 2);
      return;
    }

    var medUpdate: MedicalRecordUpdateModel = {
      categoryIds: [...values.selectedCategoryIds],
      doctorIds: [...values.selectedDoctorIds],
      serviceIds: [...values.selectedServiceIds],
    };

    if (medicalRecordId === undefined) {
      message.error("Update MR Failed Because Of Invalid Medical Record ID", 2);
      return;
    }

    var response = await medicalRecordService.updateMedicalRecord(
      medicalRecordId,
      medUpdate
    );

    if (response === 200) {
      message
        .success("Update MR Success", 2)
        .then(() => window.location.reload());
    } else {
      message.error("Update MR Failed", 2);
    }
  };

  const onFinishFailed = () => {
    message.error("Create MR Failed");
  };

  //=================new==============
  const handleChangeCategory = async (values: number[]) => {
    if (values.length === 0) {
      setOptions([]);
      setServiceTypeOptions([]);
      setServiceOptions([]);

      setDoctors([]);
      setTypes([]);
      setServices([]);

      mrDetailform.resetFields([
        "selectedDoctorIds",
        "selectedServiceTypeIds",
        "selectedServiceIds",
      ]);
      return;
    }

    var currentSelectedDoctorIds: number[] =
      mrDetailform.getFieldValue("selectedDoctorIds");
    if (currentSelectedDoctorIds === undefined) {
      currentSelectedDoctorIds = [];
    }

    mrDetailform.resetFields([
      "selectedDoctorIds",
      "selectedServiceTypeIds",
      "selectedServiceIds",
    ]);
    // ----------- new get least assigned doctor
    var newDoctorsSelectedByCategories: DoctorResponseModel[] =
      await fetchDoctors(values);
    if (newDoctorsSelectedByCategories.length > 0) {
      setDoctors(newDoctorsSelectedByCategories);
      // keep selected doctors if still available
      var oldAvailableDocIds: number[] = [];
      currentSelectedDoctorIds.forEach((oldDocId) => {
        if (
          newDoctorsSelectedByCategories.find(
            (newDoc) => newDoc.userId === oldDocId
          )
        ) {
          oldAvailableDocIds.push(oldDocId);
        }
      });
      mrDetailform.setFieldsValue({
        selectedDoctorIds: oldAvailableDocIds,
      });
    }

    // get types
    //var newTypeOptions: SelectProps["options"] = [];
    var typesBySelectedCategories: ServiceTypeResponseModel[] =
      await fetchServiceTypes(values);

    if (typesBySelectedCategories.length > 0) {
      // typesBySelectedCategories.forEach((element) => {
      //   newTypeOptions?.push({
      //     value: element.serviceTypeId,
      //     label: element.serviceTypeName,
      //   });
      // });
      // setServiceTypeOptions(newTypeOptions);
      mrDetailform.resetFields(["selectedServiceTypeIds"]);
      // mrDetailform.setFieldsValue({
      //   selectedServiceTypeIds: typesBySelectedCategories.map(
      //     (element) => element.serviceTypeId
      //   ),
      // });
      setTypes(typesBySelectedCategories);
    }

    // set services
    //var newServiceOptions: SelectProps["options"] = [];
    var servicesByServiceTypes: ServiceResponseModel[] = await fetchServices(
      typesBySelectedCategories.map((element) => element.serviceTypeId)
    );

    if (servicesByServiceTypes.length > 0) {
      // servicesByServiceTypes.forEach((element) => {
      //   newServiceOptions?.push({
      //     value: element.serviceId,
      //     label: element.serviceName,
      //   });
      // });
      // setServiceOptions(newServiceOptions);
      mrDetailform.resetFields(["selectedServiceIds"]);
      // mrDetailform.setFieldsValue({
      //   selectedServiceIds: servicesByServiceTypes.map(
      //     (element) => element.serviceId
      //   ),
      // });
      setServices(servicesByServiceTypes);
    }
  };

  const handleChangeServiceType = async (values: number[]) => {
    // new
    if (values.length === 0) {
      setServiceOptions([]);

      setServices([]);

      mrDetailform.setFieldsValue({
        selectedServiceIds: [],
      });
      return;
    }
    var currentSelectedServiceIds: number[] =
      mrDetailform.getFieldValue("selectedServiceIds");
    if (currentSelectedServiceIds === undefined) {
      currentSelectedServiceIds = [];
    }
    //var newServiceOptions: SelectProps["options"] = [];
    var newSelectedServicesByServiceTypes: ServiceResponseModel[] =
      await fetchServices(values);

    if (newSelectedServicesByServiceTypes.length > 0) {
      setServices(newSelectedServicesByServiceTypes);
      var oldSelectedServiceIds: number[] = [];
      currentSelectedServiceIds.forEach((oldServiceId) => {
        if (
          newSelectedServicesByServiceTypes.find(
            (newService) => newService.serviceId === oldServiceId
          )
        ) {
          oldSelectedServiceIds.push(oldServiceId);
        }
      });
      mrDetailform.resetFields(["selectedServiceIds"]);
      mrDetailform.setFieldsValue({
        selectedServiceIds: oldSelectedServiceIds,
      });
      setSelectedServices(
        newSelectedServicesByServiceTypes.filter((service) =>
          oldSelectedServiceIds.includes(service.serviceId)
        )
      );
    }
  };

  const handleChangeService = (values: number[]) => {
    if (values.length === 0) {
      setSelectedServices([]);
      return;
    }
    var newSelectedServices: ServiceResponseModel[] = services.filter(
      (service) => values.includes(service.serviceId)
    );
    setSelectedServices(newSelectedServices);
  };

  const checkRole = () => {
    if (authenticated?.role === Roles.Nurse) {
      setIsServiceTypeDisable(true);
    }
    if (authenticated?.role === Roles.Doctor) {
      setIsDoctorDisable(true);
    }
  };

  const fetchPatient = async () => {
    //PatientAddModel
    var response: PatientAddModel | undefined =
      await patientService.getPatientById(patientId);
    if (response === undefined) {
      message.error("Get Patient Failed", 2);
      return undefined;
    } else {
      console.log(response);
      //setPatient(response);
      return response;
    }
  };

  const fetchCates = async () => {
    var response = await categoryService.getCategories();
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
      return [];
    } else {
      console.log(response);
      //setCates(response);
      return response;
    }
  };

  async function fetchDoctors(
    values: number[]
  ): Promise<DoctorResponseModel[]> {
    var docsResponse: DoctorResponseModel[] = [];

    try {
      await Promise.all(
        values.map(async (cateId) => {
          const selectedDocsByCate = await subService.getDoctorsByCategoryId(
            cateId
          );
          if (selectedDocsByCate) {
            docsResponse = [...docsResponse, ...selectedDocsByCate];
          } else {
            throw new Error(`Failed to get doctor for category ${cateId}`);
          }
        })
      );
    } catch (error: any) {
      //message.error(error.message, 2);
      // Log or handle the error more specifically if needed
      console.error(error.message);
      return [];
    }
    return docsResponse;
  }

  async function fetchServiceTypes(
    values: number[]
  ): Promise<ServiceTypeResponseModel[]> {
    var types: ServiceTypeResponseModel[] = [];

    try {
      await Promise.all(
        values.map(async (cateId) => {
          var cateByType = await subService.getServicesType(cateId);
          if (cateByType !== undefined) {
            types = [...types, ...cateByType];
          }
        })
      );
    } catch (error: any) {
      //message.error("Get Categories by Service Failed", 2);
      // Log or handle the error more specifically if needed
      console.error(error.message);
      return [];
    }
    return types;
  }

  async function fetchServices(
    values: number[]
  ): Promise<ServiceResponseModel[]> {
    var services: ServiceResponseModel[] = [];

    try {
      await Promise.all(
        values.map(async (typeId) => {
          var serviceByType = await subService.getServices(typeId);
          if (serviceByType !== undefined) {
            services = [...services, ...serviceByType];
            //setServices(services);
          }
        })
      );
    } catch (error: any) {
      //message.error("Get Categories by Service Failed", 2);
      // Log or handle the error more specifically if needed
      console.error(error.message);
      return [];
    }
    return services;
  }

  const fetchMedicalRecordDetail = async (id: number) => {
    mrDetailform.setFieldsValue({
      id: medicalRecordId,
    });

    var mrDetailResponse: MedicalRecordDetailModel | undefined =
      await medicalRecordService.getMedicalRecordDetailById(id);
    if (mrDetailResponse === undefined) {
      message.error("Get Medical Record Detail Failed", 2);
    } else {
      setMrDetail(mrDetailResponse);
      setPrevMrId(mrDetailResponse.prevMedicalRecordId);

      if (mrDetailResponse.isCheckUp === true) {
        setIsCheckUp(true);
        setIsServiceTypeDisable(true);
      }
      if (mrDetailResponse.isPaid === true) {
        setIsPaid(true);
      }
      // set cates
      var currentSelectedCategories: CategoryResponseModel[] =
        mrDetailResponse.categories.map((cate) => ({
          categoryId: cate.categoryId,
          categoryName: cate.categoryName,
          isDeleted: cate.isDeleted,
        }));

      setCates(currentSelectedCategories);

      // set doctors
      var currentSelectedDoctors: DoctorResponseModel[] =
        mrDetailResponse.doctors.map((doc) => ({
          userId: doc.doctorId,
          userName: doc.doctorName,
          isDeleted: doc.isDeleted,
          categoryId: doc.categoryId,
        }));

      setDoctors(currentSelectedDoctors);

      // set types
      setTypes(mrDetailResponse.serviceTypes);

      // set services
      var serviceIds: number[] = [];
      mrDetailResponse.serviceTypes.forEach((type) => {
        serviceIds = [...serviceIds, ...type.services.map((x) => x.serviceId)];
      });

      var selectedServices = mrDetailResponse.serviceTypes
        .map((element) => element.services)
        .flat();

      setServices(selectedServices);
      setSelectedServices(selectedServices);

      // set default static values
      mrDetailform.setFieldsValue({
        isPaid: mrDetailResponse.isPaid,
        isCheckUp: mrDetailResponse.isCheckUp,
        prevMedicalRecordId: mrDetailResponse.prevMedicalRecordId,
        selectedCategoryIds: mrDetailResponse.categories.map(
          (cate) => cate.categoryId
        ),
        selectedDoctorIds: mrDetailResponse.doctors.map((doc) => doc.doctorId),
        selectedServiceTypeIds: mrDetailResponse.serviceTypes.map(
          (element) => element.serviceTypeId
        ),
        selectedServiceIds: serviceIds,
      });

      return mrDetailResponse;
    }
  };

  const handleReCheckUp = async () => {
    var mrDetail = mrDetailform.getFieldsValue();
    console.log(mrDetail);
    var reCheckUpMrModel: MedicalRecordAddModel = {
      categoryIds: mrDetail.selectedCategoryIds,
      doctorIds: mrDetail.selectedDoctorIds,
      patientId: patientId,
      examReason: "Tái khám",
      previousMedicalRecordId: medicalRecordId,
    };

    var response = await medicalRecordService.addMedicalRecord(
      reCheckUpMrModel
    );
    if (response === 200 || response === 201) {
      var createdMrId = await medicalRecordService.getReCheckUpByPrevMrId(
        medicalRecordId!
      );

      if (createdMrId !== undefined) {
        var medUpdate: MedicalRecordUpdateModel = {
          categoryIds: mrDetail.selectedCategoryIds,
          doctorIds: mrDetail.selectedDoctorIds,
          serviceIds: mrDetail.selectedReCheckUpServiceIds,
        };

        if (medicalRecordId === undefined) {
          message.error(
            "Update MR Failed Because Of Invalid Medical Record ID",
            2
          );
          return;
        }

        var response2 = await medicalRecordService.updateMedicalRecord(
          createdMrId,
          medUpdate
        );

        if (response2 === 200) {
          message
            .success("Update ReCheckUp MR Success", 2)
            .then(() => window.location.reload());
        } else {
          message.error("Update ReCheckUp MR Failed", 2);
        }
      }
    }
  };

  //=========== Collapse =========

  const itemsExamination: CollapseProps["items"] = [
    {
      key: `exam_${medicalRecordId}`,
      label: "Kết luận",
      children: (
        <ExaminationForm
          medicalRecordId={medicalRecordId ?? 0}
          isReload={isExamReload}
          patientId={patientId}
        />
      ),
    },
  ];

  //========= Collapse Re checkup =========
  const itemsReCheckUp: CollapseProps["items"] = [
    {
      key: `check_up_${medicalRecordId}`,
      label: "Tái khám",
      children: (
        <div>
          {isCheckUp === true ? (
            <Form.Item<MedicalRecord>
              name="selectedReCheckUpServiceTypeIds"
              label="Chọn loại dịch vụ tái khám"
            >
              <Select
                disabled={authenticated?.role !== Roles.Nurse ? false : true}
                mode="multiple"
                onChange={handleChangeServiceType}
                allowClear
                options={serviceTypeOptions}
              />
            </Form.Item>
          ) : (
            <></>
          )}
          {isCheckUp === true ? (
            <Form.Item<MedicalRecord>
              name="selectedReCheckUpServiceIds"
              label="Chọn dịch vụ tái khám"
            >
              <Select
                mode="multiple"
                disabled={authenticated?.role !== Roles.Nurse ? false : true}
                allowClear
                options={serviceOptions}
              />
            </Form.Item>
          ) : (
            <></>
          )}
          {isCheckUp === true ? (
            authenticated?.role !== Roles.Admin &&
            authenticated?.role !== Roles.Doctor ? (
              <></>
            ) : (
              <Button onClick={handleReCheckUp} type="primary">
                Tạo hồ sơ tái khám
              </Button>
            )
          ) : (
            <></>
          )}
        </div>
      ),
    },
  ];

  const fetchData = async () => {
    if (medicalRecordId === undefined) {
      message.error("Mã hồ sơ không hợp lệ", 2).then(() => navigate("/"));
    } else {
      //Fetch patient info, medical record detail and categories
      var responses = await Promise.all([
        fetchPatient(),
        fetchMedicalRecordDetail(medicalRecordId),
      ]);
      if (responses !== undefined && responses.length === 2) {
        setPatient(responses[0]);
        setMrDetail(responses[1]);
        //get cate, doc, type, service options
        var cateResponses: CategoryResponseModel[] = await fetchCates();
        if (cateResponses !== undefined) {
          setCates(cateResponses.filter((cate) => cate.isDeleted === false));
          var [docs, types] = await Promise.all([
            fetchDoctors(cateResponses.map((cate) => cate.categoryId)),
            fetchServiceTypes(cateResponses.map((cate) => cate.categoryId)),
          ]);
          if (docs !== undefined && types !== undefined) {
            setDoctors(docs.filter((doc) => doc.isDeleted === false));
            setTypes(types.filter((type) => type.isDeleted === false));
            var services = await fetchServices(
              types.map((type) => type.serviceTypeId)
            );
            if (services !== undefined) {
              setServices(
                services.filter((service) => service.isDeleted === false)
              );
            }
          }
        }
      }
    }
  };

  useEffect(() => {
    mrDetailform.setFieldsValue({
      patientId: patientId,
    });
    checkRole();
    fetchData();
  }, [patientId, medicalRecordId, isReload]);

  const handleChangeCollapse = (key: string | string[]) => {
    setIsExamReload(!isExamReload);
    console.log(key);
  };

  const handleChangeCheckUpCollapse = (key: string | string[]) => {
    setIsExamReload(!isExamReload);
    console.log(key);
  };

  const getTargetElement = () => document.getElementById("printTarget");

  return patient !== undefined ? (
    <div>
      <Button type="link" onClick={() => generatePDF(getTargetElement)}>
        Tải xuống dưới dạng PDF
      </Button>
      <div id="printTarget" style={{ padding: "20px" }}>
        <Form
          id="medicalRecordDetailForm"
          form={mrDetailform}
          name="basic"
          layout="vertical"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          initialValues={{
            dob: dayjs(patient.dob),
            editDate: today,
            gender: patient.gender,
            name: patient.name,
            phone: patient.phone,
            address: patient.address,
            height: patient.height,
            weight: patient.weight,
            blood: patient.bloodGroup,
            bloodPressure: patient.bloodPressure,
            description: patient.allergieshistory,
            prevMedicalRecordId: prevMrId,
          }}
          autoComplete="off"
        >
          <Row gutter={10}>
            <Col span={8}>
              <Form.Item label="Mã hồ sơ" name="id">
                <Input disabled />
              </Form.Item>
            </Col>
            <Col span={8}>
              <Form.Item<MedicalRecord> label="Ngày chỉnh sửa" name="editDate">
                <DatePicker disabled format={"MM/DD/YYYY HH:mm:ss"} />
              </Form.Item>
            </Col>
            {prevMrId !== undefined && prevMrId !== null ? (
              <Col span={8}>
                <Form.Item
                  label="Mã hồ sơ khám trước"
                  name="prevMedicalRecordId"
                >
                  <Input disabled />
                </Form.Item>
              </Col>
            ) : (
              <Col span={8}></Col>
            )}
          </Row>
          <Row gutter={10}>
            <Col span={8}>
              <Form.Item<MedicalRecord> label="Thanh toán" name="isPaid">
                <Radio.Group disabled>
                  <Radio value={true}>Rồi</Radio>
                  <Radio value={false}>Chưa</Radio>
                </Radio.Group>
              </Form.Item>
            </Col>
            <Col span={16}>
              <Form.Item<MedicalRecord> label="Khám" name="isCheckUp">
                <Radio.Group disabled>
                  <Radio value={true}>Rồi</Radio>
                  <Radio value={false}>Chưa</Radio>
                </Radio.Group>
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={10}>
            <Col span={8}>
              <Form.Item<MedicalRecord> label="Mã bệnh nhân" name="patientId">
                <Input disabled />
              </Form.Item>
            </Col>
            <Col span={16}>
              <Form.Item<MedicalRecord> label="Họ và tên" name="name">
                <Input placeholder="Họ và tên" disabled />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={10}>
            <Col span={12}>
              <Form.Item<MedicalRecord> label="Ngày sinh" name="dob">
                <DatePicker disabled format={"MM/DD/YYYY HH:mm:ss"} />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item<MedicalRecord> label="Giới tính" name="gender">
                <Radio.Group disabled>
                  <Radio value={true}>Male</Radio>
                  <Radio value={false}>Female</Radio>
                </Radio.Group>
              </Form.Item>
            </Col>
          </Row>
          <Row>
            <Col span={12}>
              <Form.Item<MedicalRecord> label="Số điện thoại" name="phone">
                <Input disabled placeholder="0983872xxx" />
              </Form.Item>
            </Col>
          </Row>
          <Form.Item<MedicalRecord> label="Địa chỉ" name="address">
            <Input disabled placeholder="Địa chỉ" />
          </Form.Item>
          <Row gutter={10}>
            <Col span={6}>
              <Form.Item<MedicalRecord> label="Chiều cao (cm)" name="height">
                <Input disabled placeholder="170" />
              </Form.Item>
            </Col>
            <Col span={6}>
              <Form.Item<MedicalRecord> label="Cân nặng (kg)" name="weight">
                <Input disabled placeholder="55" />
              </Form.Item>
            </Col>
            <Col span={6}>
              <Form.Item<MedicalRecord> label="Nhóm máu" name="blood">
                <Input disabled placeholder="A" />
              </Form.Item>
            </Col>
            <Col span={6}>
              <Form.Item<MedicalRecord>
                label="Huyết áp (mmHg)"
                name="bloodPressure"
              >
                <Input disabled placeholder="110" />
              </Form.Item>
            </Col>
          </Row>
          <Form.Item<MedicalRecord> label="Lí do khám" name="description">
            <TextArea placeholder="Lí do khám" />
          </Form.Item>
          {/*=========Select Category and Doctor=========== */}
          <div
            style={{
              display: isPaid === true || isCheckUp === true ? "none" : "block",
            }}
          >
            <Row gutter={10}>
              <Col span={12}>
                <Form.Item<MedicalRecord>
                  name="selectedCategoryIds"
                  label="Chọn khoa khám"
                >
                  <Select
                    disabled={isDoctorDisable}
                    mode="multiple"
                    onChange={handleChangeCategory}
                    options={cates.map((category) => ({
                      value: category.categoryId,
                      label: category.categoryName,
                    }))}
                  />
                </Form.Item>
              </Col>
              <Col span={12}>
                <Form.Item<MedicalRecord>
                  name="selectedDoctorIds"
                  label="Chọn bác sĩ khám"
                >
                  <Select
                    disabled={isDoctorDisable}
                    mode="multiple"
                    options={doctors.map((doc) => ({
                      value: doc.userId,
                      label: doc.userName,
                    }))}
                  />
                </Form.Item>
              </Col>
            </Row>
          </div>
          {/*=========Select Service Type=========== */}
          {authenticated?.role === Roles.Admin ||
          authenticated?.role === Roles.Doctor ? (
            <>
              {isPaid === true ||
                (isCheckUp === false && (
                  <Form.Item<MedicalRecord>
                    name="selectedServiceTypeIds"
                    label="Chọn loại dịch vụ khám"
                  >
                    <Select
                      disabled={isServiceTypeDisable}
                      mode="multiple"
                      onChange={handleChangeServiceType}
                      allowClear
                      options={types.map((type) => ({
                        value: type.serviceTypeId,
                        label: type.serviceTypeName,
                      }))}
                    />
                  </Form.Item>
                ))}
              {isPaid === true ||
                (isCheckUp === false && (
                  <Form.Item<MedicalRecord>
                    name="selectedServiceIds"
                    label="Chọn dịch vụ khám"
                  >
                    <Select
                      mode="multiple"
                      onChange={handleChangeService}
                      disabled={isServiceTypeDisable}
                      allowClear
                      options={services.map((service) => ({
                        value: service.serviceId,
                        label: service.serviceName,
                      }))}
                    />
                  </Form.Item>
                ))}
            </>
          ) : (
            <></>
          )}
          {/*=========Tai kham=========== */}
          {isCheckUp === true && (
            <Collapse
              items={itemsReCheckUp}
              onChange={handleChangeCheckUpCollapse}
            />
          )}
        </Form>
      </div>
      <Divider />
      <div>
        <b>Dịch vụ đã chọn:</b>{" "}
      </div>
      <br />
      <div id="listServicePrice">
        {selectedServices.map((service) => (
          <div key={service.serviceId}>
            <Row>
              <Col span={12}>{service.serviceName}</Col>
              <Col span={12}>{service.price.toLocaleString()} VND</Col>
            </Row>
          </div>
        ))}
      </div>
      <Divider dashed/>
      <Row>
        <Col span={12}>
          <b>Tạm tính: </b>
        </Col>
        <Col span={12}>
          {(selectedServices.reduce((total, s) => total + s.price, 0)).toLocaleString()} VND
        </Col>
      </Row>
      {/*=========Examination=========== */}
      {isPaid === true || isCheckUp === true ? (
        <Collapse
          destroyInactivePanel
          items={itemsExamination}
          onChange={handleChangeCollapse}
        />
      ) : (
        <></>
      )}
    </div>
  ) : (
    <div>Loading...</div>
  );
};

export default MedicalRecordDetailForm;
