import { Form, Input, message, Col, InputNumber } from "antd";
import { CategoryAddModel } from "../../Models/SubEntityModel";
import categoryService from "../../Services/CategoryService";
import { AddAccountModel } from "../../Models/AuthModel";

const AccountAddForm = () => {
  const onFinish = async (values: AddAccountModel) => {
    message.info("Chức năng đang trong quá trình phát triển");
  };

  const onFinishFailed = () => {
    message.error("Create Failed");
  };

  return (
    <Form
      id="categoryAddForm"
      name="basic"
      layout="vertical"
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      autoComplete="off"
    >
      <Form.Item label="Tên tài khoản" >
        <Input placeholder="Tên tài khoản"/>
      </Form.Item>
      <Form.Item<AddAccountModel> label="Email" name="email">
        <Input type="email" placeholder="Email" />
      </Form.Item>
      <Form.Item<AddAccountModel> label="Password" name="password">
        <Input type="password" placeholder="Password" />
      </Form.Item>
      <Form.Item label="Chức vụ" >
        <Input placeholder="Chức vụ"/>
      </Form.Item>
      <Form.Item label="Khoa khám (nếu là bác sĩ)">
        <Input placeholder="khoa khám"/>
      </Form.Item>
    </Form>
  );
};

export default AccountAddForm;
