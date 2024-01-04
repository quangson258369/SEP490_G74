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
} from "antd";
import {
  Doctor,
  MedicalRecord,
  PatientProps,
} from "../../Models/MedicalRecordModel";
import dayjs from "dayjs";
import TextArea from "antd/es/input/TextArea";
import { useContext, useEffect, useState } from "react";
import { BaseModel, CategoryResponseModel, DoctorResponseModel, ServiceTypeResponseModel } from "../../Models/SubEntityModel";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Enums/Enums";
import { PatientAddModel } from "../../Models/PatientModel";
import patientService from "../../Services/PatientSerivce";
import categoryService from "../../Services/CategoryService";
import subService from "../../Services/SubService";

const MedicalRecordDetailForm = ({ patientId }: PatientProps) => {
  const [mrDetailform] = Form.useForm();
  const today = dayjs();
  const [options, setOptions] = useState<SelectProps["options"]>([]);
  const [serviceOptions, setServiceOptions] = useState<SelectProps["options"]>(
    []
  );
  const { authenticated } = useContext(AuthContext);
  const [isDisable, setIsDisable] = useState<boolean>(false);
  const [patient, SetPatient] = useState<PatientAddModel | undefined>(
    undefined
  );
  const [serviceTypeOptions, setServiceTypeOptions] = useState<SelectProps["options"]>(
    []
  );

  const [cates, setCates] = useState<CategoryResponseModel[]>([]);

  const onFinish = (values: MedicalRecord) => {
    message.success(values.patientId, 2);
  };

  const onFinishFailed = () => {
    message.error("Create MR Failed");
  };

  const doc1: Doctor = {
    id: 1,
    categoryId: 1,
    name: "Bac si A",
    roleId: 2,
  };

  const doc2: Doctor = {
    id: 2,
    categoryId: 2,
    name: "Bac si B",
    roleId: 2,
  };

  const ser1: BaseModel = {
    id: 1,
    parentId: 1,
    name: "Tiêm thuốc",
  };

  const ser2: BaseModel = {
    id: 2,
    parentId: 2,
    name: "Trám răng",
  };

  const doctors1: Doctor[] = [doc1, doc2];
  const sers: BaseModel[] = [ser1, ser2];

  // const handleChangeCategory = (values: number) => {
  //   var docs = doctors1.filter((d) => d.categoryId === values);
  //   var newOptions: SelectProps["options"] = [];
  //   docs.forEach((element) => {
  //     newOptions!.push({
  //       value: element.id,
  //       label: element.name,
  //     });
  //   });
  //   setOptions(newOptions);
  //   mrDetailform.resetFields(["selectedDoctorId"]);
  // };

  const handleChangeCategory = async (values: number) => {
    // var docs = doctors1.filter((d) => d.categoryId === values);
    // var newOptions: SelectProps["options"] = [];
    // docs.forEach((element) => {
    //   newOptions!.push({
    //     value: element.id,
    //     label: element.name,
    //   });
    // });
    // setOptions(newOptions);
    // mrAddform.resetFields(["selectedDoctorId"]);
    var docs: DoctorResponseModel[] | undefined =
      await subService.getDoctorsByCategoryId(values);
    if (docs !== undefined) {
      var newOptions: SelectProps["options"] = [];
      docs.forEach((element) => {
        newOptions!.push({
          value: element.userId,
          label: element.userName,
        });
      });
      setOptions(newOptions);
      mrDetailform.resetFields(["selectedDoctorId"]);
    } else {
      message.error("Get Doctors Failed", 2);
    }

    var types: ServiceTypeResponseModel[] | undefined =
     await subService.getServicesType(values)

     if(types !== undefined){
        var newTypeOptions: SelectProps["options"] = [];
        types.forEach((element) => {
          newTypeOptions!.push({
            value: element.serviceTypeId,
            label: element.serviceTypeName,
          });
        });
        setServiceTypeOptions(newTypeOptions);
        mrDetailform.resetFields(["selectedServiceTypeIds"]);
     }else{
        message.error("Get Service Type Failed", 2);
     }
  };

  // const handleChangeServiceType = (values: number[]) => {
  //   var newOptions: SelectProps["options"] = [];
  //   values.forEach((value) => {
  //     var docs = sers.filter((d) => d.parentId === value);
  //     docs.forEach((element) => {
  //       newOptions!.push({
  //         value: element.id,
  //         label: element.name,
  //       });
  //     });
  //   });
  //   setServiceOptions(newOptions);
  //   var selectedServices: any = mrDetailform.getFieldsValue([
  //     "selectedServiceIds",
  //   ]);
  //   if (selectedServices.selectedServiceIds !== undefined) {
  //     selectedServices.selectedServiceIds =
  //       selectedServices.selectedServiceIds.filter((element: any) =>
  //         newOptions!.some((o) => o.value === element)
  //       );

  //     mrDetailform.setFieldsValue({
  //       selectedServiceIds: selectedServices.selectedServiceIds,
  //     });
  //   }
  // };

  const handleChangeServiceType = async (values: number[]) => {
    var newSerOptions: SelectProps["options"] = [];
    values.forEach(async (value) => {
      var services = await subService.getServices(value);
      console.log(services);
      if (services !== undefined) {
        services.forEach((element) => {
          newSerOptions!.push({
            value: element.serviceId,
            label: element.serviceName,
          });
        });
        setServiceOptions(newSerOptions);
      }
    });
    console.log(newSerOptions)
    var selectedServices: any = mrDetailform.getFieldsValue([
      "selectedServiceIds",
    ]);
    if (selectedServices.selectedServiceIds !== undefined) {
      selectedServices.selectedServiceIds =
        selectedServices.selectedServiceIds.filter((element: any) =>
          newSerOptions!.some((o) => o.value === element)
        );

        mrDetailform.setFieldsValue({
        selectedServiceIds: selectedServices.selectedServiceIds,
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
    mrDetailform.setFieldsValue({
      patientId: patientId,
    });
    checkRole();
    fetchPatient()
    fetchCates();
  }, [patientId]);

  return patient!==undefined?(
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
            rules={[
              { required: true, message: "Hãy nhập số điện thoại" },
              { pattern: /^\d{10,11}$/, message: "Số điện thoại không hợp lệ" },
            ]}
          >
            <Input disabled placeholder="0983872xxx" />
          </Form.Item>
        </Col>
      </Row>
      <Form.Item<MedicalRecord> label="Địa chỉ" name="address">
        <Input disabled placeholder="Địa chỉ" />
      </Form.Item>
      <Row gutter={10}>
        <Col span={6}>
          <Form.Item<MedicalRecord>
            label="Chiều cao (cm)"
            name="height"
            rules={[
              {
                required: true,
                message: "Hãy nhập chiều cao",
              },
              {
                pattern: new RegExp("^(20|1\\d{2}|2[0-4]\\d|250)$"),
                message: "Chiều cao phải từ 20 đến 250 cm",
              },
            ]}
          >
            <Input disabled placeholder="170" />
          </Form.Item>
        </Col>
        <Col span={6}>
          <Form.Item<MedicalRecord>
            label="Cân nặng (kg)"
            name="weight"
            rules={[
              {
                required: true,
                message: "Hãy nhập cân nặng",
              },
              {
                pattern: new RegExp("^(0*[1-9][0-9]?[0-9]?|500)$"),
                message: "Cân nặng phải từ 1 đến 500 kg",
              },
            ]}
          >
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
            name="selectedCategoryId"
            label="Chọn khoa khám"
          >
            <Select
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
            name="selectedDoctorId"
            label="Chọn bác sĩ khám"
          >
            <Select options={options} />
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
  ):<div>An Error Occurs When Getting Patient</div>;
};

export default MedicalRecordDetailForm;
