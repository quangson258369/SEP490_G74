import { Form, Input, message, Col } from "antd";
import { ServiceTypeAddModel } from "../../Models/SubEntityModel";

const ServiceTypeAddForm = () => {
  const onFinish = (values: ServiceTypeAddModel) => {
    message.success(values.name, 2);
  };

  const onFinishFailed = () => {
    message.error("Create MR Failed");
  };

  return (
    <Form
      id="serviceTypeAddForm"
      name="basic"
      layout="vertical"
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      autoComplete="off"
    >
      <Form.Item<ServiceTypeAddModel> label="Tên loại dịch vụ" name="name">
        <Input placeholder="Tên loại dịch vụ" />
      </Form.Item>
    </Form>
  );
};

export default ServiceTypeAddForm;
