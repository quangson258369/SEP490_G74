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
  InputNumber,
} from "antd";
import {
  Category,
  Doctor,
  MedicalRecord,
  MedicalRecordAddModel,
  PatientProps,
  Service,
  ServiceType,
} from "../../Models/MedicalRecordModel";
import dayjs from "dayjs";
import TextArea from "antd/es/input/TextArea";
import { useContext, useEffect, useState } from "react";
import {
  CategoryResponseModel,
  DoctorResponseModel,
  ServiceResponseModel,
  ServiceTypeResponseModel,
} from "../../Models/SubEntityModel";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import medicalRecordService from "../../Services/MedicalRecordService";
import patientService from "../../Services/PatientSerivce";
import { PatientAddModel } from "../../Models/PatientModel";
import subService from "../../Services/SubService";
import categoryService from "../../Services/CategoryService";

const MedicalRecordAddForm = ({ patientId }: PatientProps) => {
  const [mrAddform] = Form.useForm();
  const today = dayjs();
  const [options, setOptions] = useState<SelectProps["options"]>([]);
  const [serviceTypeOptions, setServiceTypeOptions] = useState<
    SelectProps["options"]
  >([]);
  const [serviceOptions, setServiceOptions] = useState<SelectProps["options"]>(
    []
  );
  const { authenticated } = useContext(AuthContext);
  const [isDisable, setIsDisable] = useState<boolean>(false);
  const [patient, SetPatient] = useState<PatientAddModel | undefined>(
    undefined
  );

  const [, setDoctors] = useState<number[]>([]);

  const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  const [docs, setDocs] = useState<DoctorResponseModel[]>([]);

  const onFinish = async (values: MedicalRecord) => {
    var medAddForm: MedicalRecordAddModel = {
      categoryIds: values.selectedCategoryIds,
      doctorIds: values.selectedDoctorIds,
      examReason: "",
      patientId: values.patientId,
    };
    console.log(medAddForm);
    var response = await medicalRecordService.addMedicalRecord(medAddForm);
    if (response !== 200 && response !== 201) {
      message.error("Created MR Failed");
    } else {
      message.success("Created success: " + response).then(() => {
        window.location.reload();
      });
    }
  };

  const onFinishFailed = () => {
    message.error("Create MR Failed With Validation");
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

  const handleChangeCategory = async (values: number[]) => {
    if (values.length === 0) {
      setOptions([]);
      setServiceTypeOptions([]);
      setServiceOptions([]);
      mrAddform.resetFields([
        "selectedDoctorIds",
        "selectedServiceTypeIds",
        "selectedServiceIds",
      ]);
      return;
    }
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
    setDoctors(newDocIds);
    mrAddform.setFieldsValue({
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
      mrAddform.resetFields(["selectedServiceTypeIds"]);
      mrAddform.setFieldsValue({
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
      mrAddform.resetFields(["selectedServiceIds"]);
      mrAddform.setFieldsValue({
        selectedServiceIds: servicesByServiceTypes.map(
          (element) => element.serviceId
        ),
      });
    }
  };

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

  const handleChangeServiceType = async (values: number[]) => {
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
      mrAddform.resetFields(["selectedServiceIds"]);
      mrAddform.setFieldsValue({
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

  useEffect(() => {
    mrAddform.setFieldsValue({
      patientId: patientId,
    });
    checkRole();
    fetchPatient();
    fetchCates();
  }, [patientId]);

  return patient !== undefined ? (
    <Form
      id="medicalRecordAddForm"
      form={mrAddform}
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
        docs: docs,
      }}
      autoComplete="off"
    >
      <Row gutter={10}>
        <Col span={8}>
          <Form.Item<MedicalRecord> label="Mã hồ sơ" name="id">
            <Input disabled />
          </Form.Item>
        </Col>
        <Col span={16}>
          <Form.Item<MedicalRecord> label="Ngày chỉnh sửa" name="editDate">
            <DatePicker disabled format={"MM/DD/YYYY HH:mm:ss"} />
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
          <Form.Item<MedicalRecord>
            label="Số điện thoại"
            name="phone"
          >
            <Input disabled placeholder="0983872xxx" />
          </Form.Item>
        </Col>
      </Row>
      <Form.Item<MedicalRecord>
        label="Địa chỉ"
        name="address"
      >
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
            <InputNumber disabled min={1} max={5000} placeholder="110" />
          </Form.Item>
        </Col>
      </Row>
      <Form.Item<MedicalRecord>
        label="Lí do khám"
        name="description"
        rules={[{ required: true, message: "Hãy nhập lí do khám" }]}
      >
        <TextArea placeholder="Lí do khám" />
      </Form.Item>
      <Row gutter={10}>
        <Col span={12}>
          <Form.Item<MedicalRecord>
            name="selectedCategoryIds"
            label="Chọn khoa khám"
          >
            <Select
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
            <Select disabled mode="multiple" options={options} />
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
          options={serviceOptions}
        />
      </Form.Item>
    </Form>
  ) : (
    <div>Error Occurs When Getting Patient</div>
  );
};

export default MedicalRecordAddForm;
