import {
  Form,
  Input,
  message,
  Select,
  Row,
  Col,
  SelectProps,
  Button,
  InputNumber,
  Divider,
} from "antd";
import { ExaminationProps } from "../../Models/MedicalRecordModel";
import { useContext, useEffect, useState } from "react";
import {
  SelectedSuppliesResponseModel,
  SuppliesPresAddModel,
  SupplyIdPreAddModel,
  SupplyPresSelectFormModel,
  SupplyResponseModel,
  SupplyTypeResponseModel,
} from "../../Models/SubEntityModel";
import subService from "../../Services/SubService";
import generatePDF from "react-to-pdf";
import type { CollapseProps } from "antd";
import { Collapse } from "antd";
import Roles from "../../Enums/Enums";
import { AuthContext } from "../../ContextProvider/AuthContext";

const SupplyPrescriptionDetailForm = ({
  medicalRecordId,
  isReload,
}: ExaminationProps) => {
  const [supplyPresForm] = Form.useForm();
  const { authenticated } = useContext(AuthContext);

  const [supplyTypeOptions, setSupplyTypeOptions] = useState<
    SelectProps["options"]
  >([]);

  const [supplyTypes, setSupplyTypes] = useState<
    SupplyTypeResponseModel[] | undefined
  >(undefined);

  const [availableSupplies, setAvailableSupplies] = useState<
    SupplyResponseModel[]
  >([]);

  const [selectedSupplies, setSelectedSupplies] = useState<
    SelectedSuppliesResponseModel[]
  >([]);

  const onFinish = async (values: any) => {
    console.log(availableSupplies);
    console.log(values);

    var selectedSupplies: SupplyIdPreAddModel[] = [];

    availableSupplies.forEach((element) => {
      var nameSelectedSupply = `selected_supply_${element.sId}`;
      var quantity = values[nameSelectedSupply];
      if (quantity !== undefined) {
        var newSupplyId: SupplyIdPreAddModel = {
          supplyId: element.sId,
          quantity: quantity,
        };
        selectedSupplies.push(newSupplyId);
      }
    });

    if (selectedSupplies.length === 0) {
      message.error("Please Select At Least One Supply", 2);
      return;
    }

    var supplyPresUpdate: SuppliesPresAddModel = {
      medicalRecordId: medicalRecordId,
      supplyIds: selectedSupplies.filter((element) => element.quantity > 0),
    };

    if (medicalRecordId === undefined) {
      message.error("Update MR Failed Because Of Invalid Medical Record ID", 2);
      return;
    }

    var response = await subService.addSuppliesPrescriptionsByMrId(
      medicalRecordId,
      supplyPresUpdate
    );

    if (response === 200) {
      message.success("Update Success", 2).then(() => window.location.reload());
    } else {
      message.error("Update Failed", 2);
    }
  };

  const onFinishFailed = () => {
    message.error("Create Supply Prescription Failed");
  };

  const handleChangeSupplyType = async (values: number[]) => {
    supplyPresForm.resetFields(["selectedSupplies"]);

    // new
    if (values.length === 0) {
      //setSupplyOptions([]);
      supplyPresForm.resetFields(["selectedSupplies"]);
      return;
    }
    var newSupplyOptions: SelectProps["options"] = [];
    var suppliesByType: SupplyResponseModel[] = await fetchSupplies(values);

    console.log(suppliesByType);
    if (suppliesByType.length > 0) {
      suppliesByType.forEach((element) => {
        newSupplyOptions?.push({
          value: element.sId,
          label: element.sName,
        });
      });
      //setSupplyOptions(newSupplyOptions);
      supplyPresForm.resetFields(["selectedSupplies"]);
      setAvailableSupplies(suppliesByType);
      //   supplyPresForm.setFieldsValue({
      //     : suppliesByType.map((element) => element.sId),
      //   });
    }
  };

  async function fetchSupplies(
    values: number[]
  ): Promise<SupplyResponseModel[]> {
    var supplies: SupplyResponseModel[] = [];

    try {
      await Promise.all(
        values.map(async (typeId) => {
          var suppliesByType = await subService.getSuppliesBySupplyTypeId(
            typeId
          );
          if (suppliesByType !== undefined) {
            supplies = [...supplies, ...suppliesByType];
          }
        })
      );
    } catch (error) {
      message.error("Get Categories by Service Failed", 2);
      // Log or handle the error more specifically if needed
      console.error(error);
    }
    console.log(supplies);
    return supplies;
  }

  const fetchSupplyType = async () => {
    var response = await subService.getAllSupplyTypes();
    console.log(response);
    if (response === undefined) {
      message.error("Get Supply Type Failed", 2);
      return;
    } else {
      var newOptions: SelectProps["options"] = [];
      response.forEach((element) => {
        newOptions!.push({
          value: element.suppliesTypeId,
          label: element.suppliesTypeName,
        });
      });
      setSupplyTypeOptions(newOptions);
      setSupplyTypes(response);
      supplyPresForm.setFieldsValue({
        medicalRecordId: medicalRecordId,
      });
    }
  };

  const fetchSelectedSupplies = async (medicalRecordId: number) => {
    var response = await subService.getSelectedSuppliesByMrId(medicalRecordId);
    console.log(response);
    if (response === undefined) {
      message.error("Get Selected Supplies Failed", 2);
      return;
    } else {
      setSelectedSupplies(response);
    }
  };

  const items: CollapseProps["items"] = [
    {
      key: `supplies_${medicalRecordId}`,
      label: <b>Thêm thuốc</b>,
      children: (
        <div id="add-supplies-container">
          <Form.Item<SupplyPresSelectFormModel>
            name="selectedSupplyTypes"
            label={<b>Chọn loại thuốc</b>}
          >
            <Select
              mode="multiple"
              onChange={handleChangeSupplyType}
              allowClear
              //set supply type options
              options={supplyTypeOptions}
            />
          </Form.Item>
          <Row gutter={10}>
            <Col span={10}>
              <b>Tên thuốc</b>
            </Col>
            <Col span={6}>
              <b>Giá tiền (VND)</b>
            </Col>
            {/* <Col span={4}>
              <b>Số lượng còn lại</b>
            </Col> */}
            <Col span={4}>
              <b>Số lượng đã chọn</b>
            </Col>
          </Row>
          <Divider dashed />
          {availableSupplies.map((element) => {
            return (
              <div key={element.sId}>
                <Row gutter={10}>
                  <Col span={10}>{element.sName}</Col>
                  <Col span={6}>{element.price.toLocaleString()} VND</Col>
                  {/* <Col span={4}>{element.unitInStock}</Col> */}
                  <Col span={4}>
                    <Form.Item name={`selected_supply_${element.sId}`}>
                      <InputNumber
                        defaultValue={0}
                        min={0}
                        max={element.unitInStock}
                        placeholder="Quantity"
                      />
                    </Form.Item>
                  </Col>
                </Row>
              </div>
            );
          })}
        </div>
      ),
    },
  ];

  useEffect(() => {
    if (medicalRecordId !== undefined) {
      fetchSelectedSupplies(medicalRecordId);
      fetchSupplyType();
    }
  }, [medicalRecordId, isReload]);

  const getTargetElement = () => document.getElementById("printTargetSuppPres");
  return supplyTypes !== undefined ? (
    <div style={{ minWidth: "1000px", width: "max-content" }}>
      <Button type="link" onClick={() => generatePDF(getTargetElement)}>
        Tải xuống dưới dạng PDF
      </Button>
      <div id="main-container">
        <Form
          id="supplyPresDetailForm"
          form={supplyPresForm}
          name="basic3"
          layout="vertical"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
        >
          <div id="printTargetSuppPres" style={{ padding: "20px" }}>
            <div id="mr-title">
              <Row gutter={10}>
                <Col span={8}>
                  <Form.Item<SupplyPresSelectFormModel>
                    label="Mã hồ sơ"
                    name="medicalRecordId"
                  >
                    <Input disabled />
                  </Form.Item>
                </Col>
              </Row>
              <Row gutter={10}></Row>
            </div>
            <div
              id="main-container"
              style={{
                border: "1px solid black",
                padding: "20px",
                borderRadius: "5px",
              }}
            >
              <Row>
                <Col>
                  <b>Đơn thuốc</b>
                </Col>
              </Row>
              <Divider />
              <Row gutter={10}>
                <Col span={12}>
                  <b>Tên thuốc</b>
                </Col>
                <Col span={4}>
                  <b>Số lượng</b>
                </Col>
                <Col span={4}>
                  <b>Giá tiền (VND)</b>
                </Col>
                <Col span={4}>
                  <b>Tạm tính (VND)</b>
                </Col>
              </Row>
              <Divider dashed />
              {selectedSupplies.map((element) => {
                return (
                  <div key={element.supplyId}>
                    <Row gutter={10}>
                      <Col span={12}>{element.supplyName}</Col>
                      <Col span={4}>{element.quantity}</Col>
                      <Col span={4}>{element.price.toLocaleString()} VND</Col>
                      <Col span={4}>
                        {(element.price * element.quantity).toLocaleString()}{" "}
                        VND
                      </Col>
                    </Row>
                  </div>
                );
              })}
              <Divider dashed />
              <Row>
                <Col span={16}></Col>
                <Col span={4}>
                  <b>Thành tiền</b>
                </Col>
                <Col span={4}>
                  <b>
                    {selectedSupplies
                      .reduce(
                        (sum, element) =>
                          sum + element.price * element.quantity,
                        0
                      )
                      .toLocaleString()}{" "}
                    VND
                  </b>
                </Col>
              </Row>
            </div>
            <Divider />
          </div>
          {/* <div id="add-supplies-container">
          <Row>
            <Col>
              <b>Chọn thêm thuốc</b>
            </Col>
          </Row>
          <br />
          <Form.Item<SupplyPresSelectFormModel>
            name="selectedSupplyTypes"
            label="Chọn loại thuốc"
          >
            <Select
              mode="multiple"
              onChange={handleChangeSupplyType}
              allowClear
              //set supply type options
              options={supplyTypeOptions}
            />
          </Form.Item>
          <Row gutter={10}>
            <Col span={10}>
              <b>Tên thuốc</b>
            </Col>
            <Col span={6}>
              <b>Giá tiền (VND)</b>
            </Col>
            <Col span={4}>
              <b>Số lượng còn lại</b>
            </Col>
            <Col span={4}>
              <b>Số lượng đã chọn</b>
            </Col>
          </Row>
          <Divider dashed />
          {availableSupplies.map((element) => {
            return (
              <div key={element.sId}>
                <Row gutter={10}>
                  <Col span={10}>{element.sName}</Col>
                  <Col span={6}>{element.price.toLocaleString()} VND</Col>
                  <Col span={4}>{element.unitInStock}</Col>
                  <Col span={4}>
                    <Form.Item name={`selected_supply_${element.sId}`}>
                      <InputNumber
                        min={0}
                        max={element.unitInStock}
                        placeholder="Quantity"
                      />
                    </Form.Item>
                  </Col>
                </Row>
              </div>
            );
          })}
          </div> */}
          {authenticated?.role !== Roles.Admin &&
          authenticated?.role !== Roles.Doctor ? (
            <></>
          ) : (
            <Collapse items={items} />
          )}
        </Form>
      </div>
    </div>
  ) : (
    <div>An Error Occurs When Getting Patient</div>
  );
};

export default SupplyPrescriptionDetailForm;
