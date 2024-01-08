import { Button, Col, Modal, Row } from "antd";
import { useState } from "react";
import PatientAddForm from "../component/Patient/PatientAddForm";
import CategoryAddForm from "../component/SubEntities/CategoryAddForm";
import ServiceTypeAddForm from "../component/SubEntities/ServiceTypeAddForm";
import ServiceAddForm from "../component/SubEntities/ServiceAddForm";
interface ParentCompProps {
  childComp?: React.ReactNode;
}

const AdminPage: React.FC<ParentCompProps> = (props: any) => {
  const { childComp } = props;
  const [open, setOpen] = useState<boolean>(false);
  const [openCategory, setOpenCategory] = useState<boolean>(false);
  const [openServiceType, setOpenServiceType] = useState<boolean>(false);
  const [openService, setOpenService] = useState<boolean>(false);

  const handleCancel = () => {
    setOpen(false);
  };
  const handleCancelCategory = () => {
    setOpenCategory(false);
  };
  const handleCancelServiceType = () => {
    setOpenServiceType(false);
  };
  const handleCancelService = () => {
    setOpenService(false);
  };
  const handleAddPatient = () => {
    setOpen(true);
  };
  const handleAddCategory = () => {
    setOpenCategory(true);
  };
  const handleAddServiceType = () => {
    setOpenServiceType(true);
  };
  const handleAddService = () => {
    setOpenService(true);
  };
  return (
    <div>
      <div>
        <Row>
          <Col span={6}>
            <Button type="primary" onClick={handleAddPatient}>
              Thêm mới bệnh nhân
            </Button>
          </Col>
          <Col span={6}>
            <Button type="primary" onClick={handleAddCategory}>
              Thêm mới khoa khám
            </Button>
          </Col>
          <Col span={6}>
            <Button type="primary" onClick={handleAddServiceType}>
              Thêm mới loại dịch vụ
            </Button>
          </Col>
          <Col span={6}>
            <Button type="primary" onClick={handleAddService}>
              Thêm mới dịch vụ
            </Button>
          </Col>
        </Row>
        <Modal
          title="Thêm mới bệnh nhân"
          open={open}
          onCancel={handleCancel}
          maskClosable={false}
          width="max-content"
          footer={[
            <Button key="back" onClick={handleCancel}>
              Hủy
            </Button>,
            <Button
              key="submit"
              type="primary"
              form="patientAddForm"
              htmlType="submit"
            >
              Lưu
            </Button>,
          ]}
        >
          <PatientAddForm />
        </Modal>

        <Modal
          title="Thêm mới khoa khám"
          open={openCategory}
          onCancel={handleCancelCategory}
          maskClosable={false}
          width="500px"
          footer={[
            <Button key="back" onClick={handleCancelCategory}>
              Hủy
            </Button>,
            <Button
              key="submit"
              type="primary"
              form="categoryAddForm"
              htmlType="submit"
            >
              Lưu
            </Button>,
          ]}
        >
          <CategoryAddForm />
        </Modal>

        <Modal
          title="Thêm mới loại dịch vụ"
          open={openServiceType}
          onCancel={handleCancelServiceType}
          maskClosable={false}
          width="500px"
          footer={[
            <Button key="back" onClick={handleCancelServiceType}>
              Hủy
            </Button>,
            <Button
              key="submit"
              type="primary"
              form="serviceTypeAddForm"
              htmlType="submit"
            >
              Lưu
            </Button>,
          ]}
        >
          <ServiceTypeAddForm />
        </Modal>

        <Modal
          title="Thêm mới dịch vụ"
          open={openService}
          onCancel={handleCancelService}
          maskClosable={false}
          width="500px"
          footer={[
            <Button key="back" onClick={handleCancelService}>
              Hủy
            </Button>,
            <Button
              key="submit"
              type="primary"
              form="serviceAddForm"
              htmlType="submit"
            >
              Lưu
            </Button>,
          ]}
        >
          <ServiceAddForm />
        </Modal>
      </div>
      <br />
      <div>{childComp}</div>
    </div>
  );
};

export default AdminPage;
