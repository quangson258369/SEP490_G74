import React, { useEffect, useState } from "react";
import { Button, Space, Table, message, Modal, Form, Row, Col } from "antd";
import { ColumnsType } from "antd/es/table";
import Search from "antd/es/input/Search";
import Input from "antd/es/input/Input";
import { CategoryResponseModel } from "../../Models/SubEntityModel";
import categoryService from "../../Services/CategoryService";

interface Category {
  id: number;
  name: string;
}

interface ServiceType {
  id: number;
  name: string;
}

interface Service {
  id: number;
  name: string;
}

const SubEntityTable: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [serviceTypes, setServiceTypes] = useState<ServiceType[]>([]);
  const [services, setServices] = useState<Service[]>([]);

  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editForm] = Form.useForm();
  const [selectedId, setSelectedId] = useState<number | null>(null);

  const [cates, setCates] = useState<CategoryResponseModel[]>([]);

  const handleEdit = (id: number) => {
    setEditModalVisible(true);
    message.info("Editing " + id, 2);
  };

  const handleEditName = (values: Category | ServiceType | Service) => {
    message.success(
      "Name edited successfully: " + values.id + " - " + values.name,
      2
    );

    // Close the modal
    setEditModalVisible(false);
  };

  const handleCancelEdit = () => {
    // Close the modal without performing any edit operation
    setEditModalVisible(false);
  };

  const handleRemove = (id: number) => {
    Modal.confirm({
      title: "Xác nhận xóa",
      content: "Bạn có chắc chắn muốn xóa?",
      onOk: () => {
        message.error("Đã xóa " + id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };

  const lcategories: Category[] = [
    { id: 1, name: "Category 1 kjdbaksjdbaksjdbaksjdbaskjsdbak.b" },
    { id: 2, name: "Category 2" },
    { id: 3, name: "Category 3" },
  ];

  const lserviceTypes: ServiceType[] = [
    { id: 1, name: "Service Type 1" },
    { id: 2, name: "Service Type 2" },
    { id: 3, name: "Service Type 3" },
  ];

  const lservices: Service[] = [
    { id: 1, name: "Service 1" },
    { id: 2, name: "Service 2" },
    { id: 3, name: "Service 3" },
  ];

  const columns: ColumnsType<Category | ServiceType | Service> = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Button
            key="edit"
            type="primary"
            onClick={() => handleEdit(record.id)}
          >
            Chỉnh sửa
          </Button>
          <Button
            key="remove"
            danger
            type="primary"
            onClick={() => handleRemove(record.id)}
          >
            Xóa
          </Button>
        </Space>
      ),
    },
  ];

  const cateColumns: ColumnsType<CategoryResponseModel> = [
    {
      title: "ID",
      dataIndex: "categoryId",
      key: "categoryId",
    },
    {
      title: "Name",
      dataIndex: "categoryName",
      key: "categoryName",
    },
    {
      title: "",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Button
            key="edit"
            type="primary"
            onClick={() => handleEdit(record.categoryId)}
          >
            Chỉnh sửa
          </Button>
          <Button
            key="remove"
            danger
            type="primary"
            onClick={() => handleRemove(record.categoryId)}
          >
            Xóa
          </Button>
        </Space>
      ),
    },
  ];

  const fetchCategories = async () => {
    var response = await categoryService.getCategories();
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
    } else {
      console.log(response);
      setCates(response);
    }
  }

  useEffect(() => {
    setCategories(lcategories);
    setServiceTypes(lserviceTypes);
    setServices(lservices);
  }, []);

  const handleSearchCategories = (e: any) => {
    const filteredCategories = lcategories.filter((category) =>
      category.name.toLowerCase().includes(e.target.value.toLowerCase().trim())
    );
    setCategories(filteredCategories);
  };

  const handleSearchServiceTypes = (e: any) => {
    const filteredServiceTypes = lserviceTypes.filter((serviceType) =>
      serviceType.name
        .toLowerCase()
        .includes(e.target.value.toLowerCase().trim())
    );
    setServiceTypes(filteredServiceTypes);
  };

  const handleSearchServices = (e: any) => {
    const filteredServices = lservices.filter((service) =>
      service.name.toLowerCase().includes(e.target.value.toLowerCase().trim())
    );
    setServices(filteredServices);
  };

  useEffect(() => {
    editForm.setFieldsValue({
      id: selectedId,
    });
    fetchCategories();
  }, [selectedId]);

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <h2>Khoa khám</h2>
      <Row>
        <Col span={24}>
          <Search
            key="1"
            placeholder="Nhập tên"
            onChange={handleSearchCategories}
          />
        </Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<CategoryResponseModel>
            rowKey="id"
            dataSource={cates}
            columns={cateColumns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedId(record.categoryId), // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row>
      <h2>Loại dịch vụ</h2>
      <Row>
        <Col span={24}>
          <Search
            key="2"
            placeholder="Nhập tên"
            onChange={handleSearchServiceTypes}
          />
        </Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<ServiceType>
            rowKey="id"
            dataSource={serviceTypes}
            columns={columns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedId(record.id), // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row>

      <h2>Dịch vụ</h2>
      <Row>
        <Col span={24}>
          <Search
            key="3"
            placeholder="Nhập tên"
            onChange={handleSearchServices}
          />
        </Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<Service>
            rowKey="id"
            dataSource={services}
            columns={columns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedId(record.id), // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row>
      <Modal
        title="Chỉnh sửa"
        key="editModal"
        onCancel={handleCancelEdit}
        open={editModalVisible}
        footer={[
          <Button key="submit" form="editForm" type="primary" htmlType="submit">
            Chỉnh sửa
          </Button>,
          <Button key="remove" type="primary" onClick={handleCancelEdit}>
            Hủy
          </Button>,
        ]}
      >
        <Form
          id="editForm"
          form={editForm}
          layout="vertical"
          name="basic"
          onFinish={handleEditName}
        >
          <Form.Item<Category | ServiceType | Service> label="ID" name="id">
            <Input placeholder="ID" disabled />
          </Form.Item>
          <Form.Item<Category | ServiceType | Service> label="Tên" name="name">
            <Input placeholder="Nhập tên" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default SubEntityTable;
