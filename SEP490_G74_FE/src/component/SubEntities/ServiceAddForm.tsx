import { Form, Input, message, Col } from "antd";
import { ServiceAddModel } from "../../Models/SubEntityModel";

const ServiceAddForm = () => {
  const onFinish = (values: ServiceAddModel) => {
    message.success(values.name, 2);
  };

  const onFinishFailed = () => {
    message.error("Create MR Failed");
  };

  return (
    <Form
      id="serviceAddForm"
      name="basic"
      layout="vertical"
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      autoComplete="off"
    >
      <Form.Item<ServiceAddModel> label="Tên dịch vụ" name="name">
        <Input placeholder="Tên dịch vụ" />
      </Form.Item>
    </Form>
  );
};

export default ServiceAddForm;
