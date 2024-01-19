import React, { useEffect, useRef, useState } from "react";
import {
  Button,
  Space,
  Table,
  message,
  Modal,
  Form,
  Row,
  Col,
  Select,
  InputNumber,
} from "antd";
import { ColumnType, ColumnsType } from "antd/es/table";
import Input, { InputRef } from "antd/es/input/Input";
import {
  CategoryAddModel,
  CategoryResponseModel,
} from "../../Models/SubEntityModel";
import categoryService from "../../Services/CategoryService";
import { FilterConfirmProps } from "antd/es/table/interface";
import { SearchOutlined } from "@ant-design/icons";
import Highlighter from "react-highlight-words";
import {
  AccountResponseModel,
  JWTTokenModel,
  RoleResponseModel,
  UpdateAccountModel,
} from "../../Models/AuthModel";
import authService from "../../Services/AuthService";
import { TOKEN } from "../../Commons/Global";
import { jwtDecode } from "jwt-decode";
import { useForm } from "antd/es/form/Form";

const AccountsTable: React.FC = () => {
  const [searchText, setSearchText] = useState("");
  const [searchedColumn, setSearchedColumn] = useState("");
  const searchInput = useRef<InputRef>(null);
  type DataIndex = keyof AccountResponseModel;
  const handleSearch = (
    selectedKeys: string[],
    confirm: (param?: FilterConfirmProps) => void,
    dataIndex: DataIndex
  ) => {
    confirm();
    setSearchText(selectedKeys[0]);
    setSearchedColumn(dataIndex);
  };

  const handleReset = (clearFilters: () => void) => {
    clearFilters();
    setSearchText("");
  };

  const getColumnSearchProps = (
    dataIndex: DataIndex
  ): ColumnType<AccountResponseModel> => ({
    filterDropdown: ({
      setSelectedKeys,
      selectedKeys,
      confirm,
      clearFilters,
      close,
    }) => (
      <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
        <Input
          ref={searchInput}
          placeholder={`Search ${dataIndex}`}
          value={selectedKeys[0]}
          onChange={(e) =>
            setSelectedKeys(e.target.value ? [e.target.value] : [])
          }
          onPressEnter={() =>
            handleSearch(selectedKeys as string[], confirm, dataIndex)
          }
          style={{ marginBottom: 8, display: "block" }}
        />
        <Space>
          <Button
            type="primary"
            onClick={() =>
              handleSearch(selectedKeys as string[], confirm, dataIndex)
            }
            icon={<SearchOutlined />}
            size="small"
            style={{ width: 90 }}
          >
            Search
          </Button>
          <Button
            onClick={() => clearFilters && handleReset(clearFilters)}
            size="small"
            style={{ width: 90 }}
          >
            Reset
          </Button>
          <Button
            type="link"
            size="small"
            onClick={() => {
              confirm({ closeDropdown: false });
              setSearchText((selectedKeys as string[])[0]);
              setSearchedColumn(dataIndex);
            }}
          >
            Filter
          </Button>
          <Button
            type="link"
            size="small"
            onClick={() => {
              close();
            }}
          >
            close
          </Button>
        </Space>
      </div>
    ),
    filterIcon: (filtered: boolean) => (
      <SearchOutlined style={{ color: filtered ? "#1677ff" : undefined }} />
    ),
    onFilter: (value, record) =>
      record[dataIndex]
        .toString()
        .toLowerCase()
        .includes((value as string).toLowerCase()),
    onFilterDropdownOpenChange: (visible) => {
      if (visible) {
        setTimeout(() => searchInput.current?.select(), 100);
      }
    },
    render: (text) =>
      searchedColumn === dataIndex ? (
        <Highlighter
          highlightStyle={{ backgroundColor: "#ffc069", padding: 0 }}
          searchWords={[searchText]}
          autoEscape
          textToHighlight={text ? text.toString() : ""}
        />
      ) : (
        text
      ),
  });

  //===================== State =====================
  const [accounts, setAccounts] = useState<AccountResponseModel[]>([]);

  const [editAccountModalVisible, setEditAccountModalVisible] = useState(false);

  const [selectedAccountId, setSelectedAccountId] = useState<number | null>(
    null
  );

  const [accountEditForm] = useForm();

  //===================handle cancel==================
  //===== handle cancel edit form =====
  const handleCancelAccountEdit = () => {
    setEditAccountModalVisible(false);
  };

  //=============handle open edit modal
  const handleOpenEditAccount = (id: number) => {
    setSelectedAccountId(id);
    setEditAccountModalVisible(true);
    accountEditForm.setFieldsValue({ userId: id });
  };

  //===================== Columns =====================
  const accountsColumns: ColumnsType<AccountResponseModel> = [
    {
      title: "ID",
      dataIndex: "userId",
      key: "action_userId",
      sorter: (a, b) => a.userId - b.userId,
    },
    {
      title: "Name",
      dataIndex: "userName",
      key: "action_userName",
      sorter: (a, b) => a.userName.length - b.userName.length,
      ...getColumnSearchProps("userName"),
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "action_email",
      sorter: (a, b) => a.email.length - b.email.length,
      ...getColumnSearchProps("email"),
    },
    {
      title: "Chức vụ",
      dataIndex: "roleName",
      key: "action_roleName",
      sorter: (a, b) => a.roleName.length - b.roleName.length,
      ...getColumnSearchProps("roleName"),
    },
    {
      title: "",
      key: `actionC`,
      render: (_, record) => (
        <Space size="middle" key={`space_${record.userId}`}>
          <Button
            key={`editC_${record.userId}`}
            type="primary"
            onClick={() => handleOpenEditAccount(record.userId)}
          >
            Chỉnh sửa
          </Button>
          <Button
            key={`removeC_${record.userId}`}
            danger
            type="primary"
            onClick={() => handleDeleteAccount(record.userId)}
          >
            Vô hiệu hóa
          </Button>
        </Space>
      ),
    },
  ];

  //===================== Fetch Data =====================

  const fetchAccounts = async () => {
    var response = await authService.getAccounts();
    if (response === undefined) {
      message.error("Tải tài khoản thất bại", 2);
    } else {
      console.log(response);
      setAccounts(response);
      return response;
    }
  };

  //==============Delete=================
  //show confirm dialog before delete
  const handleDeleteAccount = (id: number) => {
    Modal.confirm({
      title: "Xác nhận Vô hiệu hóa",
      content: "Bạn có chắc chắn muốn Vô hiệu hóa?",
      onOk: () => {
        removeAccount(id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };
  //===========Delete API=============
  const removeAccount = async (id: number) => {

    var token = localStorage.getItem(TOKEN);
    if(token !== null){
      var user : JWTTokenModel = jwtDecode(token);
      if(user!==undefined){
        if(user.nameid !== undefined){
          if(Number.parseInt(user.nameid) === id){
            message.error("Không thể Vô hiệu hóa chính mình", 2);
            return;
          }
        }
      }
    }

    var response = await authService.deleteAccount(id);
    if (response !== undefined && response === 200) {
      message.success("Delete Success", 1).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Delete Failed");
    }
  };

  //=============================

  useEffect(() => {
    const fetchData = async () => {
      await fetchAccounts();
    };
    fetchData();
  }, []);

  // Edit account
  const [roles, setRoles] = useState<RoleResponseModel[]>([]);
  const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  const [isDoctor, setIsDoctor] = useState<boolean>(false);

  const onFinishEditAccount = async (values: UpdateAccountModel) => {
    if (values.roleId !== 2) {
      values.categoryId = 0;
    }
    values = {...values, userId: selectedAccountId ?? 0}
    console.log(values);
    var response = await authService.updateAccount(values);
    if (response !== undefined && response === 200) {
      message.success("Update Success", 1).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Update Failed");
    }
  };

  const onFinishFailed = () => {
    message.error("Create Failed");
  };

  const fetchRoles = async () => {
    var response = await authService.getRoles();
    if (response !== undefined) {
      setRoles(response);
    } else {
      message.error("Get Roles Failed");
    }
  };

  const fetchCates = async () => {
    var response = await categoryService.getCategories();
    if (response !== undefined) {
      setCates(response);
    } else {
      message.error("Get Roles Failed");
    }
  };

  const prepareData = async () => {
    var response = await Promise.all([fetchRoles(), fetchCates()]);
    if (response !== undefined) {
      console.log(response);
    }
  };

  const handleOnChane = (values: number) => {
    console.log(values);
    if (values === 2) {
      setIsDoctor(true);
    } else {
      setIsDoctor(false);
    }
  };

  useEffect(() => {
    prepareData();
  }, [isDoctor]);

  // Filter Search

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        height: "auto",
        minHeight:"100vh"
      }}
    >
      {/*===================== Accounts =====================*/}
      <h2>Nhân sự</h2>
      <Row>
        <Col span={24}></Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<AccountResponseModel>
            rowKey={(record) => `id_${record.userId}`}
            dataSource={accounts}
            columns={accountsColumns}
            pagination={{ pageSize: 5 }}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedAccountId(record.userId),
            })}
          />
        </Col>
      </Row>

      {/*=============== Edit account modal ========================*/}
      <Modal
        title="Chỉnh sửa account"
        key="editSubModal"
        onCancel={handleCancelAccountEdit}
        destroyOnClose={true}
        open={editAccountModalVisible}
        footer={[
          <Button
            key="editForm"
            form="editForm"
            type="primary"
            htmlType="submit"
          >
            Chỉnh sửa
          </Button>,
          <Button
            key="removeF"
            type="primary"
            onClick={handleCancelAccountEdit}
          >
            Hủy
          </Button>,
        ]}
      >
        {/* Edit account form */}
        <Form
          id="editForm"
          form={accountEditForm}
          name="basic"
          layout="vertical"
          onFinish={onFinishEditAccount}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
        >
          <Form.Item<UpdateAccountModel> label="ID" name="userId">
            <InputNumber disabled />
          </Form.Item>
          <Form.Item<UpdateAccountModel> label="Password" name="password">
            <Input type="password" placeholder="Password" />
          </Form.Item>
          <Form.Item<UpdateAccountModel>
            label="Confirm Password"
            name="confirmPassword"
          >
            <Input type="password" placeholder="Password" />
          </Form.Item>
          <Form.Item<UpdateAccountModel> name="roleId" label="Chọn chức vụ">
            <Select
              onChange={handleOnChane}
              options={roles.map((role) => ({
                value: role.roleId,
                label: role.roleName,
              }))}
            />
          </Form.Item>
          <Form.Item<UpdateAccountModel>
            name="categoryId"
            label="Khoa khám (nếu là bác sĩ)"
          >
            <Select
              disabled={!isDoctor}
              options={cates.map((cate) => ({
                value: cate.categoryId,
                label: cate.categoryName,
              }))}
            />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default AccountsTable;
