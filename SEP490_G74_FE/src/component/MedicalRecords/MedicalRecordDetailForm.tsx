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
import patientService from "../../Services/PatientSerivce";
import categoryService from "../../Services/CategoryService";
import subService from "../../Services/SubService";
import medicalRecordService from "../../Services/MedicalRecordService";
import generatePDF, { usePDF } from "react-to-pdf";

const MedicalRecordDetailForm = ({
  patientId,
  medicalRecordId,
  isReload,
}: PatientProps) => {
  const [mrDetailform] = Form.useForm();
  const today = dayjs();
  const { authenticated } = useContext(AuthContext);
  const [isDisable, setIsDisable] = useState<boolean>(false);
  const [isDoctorDisable, setIsDoctorDisable] = useState<boolean>(false);
  const [isCheckUp, setIsCheckUp] = useState<boolean>(false);
  const [prevMrId, setPrevMrId] = useState<number | undefined>(undefined);
  const [patient, SetPatient] = useState<PatientAddModel | undefined>(
    undefined
  );

  const [options, setOptions] = useState<SelectProps["options"]>([]);
  const [serviceTypeOptions, setServiceTypeOptions] = useState<
    SelectProps["options"]
  >([]);
  const [serviceOptions, setServiceOptions] = useState<SelectProps["options"]>(
    []
  );
  const [, setDoctorIds] = useState<number[]>([]);

  //const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  const [docs, setDocs] = useState<DoctorResponseModel[]>([]);
  //===================

  const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  const [doctors, setDoctors] = useState<DoctorResponseModel[]>([]);
  const [types, setTypes] = useState<ServiceTypeResponseModel[]>([]);
  const [services, setServices] = useState<ServiceResponseModel[]>([]);

  const onFinish = async (values: MedicalRecord) => {
    console.log(values)
    if (
      values.selectedServiceTypeIds === undefined ||
      values.selectedServiceIds === undefined || values.selectedDoctorIds === undefined || values.selectedCategoryIds.length === 0
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

  // const handleChangeCategory = async (values: number) => {
  // var docs: DoctorResponseModel[] | undefined =
  //   await subService.getDoctorsByCategoryId(values);
  // if (docs !== undefined) {
  //   setDoctors(docs);
  //   mrDetailform.resetFields(["selectedDoctorId"]);
  // } else {
  //   message.error("Get Doctors Failed", 2);
  // }

  // var types: ServiceTypeResponseModel[] | undefined =
  //   await subService.getServicesType(values);

  // if (types !== undefined) {
  //   setTypes(types);
  //   mrDetailform.resetFields(["selectedServiceTypeIds"]);
  //   mrDetailform.resetFields(["selectedServiceIds"]);
  // } else {
  //   message.error("Get Service Type Failed", 2);
  // }

  // };

  //=================new==============
  const handleChangeCategory = async (values: number[]) => {
    if (values.length === 0) {
      setOptions([]);
      setServiceTypeOptions([]);
      setServiceOptions([]);
      mrDetailform.resetFields([
        "selectedDoctorIds",
        "selectedServiceTypeIds",
        "selectedServiceIds",
      ]);
      return;
    }
    mrDetailform.resetFields([
      "selectedDoctorIds",
      "selectedServiceTypeIds",
      "selectedServiceIds",
    ]);
    // ----------- new get least assigned doctor
    var docs: DoctorResponseModel[] = await fetchDoctors(values);

    var newDocIds: number[] = [];
    var newOptions: SelectProps["options"] = [];
    console.log(docs);
    docs.forEach((element) => {
      newOptions!.push({
        value: element.userId,
        label: element.userName,
      });
      newDocIds.push(element.userId);
    });
    console.log(newDocIds);
    setOptions(newOptions);
    setDocs(docs);
    setDoctorIds(newDocIds);
    mrDetailform.setFieldsValue({
      selectedDoctorIds: newDocIds,
    });

    // get types
    var newTypeOptions: SelectProps["options"] = [];
    var categoriesBySelectedTypes: ServiceTypeResponseModel[] =
      await fetchServiceTypes(values);

    if (categoriesBySelectedTypes.length > 0) {
      categoriesBySelectedTypes.forEach((element) => {
        newTypeOptions?.push({
          value: element.serviceTypeId,
          label: element.serviceTypeName,
        });
      });
      setServiceTypeOptions(newTypeOptions);
      mrDetailform.resetFields(["selectedServiceTypeIds"]);
      mrDetailform.setFieldsValue({
        selectedServiceTypeIds: categoriesBySelectedTypes.map(
          (element) => element.serviceTypeId
        ),
      });
    }

    // set services
    var newServiceOptions: SelectProps["options"] = [];
    var servicesByServiceTypes: ServiceResponseModel[] = await fetchServices(
      categoriesBySelectedTypes.map((element) => element.serviceTypeId)
    );

    console.log(servicesByServiceTypes);
    if (servicesByServiceTypes.length > 0) {
      servicesByServiceTypes.forEach((element) => {
        newServiceOptions?.push({
          value: element.serviceId,
          label: element.serviceName,
        });
      });
      setServiceOptions(newServiceOptions);
      mrDetailform.resetFields(["selectedServiceIds"]);
      mrDetailform.setFieldsValue({
        selectedServiceIds: servicesByServiceTypes.map(
          (element) => element.serviceId
        ),
      });
    }
  };

  const handleChangeServiceType = async (values: number[]) => {
    mrDetailform.resetFields([
      //"selectedDoctorIds",
      //"selectedServiceTypeIds",
      "selectedServiceIds",
    ]);
    // var newSerOptions: SelectProps["options"] = [];
    // values.forEach(async (value) => {
    //   var services = await subService.getServices(value);
    //   console.log(services);
    //   if (services !== undefined) {
    //     services.forEach((element) => {
    //       newSerOptions!.push({
    //         value: element.serviceId,
    //         label: element.serviceName,
    //       });
    //     });
    //     setServiceOptions(newSerOptions);
    //     setServices(services);
    //   }
    // });
    // console.log(newSerOptions);
    // var selectedServices: any = mrDetailform.getFieldsValue([
    //   "selectedServiceIds",
    // ]);

    // if (selectedServices.selectedServiceIds !== undefined) {
    //   selectedServices.selectedServiceIds =
    //     selectedServices.selectedServiceIds.filter((element: any) =>
    //       newSerOptions!.some((o) => o.value === element)
    //     );

    //   mrDetailform.setFieldsValue({
    //     selectedServiceIds: selectedServices.selectedServiceIds,
    //   });
    // }

    // new
    if (values.length === 0) {
      setServiceOptions([]);
      mrDetailform.resetFields(["selectedServiceIds"]);
      return;
    }
    var newServiceOptions: SelectProps["options"] = [];
    var servicesByServiceTypes: ServiceResponseModel[] = await fetchServices(
      values
    );

    console.log(servicesByServiceTypes);
    if (servicesByServiceTypes.length > 0) {
      servicesByServiceTypes.forEach((element) => {
        newServiceOptions?.push({
          value: element.serviceId,
          label: element.serviceName,
        });
      });
      setServiceOptions(newServiceOptions);
      mrDetailform.resetFields(["selectedServiceIds"]);
      mrDetailform.setFieldsValue({
        selectedServiceIds: servicesByServiceTypes.map(
          (element) => element.serviceId
        ),
      });
    }
  };

  const checkRole = () => {
    if (authenticated?.role === Roles.Nurse) {
      setIsDisable(true);
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
    } else {
      console.log(response);
      SetPatient(response);
    }
  };

  const fetchCates = async () => {
    var response = await categoryService.getCategories();
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
    } else {
      console.log(response);
      setCates(response);
    }
  };

  async function fetchDoctors(
    values: number[]
  ): Promise<DoctorResponseModel[]> {
    const docs: DoctorResponseModel[] = [];

    try {
      await Promise.all(
        values.map(async (cateId) => {
          const leastAsiggnedDoc =
            await subService.getLeastAssignedDoctorByCategoryId(cateId);
          if (leastAsiggnedDoc) {
            docs.push(leastAsiggnedDoc);
          } else {
            throw new Error(`Failed to get doctor for category ${cateId}`);
          }
        })
      );
    } catch (error) {
      message.error("Get Doctors Failed", 2);
      // Log or handle the error more specifically if needed
      console.error(error);
    }
    return docs;
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
    } catch (error) {
      message.error("Get Categories by Service Failed", 2);
      // Log or handle the error more specifically if needed
      console.error(error);
    }
    console.log(types);
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
          }
        })
      );
    } catch (error) {
      message.error("Get Categories by Service Failed", 2);
      // Log or handle the error more specifically if needed
      console.error(error);
    }
    console.log(services);
    return services;
  }

  const fetchMedicalRecordDetail = async (id: number) => {
    mrDetailform.setFieldsValue({
      id: medicalRecordId,
    });

    var response: MedicalRecordDetailModel | undefined =
      await medicalRecordService.getMedicalRecordDetailById(id);
    if (response === undefined) {
      message.error("Get Medical Record Detail Failed", 2);
    } else {
      setPrevMrId(response.prevMedicalRecordId);
      if (response.isCheckUp === true) {
        setIsCheckUp(true);
        setIsDisable(true);
      }
      var docs: DoctorResponseModel[] = response.doctors.map((doc) => ({
        userId: doc.doctorId,
        userName: doc.doctorName,
      }));

      setDoctors(docs);

      mrDetailform.setFieldsValue({
        isPaid: response.isPaid,
        isCheckUp: response.isCheckUp,
      });

      mrDetailform.setFieldsValue({
        selectedCategoryIds: response.categories.map((cate) => cate.categoryId),
        selectedDoctorIds: response.doctors.map((doc) => doc.doctorId),
        prevMedicalRecordId: response.prevMedicalRecordId,
      });
      //===============================
      var newOptions: SelectProps["options"] = [];
      var newDocIds: number[] = [];
      docs.forEach((element) => {
        newOptions!.push({
          value: element.userId,
          label: element.userName,
        });
        newDocIds.push(element.userId);
      });
      console.log(newDocIds);
      setOptions(newOptions);
      setDocs(docs);
      setDoctorIds(newDocIds);
      mrDetailform.setFieldsValue({
        selectedDoctorIds: newDocIds,
      });

      //=========================Parse selected service types to setFieldsValue=========
      var serviceTypeIds = response?.serviceTypes.map(
        (element) => element.serviceTypeId
      );

      mrDetailform.setFieldsValue({
        selectedServiceTypeIds: serviceTypeIds,
      });
      setTypes(response.serviceTypes);

      //========= set full type options by selected categories ============
      var typesBySelectedCategories = await fetchServiceTypes(
        response.categories.map((cate) => cate.categoryId)
      );
      if (typesBySelectedCategories.length > 0) {
        var newTypeOptions: SelectProps["options"] = [];
        typesBySelectedCategories.forEach((element) => {
          newTypeOptions!.push({
            value: element.serviceTypeId,
            label: element.serviceTypeName,
          });
        });
        setServiceTypeOptions(newTypeOptions);
      }
      // get service
      var serviceIds: number[] = [];
      var newServiceOptions: SelectProps["options"] = [];

      var allServicesByServiceTypes = await fetchServices(
        typesBySelectedCategories.map((x) => x.serviceTypeId)
      );
      if (allServicesByServiceTypes.length > 0) {
        allServicesByServiceTypes.forEach((service) => {
          newServiceOptions!.push({
            value: service.serviceId,
            label: service.serviceName,
          });
        });
      }

      // response.serviceTypes.forEach((element) => {
      //   element.services.forEach((service) => {
      //     newServiceOptions!.push({
      //       value: service.serviceId,
      //       label: service.serviceName,
      //     });
      //     serviceIds.push(service.serviceId);
      //   });
      // })

      setServiceOptions(newServiceOptions);

      // response.serviceTypes.forEach((element) => {
      //   element.services.forEach((service) => {
      //     serviceIds.push(service.serviceId);
      //   });
      // });

      response.serviceTypes.forEach((type) => {
        type.services.forEach((ser) => {
          serviceIds.push(ser.serviceId);
        });
      });

      mrDetailform.setFieldsValue({
        selectedServiceIds: serviceIds,
      });

      var selectedServices = response.serviceTypes
        .map((element) => element.services)
        .flat();
      console.log(selectedServices);
      setServices(selectedServices);
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

  useEffect(() => {
    mrDetailform.setFieldsValue({
      patientId: patientId,
    });
    checkRole();
    fetchPatient();
    fetchCates();
    if (medicalRecordId !== undefined) {
      fetchMedicalRecordDetail(medicalRecordId);
    }
  }, [patientId, medicalRecordId, isReload]);

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
                  mode="multiple"
                  disabled
                  // options={doctors.map((doc) => ({
                  //   value: doc.userId,
                  //   label: doc.userName,
                  // }))}
                  options={options}
                />
              </Form.Item>
            </Col>
          </Row>
          <Form.Item<MedicalRecord>
            name="selectedServiceTypeIds"
            label="Chọn loại dịch vụ khám"
          >
            <Select
              disabled={isDisable}
              mode="multiple"
              onChange={handleChangeServiceType}
              allowClear
              // options={types.map((type) => ({
              //   value: type.serviceTypeId,
              //   label: type.serviceTypeName,
              // }))}
              options={serviceTypeOptions}
            />
          </Form.Item>
          <Form.Item<MedicalRecord>
            name="selectedServiceIds"
            label="Chọn dịch vụ khám"
          >
            <Select
              mode="multiple"
              disabled={isDisable}
              allowClear
              // options={services.map((service) => ({
              //   value: service.serviceId,
              //   label: service.serviceName,
              // }))}
              options={serviceOptions}
            />
          </Form.Item>
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
                // options={types.map((type) => ({
                //   value: type.serviceTypeId,
                //   label: type.serviceTypeName,
                // }))}
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
                // options={services.map((service) => ({
                //   value: service.serviceId,
                //   label: service.serviceName,
                // }))}
                options={serviceOptions}
              />
            </Form.Item>
          ) : (
            <></>
          )}
          {isCheckUp && (
            <Button onClick={handleReCheckUp} type="primary">
              Tạo hồ sơ tái khám
            </Button>
          )}
        </Form>
      </div>
    </div>
  ) : (
    <div>An Error Occurs When Getting Patient</div>
  );
};

export default MedicalRecordDetailForm;
