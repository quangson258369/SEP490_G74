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
  InputNumber,
  DatePicker,
} from "antd";
import { ColumnType, ColumnsType } from "antd/es/table";
import Search from "antd/es/input/Search";
import Input, { InputRef } from "antd/es/input/Input";
import {
  CategoryAddModel,
  CategoryResponseModel,
  ServiceAddModel,
  ServiceResponseModel,
  ServiceTypeAddModel,
  ServiceTypeResponseModel,
  SupplyAddModel,
  SupplyResponseModel,
  SupplyTypeAddModel,
  SupplyTypeResponseModel,
} from "../../Models/SubEntityModel";
import categoryService from "../../Services/CategoryService";
import subService from "../../Services/SubService";
import ServiceTypeAddForm from "../SubEntities/ServiceTypeAddForm";
import ServiceAddForm from "../SubEntities/ServiceAddForm";
import CategoryAddForm from "../SubEntities/CategoryAddForm";
import dayjs from "dayjs";
import { FilterConfirmProps } from "antd/es/table/interface";
import { SearchOutlined } from "@ant-design/icons";
import Highlighter from "react-highlight-words";

const CategoryTable: React.FC = () => {
  const [searchText, setSearchText] = useState("");
  const [searchedColumn, setSearchedColumn] = useState("");
  const searchInput = useRef<InputRef>(null);
  type DataIndex = keyof CategoryResponseModel
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
  ): ColumnType<
    CategoryResponseModel
  > => ({
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
  const [cates, setCates] = useState<CategoryResponseModel[]>([]);
  const [serviceTypes, setServiceTypes] = useState<ServiceTypeResponseModel[]>(
    []
  );
  const [services, setServices] = useState<ServiceResponseModel[]>([]);

  const [supplyTypes, setSupplyTypes] = useState<SupplyTypeResponseModel[]>([]);
  const [supplies, setSupplies] = useState<SupplyResponseModel[]>([]);

  //=====================================
  const [editCateModalVisible, setEditCateModalVisible] = useState(false);
  const [editTypeModalVisible, setEditTypeModalVisible] = useState(false);
  const [editServiceModalVisible, setEditServiceModalVisible] = useState(false);
  const [editSupplyTypeModalVisible, setEditSupplyTypeModalVisible] =
    useState(false);
  const [editSupplyModalVisible, setEditSupplyModalVisible] = useState(false);

  const [editCateForm] = Form.useForm();
  const [editTypeForm] = Form.useForm();
  const [editServiceForm] = Form.useForm();
  const [editSupplyTypeForm] = Form.useForm();
  const [editSupplyForm] = Form.useForm();

  const [supplyTypeAddForm] = Form.useForm();

  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(
    null
  );
  const [selectedTypeId, setSelectedTypeId] = useState<number | null>(null);
  const [selectedServiceId, setSelectedServiceId] = useState<number | null>(
    null
  );

  const [selectedSupplyTypeId, setSelectedSupplyTypeId] = useState<
    number | null
  >(null);
  const [selectedSupplyId, setSelectedSupplyId] = useState<number | null>(null);

  //================== open flag =============
  const [openServiceType, setOpenServiceType] = useState<boolean>(false);
  const [openService, setOpenService] = useState<boolean>(false);
  const [openSupplyType, setOpenSupplyType] = useState<boolean>(false);
  const [openSupply, setOpenSupply] = useState<boolean>(false);

  //====== add supply type modal
  const [openAddSupplyType, setOpenAddSupplyType] = useState<boolean>(false);

  //===================handle cancel==================
  //===== handle cancel edit form =====
  const handleCancelCateEdit = () => {
    setEditCateModalVisible(false);
  };

  const handleCancelTypeEdit = () => {
    setEditTypeModalVisible(false);
  };

  const handleCancelServiceEdit = () => {
    setEditServiceModalVisible(false);
  };

  const handleCancelSupplyTypeEdit = () => {
    setEditSupplyTypeModalVisible(false);
  };

  const handleCancelSupplyEdit = () => {
    setEditSupplyModalVisible(false);
  };

  //===== handle cancel modal =====
  const handleCancelServiceType = () => {
    setOpenServiceType(false);
  };

  const handleCancelService = () => {
    setOpenService(false);
  };

  const handleCancelSupplyType = () => {
    setOpenSupplyType(false);
  };

  const handleCancelSupply = () => {
    setOpenSupply(false);
  };

  //================handle add
  const handleAddServiceType = (id: number) => {
    setSelectedCategoryId(id);
    setOpenServiceType(true);
  };

  const handleAddService = (id: number) => {
    setSelectedTypeId(id);
    setOpenService(true);
  };

  const handleAddSupply = (id: number) => {
    setSelectedSupplyTypeId(id);
    setOpenSupply(true);
  };

  //=============handle open edit modal
  const handleOpenEditCategory = (id: number) => {
    setSelectedCategoryId(id);
    setEditCateModalVisible(true);
  };

  const handleOpenEditServiceType = (
    id: number,
    categoryId: number | undefined | null
  ) => {
    if (categoryId === undefined || categoryId === null) {
      message.error("Loại dịch vụ có khoa khám không hợp lệ", 2);
      return;
    }
    setSelectedTypeId(id);
    setSelectedCategoryId(categoryId);
    setEditTypeModalVisible(true);
  };

  const handleOpenEditService = (
    id: number,
    serviceTypeId: number | undefined | null
  ) => {
    if (serviceTypeId === undefined || serviceTypeId === null) {
      message.error("Dịch vụ có loại dịch vụ không hợp lệ", 2);
      return;
    }
    setSelectedTypeId(serviceTypeId);
    setSelectedServiceId(id);
    setEditServiceModalVisible(true);
  };

  const handleOpenEditSupplyType = (id: number) => {
    setSelectedSupplyTypeId(id);
    setEditSupplyTypeModalVisible(true);
  };

  const handleOpenEditSupply = (
    id: number,
    supplyTypeId: number | undefined | null
  ) => {
    if (supplyTypeId === undefined || supplyTypeId === null) {
      message.error("Dịch vụ có loại dịch vụ không hợp lệ", 2);
      return;
    }
    setSelectedSupplyId(id);
    setSelectedSupplyTypeId(supplyTypeId);
    setEditSupplyModalVisible(true);
  };

  //============ Handle Edit Call API =============

  const handleEditCategory = async (values: CategoryResponseModel) => {
    if (selectedCategoryId === null) {
      message.error("Vui lòng chọn khoa khám", 2);
      return;
    }

    var editCartMode: CategoryAddModel = {
      categoryName: values.categoryName,
    };

    var response = await categoryService.updateCategory(
      selectedCategoryId,
      editCartMode
    );
    if (response === 204) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditCateModalVisible(false);
  };

  const handleEditType = async (values: ServiceTypeResponseModel) => {
    if (selectedTypeId === null) {
      message.error("Vui lòng chọn loại dịch vụ khám", 2);
      return;
    }

    if (selectedCategoryId === null) {
      message.error("Loại dịch vụ có khoa khám không hợp lệ", 2);
      return;
    }

    var editTypeModel: ServiceTypeAddModel = {
      serviceTypeName: values.serviceTypeName,
      categoryId: selectedCategoryId,
    };

    var response = await subService.updateServiceType(
      selectedTypeId,
      editTypeModel
    );
    if (response === 200) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditTypeModalVisible(false);
  };

  const handleEditService = async (values: ServiceResponseModel) => {
    if (selectedServiceId === null) {
      message.error("Vui lòng chọn loại dịch vụ khám", 2);
      return;
    }

    if (selectedTypeId === null) {
      message.error("Dịch vụ có loại khoa khám không hợp lệ", 2);
      return;
    }

    var editServiceModel: ServiceAddModel = {
      serviceName: values.serviceName,
      serviceTypeId: selectedTypeId,
      price: values.price,
    };

    var response = await subService.updateService(
      selectedServiceId,
      editServiceModel
    );
    if (response === 200) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditServiceModalVisible(false);
  };

  //================= Handle Add Call API =================
  const handleInsertSupplyType = async (values: SupplyTypeResponseModel) => {

    var supplyTypeAdd: SupplyTypeAddModel = {
      suppliesTypeName: values.suppliesTypeName,
    };

    var response = await subService.addSupplyType(supplyTypeAdd);
    if (response !== undefined && response >= 200 && response < 300) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditTypeModalVisible(false);
  };

  const handleUpdateSupplyType = async (values: SupplyTypeResponseModel) => {
    message.error(`Check supply type add click: ${values.suppliesTypeName}`, 2);

    if (selectedSupplyTypeId === null || selectedSupplyTypeId === undefined) {
      message.error("Loại thuốc không hợp lệ", 2);
      return;
    }

    var supplyTypeAdd: SupplyTypeAddModel = {
      suppliesTypeName: values.suppliesTypeName,
    };

    var response = await subService.updateSupplyType(
      selectedSupplyTypeId,
      supplyTypeAdd
    );
    if (response !== undefined && response >= 200 && response < 300) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditTypeModalVisible(false);
  };

  const handleInsertSupply = async (values: SupplyResponseModel) => {
    //message.error(`Check supply type add click: ${values.sName}`, 2);

    if (selectedSupplyTypeId === null || selectedSupplyTypeId === undefined) {
      message.error("Loại thuốc không hợp lệ", 2);
      return;
    }

    var supplyAdd: SupplyAddModel = {
      sName: values.sName,
      suppliesTypeId: selectedSupplyTypeId,
      distributor: values.distributor,
      price: values.price,
      unitInStock: values.unitInStock,
      exp: dayjs(values.exp).format("YYYY-MM-DDTHH:mm:ss.SSSZ"),
      uses: values.uses,
    };

    console.log(supplyAdd);

    var response = await subService.addSupply(supplyAdd);
    if (response !== undefined && response >= 200 && response < 300) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditSupplyModalVisible(false);
  };

  const handleUpdateSupply = async (values: SupplyResponseModel) => {
    //message.error(`Check supply type add click: ${values.suppliesTypeName}`, 2);

    if (
      selectedSupplyTypeId === null ||
      selectedSupplyTypeId === undefined ||
      selectedSupplyId === null ||
      selectedSupplyId === undefined
    ) {
      message.error("Loại thuốc không hợp lệ", 2);
      return;
    }

    var supplyAdd: SupplyAddModel = {
      sName: values.sName,
      suppliesTypeId: selectedSupplyTypeId,
      distributor: values.distributor,
      price: values.price,
      unitInStock: values.unitInStock,
      exp: dayjs(values.exp).format("YYYY-MM-DDTHH:mm:ss.SSSZ"),
      uses: values.uses,
    };

    console.log(selectedSupplyId);

    var response = await subService.updateSupply(selectedSupplyId, supplyAdd);
    if (response !== undefined && response >= 200 && response < 300) {
      message.success("Chỉnh sửa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Chỉnh sửa thất bại", 2);
    }
    setEditSupplyModalVisible(false);
  };

  //===================== Columns =====================

  const cateColumns: ColumnsType<CategoryResponseModel> = [
    {
      title: "ID",
      dataIndex: "categoryId",
      key: "actionC_categoryId",
      sorter: (a, b) => a.categoryId - b.categoryId,
    },
    {
      title: "Name",
      dataIndex: "categoryName",
      key: "actionC_categoryName",
      sorter: (a, b) => a.categoryName.length - b.categoryName.length,
      ...getColumnSearchProps("categoryName"),
    },
    {
      title: "",
      key: `actionC`,
      render: (_, record) => (
        <Space size="middle" key={`space_${record.categoryId}`}>
          <Button
            key={`editC_${record.categoryId}`}
            type="primary"
            onClick={() => handleOpenEditCategory(record.categoryId)}
          >
            Chỉnh sửa
          </Button>
          <Button
            key={`removeC_${record.categoryId}`}
            danger
            type="primary"
            onClick={() => handleDeleteCategory(record.categoryId)}
          >
            Vô hiệu hóa
          </Button>
          <Button
            key={`addTypeC_${record.categoryId}`}
            type="primary"
            onClick={() => handleAddServiceType(record.categoryId)}
          >
            Thêm mới loại dịch vụ
          </Button>
        </Space>
      ),
    },
  ];

  const typeColumns: ColumnsType<ServiceTypeResponseModel> = [
    {
      title: "ID",
      dataIndex: "serviceTypeId",
      key: "actionT_serviceTypeId",
      sorter: (a, b) => a.serviceTypeId - b.serviceTypeId,
    },
    {
      title: "Name",
      dataIndex: "serviceTypeName",
      key: "actionT_serviceTypeName",
      sorter: (a, b) => a.serviceTypeName.length - b.serviceTypeName.length,
    },
    {
      title: "Category",
      dataIndex: "categoryName",
      key: "actionT_categoryName",
      sorter: (a, b) => a.categoryName!.length - b.categoryName!.length,
    },
    {
      title: "",
      key: "actionT",
      render: (_, record) => (
        <Space size="middle" key={`spaceT_${record.serviceTypeId}`}>
          <Button
            key={`editT_${record.serviceTypeId}`}
            type="primary"
            onClick={() =>
              handleOpenEditServiceType(record.serviceTypeId, record.categoryId)
            }
          >
            Chỉnh sửa
          </Button>
          <Button
            key={`removeT_${record.serviceTypeId}`}
            danger
            type="primary"
            onClick={() => handleDeleteServiceType(record.serviceTypeId)}
          >
            Vô hiệu hóa
          </Button>
          <Button
            key={`addTypeC_${record.serviceTypeId}`}
            type="primary"
            onClick={() => handleAddService(record.serviceTypeId)}
          >
            Thêm mới dịch vụ
          </Button>
        </Space>
      ),
    },
  ];

  const servicesColumns: ColumnsType<ServiceResponseModel> = [
    {
      title: "ID",
      dataIndex: "serviceId",
      key: "actionC_serviceId",
      sorter: (a, b) => a.serviceId - b.serviceId,
    },
    {
      title: "Name",
      dataIndex: "serviceName",
      key: "actionC_serviceName",
      sorter: (a, b) => a.serviceName.length - b.serviceName.length,
    },
    {
      title: "Giá",
      dataIndex: "price",
      key: "actionC_price",
      sorter: (a, b) => a.price - b.price,
    },
    {
      title: "Type",
      dataIndex: "serviceTypeName",
      key: "actionC_serviceTypeName",
      sorter: (a, b) => a.serviceTypeName!.length - b.serviceTypeName!.length,
    },
    {
      title: "",
      key: "actionC",
      render: (_, record) => (
        <Space size="middle" key={`spaceS_${record.serviceId}`}>
          <Button
            key={`editS_${record.serviceId}`}
            type="primary"
            onClick={() =>
              handleOpenEditService(record.serviceId, record.serviceTypeId)
            }
          >
            Chỉnh sửa
          </Button>
          <Button
            key={`removeS_${record.serviceId}`}
            danger
            type="primary"
            onClick={() => handleDeleteService(record.serviceId)}
          >
            Vô hiệu hóa
          </Button>
        </Space>
      ),
    },
  ];

  const supplyTypeColumns: ColumnsType<SupplyTypeResponseModel> = [
    {
      title: "ID",
      dataIndex: "suppliesTypeId",
      key: "actionT_suppliesTypeId",
      sorter: (a, b) => a.suppliesTypeId - b.suppliesTypeId,
    },
    {
      title: "Name",
      dataIndex: "suppliesTypeName",
      key: "actionT_suppliesTypeName",
      sorter: (a, b) => a.suppliesTypeName.length - b.suppliesTypeName.length,
    },
    // {
    //   title: "Category",
    //   dataIndex: "categoryName",
    //   key: "actionT_categoryName",
    //   sorter: (a, b) => a.categoryName!.length - b.categoryName!.length,
    // },
    {
      title: "",
      key: "actionT",
      render: (_, record) => (
        <Space size="middle" key={`spaceT_${record.suppliesTypeId}`}>
          <Button
            key={`editT_${record.suppliesTypeId}`}
            type="primary"
            onClick={() => handleOpenEditSupplyType(record.suppliesTypeId)}
          >
            Chỉnh sửa
          </Button>
          <Button
            key={`removeT_${record.suppliesTypeId}`}
            danger
            type="primary"
            onClick={() => handleDeleteSupplyType(record.suppliesTypeId)}
          >
            Vô hiệu hóa
          </Button>
          <Button
            key={`addTypeC_${record.suppliesTypeId}`}
            type="primary"
            onClick={() => handleAddSupply(record.suppliesTypeId)}
          >
            Thêm mới thuốc
          </Button>
        </Space>
      ),
    },
  ];

  const suppliesColumns: ColumnsType<SupplyResponseModel> = [
    {
      title: "ID",
      dataIndex: "sId",
      key: "actionC_sId",
      sorter: (a, b) => a.sId - b.sId,
    },
    {
      title: "Name",
      dataIndex: "sName",
      key: "actionC_sName",
      sorter: (a, b) => a.sName.length - b.sName.length,
    },
    {
      title: "Cách dùng",
      dataIndex: "uses",
      key: "actionC_uses",
    },
    {
      title: "Ngày hết hạn",
      dataIndex: "exp",
      key: "actionC_exp",
      sorter: (a, b) => a.exp!.length - b.exp!.length,
      render: (date: any) => dayjs(date).format("DD/MM/YYYY HH:mm:ss"),
    },
    {
      title: "Nhà phân phối",
      dataIndex: "distributor",
      key: "actionC_distributor",
      sorter: (a, b) => a.distributor!.length - b.distributor!.length,
    },
    {
      title: "Số lượng tồn kho",
      dataIndex: "unitInStock",
      key: "actionC_unitInStock",
      sorter: (a, b) => a.unitInStock! - b.unitInStock!,
    },
    {
      title: "Giá",
      dataIndex: "price",
      key: "actionC_price",
      sorter: (a, b) => a.price - b.price,
    },
    {
      title: "",
      key: "actionC",
      render: (_, record) => (
        <Space size="middle" key={`spaceS_${record.sId}`}>
          <Button
            key={`editS_${record.sId}`}
            type="primary"
            onClick={() =>
              handleOpenEditSupply(record.sId, record.suppliesTypeId)
            }
          >
            Chỉnh sửa
          </Button>
          <Button
            key={`removeS_${record.sId}`}
            danger
            type="primary"
            onClick={() => handleDeleteSupply(record.sId)}
          >
            Vô hiệu hóa
          </Button>
        </Space>
      ),
    },
  ];

  //===================== Fetch Data =====================

  const fetchCategories = async () => {
    var response = await categoryService.getCategories();
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
    } else {
      console.log(response);
      setCates(response);
      return response;
    }
  };

  const fetchServiceTypes = async (cates: CategoryResponseModel[]) => {
    var response = await subService.getServicesType(0);
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
    } else {
      console.log(response);
      //map category name to service type
      response.map((item: ServiceTypeResponseModel) => {
        item.categoryName = cates.find(
          (cate) => cate.categoryId === item.categoryId
        )?.categoryName;
        item.categoryId = cates.find(
          (cate) => cate.categoryId === item.categoryId
        )?.categoryId;
      });
      setServiceTypes(response);
      return response;
    }
  };

  const fetchServices = async (serviceTypes: ServiceTypeResponseModel[]) => {
    var response = await subService.getServices(0);
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
    } else {
      console.log(response);
      //map service type name to service
      response.map((item: ServiceResponseModel) => {
        item.serviceTypeName = serviceTypes.find(
          (type) => type.serviceTypeId === item.serviceTypeId
        )?.serviceTypeName;
        item.serviceTypeId = serviceTypes.find(
          (type) => type.serviceTypeId === item.serviceTypeId
        )?.serviceTypeId;
      });
      setServices(response);
      return response;
    }
  };

  const fetchSupplyTypes = async () => {
    var response = await subService.getAllSupplyTypes();
    if (response === undefined) {
      message.error("Get Categories Failed", 2);
    } else {
      console.log(response);
      //map category name to service type
      setSupplyTypes(response);
      return response;
    }
  };

  const fetchSupplies = async (supplyTypes: SupplyTypeResponseModel[]) => {
    var response = await subService.getAllSupplies();
    if (response === undefined) {
      message.error("Get Supplie Failed", 2);
    } else {
      console.log(response);
      //map service type name to service
      response.map((item: SupplyResponseModel) => {
        item.suppliesTypeName = supplyTypes.find(
          (type) => type.suppliesTypeId === item.suppliesTypeId
        )?.suppliesTypeName;

        item.suppliesTypeId =
          supplyTypes.find(
            (type) => type.suppliesTypeId === item.suppliesTypeId
          )?.suppliesTypeId ?? 0;
      });
      setSupplies(response);
      return response;
    }
  };

  //==============Delete=================
  //show confirm dialog before delete
  const handleDeleteCategory = (id: number) => {
    Modal.confirm({
      title: "Xác nhận Vô hiệu hóa",
      content: "Bạn có chắc chắn muốn Vô hiệu hóa?",
      onOk: () => {
        removeCategory(id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };

  const handleDeleteServiceType = (id: number) => {
    Modal.confirm({
      title: "Xác nhận Vô hiệu hóa",
      content: "Bạn có chắc chắn muốn Vô hiệu hóa?",
      onOk: () => {
        removeServiceType(id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };

  const handleDeleteService = (id: number) => {
    Modal.confirm({
      title: "Xác nhận Vô hiệu hóa",
      content: "Bạn có chắc chắn muốn Vô hiệu hóa?",
      onOk: () => {
        removeService(id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };

  const handleDeleteSupplyType = (id: number) => {
    Modal.confirm({
      title: "Xác nhận Vô hiệu hóa",
      content: "Bạn có chắc chắn muốn Vô hiệu hóa?",
      onOk: () => {
        removeSupplyType(id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };

  const handleDeleteSupply = (id: number) => {
    Modal.confirm({
      title: "Xác nhận Vô hiệu hóa",
      content: "Bạn có chắc chắn muốn Vô hiệu hóa?",
      onOk: () => {
        removeSupply(id);
      },
      onCancel: () => {
        // Do nothing
      },
    });
  };
  //===========Delete API=============
  const removeCategory = async (id: number) => {
    var response = await categoryService.deleteCategories(id);
    if (response === 200) {
      message.success("Vô hiệu hóa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Vô hiệu hóa thất bại", 2);
    }
  };

  const removeServiceType = async (id: number) => {
    var response = await subService.deleteServiceType(id);
    if (response === 200) {
      message.success("Vô hiệu hóa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Vô hiệu hóa thất bại", 2);
    }
  };

  const removeService = async (id: number) => {
    var response = await subService.deleteService(id);
    if (response === 200) {
      message.success("Vô hiệu hóa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Vô hiệu hóa thất bại", 2);
    }
  };

  const removeSupplyType = async (id: number) => {
    var response = await subService.deleteSupplyType(id);
    if (response === 200) {
      message.success("Vô hiệu hóa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Vô hiệu hóa thất bại", 2);
    }
  };

  const removeSupply = async (id: number) => {
    var response = await subService.deleteSupply(id);
    if (response === 200) {
      message.success("Vô hiệu hóa thành công", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Vô hiệu hóa thất bại", 2);
    }
  };

  //=============================

  useEffect(() => {
    const fetchData = async () => {
      var cates = await fetchCategories();
      if (cates !== undefined) {
        var types = await fetchServiceTypes(cates);
        if (types !== undefined) {
          var services = await fetchServices(types);
          if (services !== undefined) {
            var supplyTypes = await fetchSupplyTypes();
            if (supplyTypes !== undefined) {
              var supplies = await fetchSupplies(supplyTypes);
            }
          }
        }
      }
    };
    fetchData();
  }, []);

  // Filter Search

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      {/*===================== Category =====================*/}
      {/* <h2>Khoa khám</h2>
      <Row>
        <Col span={24}></Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<CategoryResponseModel>
            rowKey={(record) => `idC_${record.categoryId}`}
            dataSource={cates}
            columns={cateColumns}
            pagination={{ pageSize: 5 }}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedCategoryId(record.categoryId), // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row> */}
      {/*===================== Service Type =====================*/}
      {/* <h2>Loại dịch vụ</h2>
      <Row>
        <Col span={24}></Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<ServiceTypeResponseModel>
            rowKey={(record) => `idT_${record.serviceTypeId}`}
            dataSource={serviceTypes}
            pagination={{ pageSize: 5 }}
            columns={typeColumns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedTypeId(record.serviceTypeId), // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row> */}
      {/*===================== Service =====================*/}
      {/* <h2>Dịch vụ</h2>
      <Row>
        <Col span={24}></Col>
      </Row>
      <div>
        <br />
      </div>
      <Row>
        <Col span={24}>
          <Table<ServiceResponseModel>
            rowKey={(record) => `idS_${record.serviceId}`}
            dataSource={services}
            pagination={{ pageSize: 5 }}
            columns={servicesColumns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => setSelectedServiceId(record.serviceId), // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row> */}

      {/*============== Supply type ==================*/}

      <Row
        gutter={10}
        style={{
          display: "flex",
          justifyContent: "flex-start",
          alignItems: "center",
        }}
      >
        <Col>
          <h2>Loại thuốc</h2>
        </Col>
        <Col>
          <Button type="primary" onClick={() => setOpenAddSupplyType(true)}>
            Thêm loại thuốc
          </Button>
        </Col>
      </Row>
      {/* <Row>
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
      </div> */}
      <Row>
        <Col span={24}>
          <Table<SupplyTypeResponseModel>
            rowKey={(record) => `idT_${record.suppliesTypeId}`}
            dataSource={supplyTypes}
            pagination={{ pageSize: 5 }}
            columns={supplyTypeColumns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => {
                //alert(record.suppliesTypeId);
                setSelectedSupplyTypeId(record.suppliesTypeId);
              }, // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row>

      {/*============== Supply ==================*/}
      <h2>Thuốc</h2>
      <Row>
        <Col span={24}>
          <Table<SupplyResponseModel>
            rowKey={(record) => `idT_${record.sId}`}
            dataSource={supplies}
            pagination={{ pageSize: 5 }}
            columns={suppliesColumns}
            style={{ width: "auto", minWidth: "400px" }}
            onRow={(record) => ({
              onClick: () => {
                //alert(record.sId);
                setSelectedSupplyId(record.sId);
              }, // set the selected id when a row is clicked
            })}
          />
        </Col>
      </Row>

      {/*======================== PopUp Modal ==========================*/}

      {/*=============== Edit category modal ========================*/}
      <Modal
        title="Chỉnh sửa khoa khám"
        key="editSubModal"
        onCancel={handleCancelCateEdit}
        open={editCateModalVisible}
        footer={[
          <Button
            key="submitF"
            form="editForm"
            type="primary"
            htmlType="submit"
          >
            Chỉnh sửa
          </Button>,
          <Button key="removeF" type="primary" onClick={handleCancelCateEdit}>
            Hủy
          </Button>,
        ]}
      >
        {/* Edit category form */}
        <Form
          id="editForm"
          form={editCateForm}
          layout="vertical"
          name="basic"
          onFinish={handleEditCategory}
        >
          <Form.Item<CategoryResponseModel> label="ID" name="categoryId">
            <Input placeholder="ID" disabled />
          </Form.Item>
          <Form.Item<CategoryResponseModel> label="Tên" name="categoryName">
            <Input placeholder="Nhập tên" />
          </Form.Item>
        </Form>
      </Modal>

      {/*=============== Add service type modal ========================*/}
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
        <ServiceTypeAddForm id={selectedCategoryId} />
      </Modal>

      {/*=============== Edit service type modal ========================*/}
      <Modal
        title="Chỉnh sửa loại dịch vụ"
        key="editTypeModal"
        onCancel={handleCancelTypeEdit}
        open={editTypeModalVisible}
        footer={[
          <Button
            key="submitTypeF"
            form="editTypeForm"
            type="primary"
            htmlType="submit"
          >
            Chỉnh sửa
          </Button>,
          <Button key="removeF" type="primary" onClick={handleCancelTypeEdit}>
            Hủy
          </Button>,
        ]}
      >
        {/*==== Edit service type form ===== */}
        <Form
          id="editTypeForm"
          form={editTypeForm}
          layout="vertical"
          name="basicT"
          onFinish={handleEditType}
        >
          <Form.Item<ServiceTypeResponseModel> label="ID" name="serviceTypeId">
            <Input placeholder="ID" disabled />
          </Form.Item>
          <Form.Item<ServiceTypeResponseModel>
            label="Tên"
            name="serviceTypeName"
          >
            <Input placeholder="Nhập tên" />
          </Form.Item>
        </Form>
      </Modal>

      {/*=============== Add service modal ========================*/}
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
        {/*====== Form Add Service ============*/}
        <ServiceAddForm id={selectedTypeId} />
      </Modal>

      {/*================== Edit service modal ===================*/}
      <Modal
        title="Chỉnh sửa dịch vụ"
        key="editServiceModal"
        onCancel={handleCancelServiceEdit}
        open={editServiceModalVisible}
        footer={[
          <Button
            key="submitTypeF"
            form="editServiceForm"
            type="primary"
            htmlType="submit"
          >
            Chỉnh sửa
          </Button>,
          <Button
            key="removeF"
            type="primary"
            onClick={handleCancelServiceEdit}
          >
            Hủy
          </Button>,
        ]}
      >
        {/*===== Edit service form =========*/}
        <Form
          id="editServiceForm"
          form={editServiceForm}
          layout="vertical"
          name="basic"
          onFinish={handleEditService}
        >
          <Form.Item<ServiceResponseModel> label="ID" name="serviceId">
            <Input placeholder="ID" disabled />
          </Form.Item>
          <Form.Item<ServiceResponseModel> label="Tên" name="serviceName">
            <Input placeholder="Nhập tên" />
          </Form.Item>
          <Form.Item<ServiceResponseModel> label="Tên" name="price">
            <InputNumber min={1} max={900000} placeholder="Giá dịch vụ"/>
          </Form.Item>
        </Form>
      </Modal>

      {/*=============== Edit supply type modal ========================*/}
      <Modal
        title="Chỉnh sửa loại thuốc"
        key="editSupplyTypeModal"
        onCancel={handleCancelSupplyTypeEdit}
        open={editSupplyTypeModalVisible}
        footer={[
          <Button
            key="submitSupplyTypeF"
            form="editSupplyTypeForm"
            type="primary"
            htmlType="submit"
          >
            Chỉnh sửa loại thuốc
          </Button>,
          <Button
            key="removeF"
            type="primary"
            onClick={handleCancelSupplyTypeEdit}
          >
            Hủy
          </Button>,
        ]}
      >
        {/*==== Edit supply type form ===== */}
        <Form
          id="editSupplyTypeForm"
          form={editSupplyTypeForm}
          layout="vertical"
          name="basicT"
          onFinish={handleUpdateSupplyType}
        >
          <Form.Item<SupplyTypeResponseModel> label="ID" name="suppliesTypeId">
            <Input placeholder="ID" disabled />
          </Form.Item>
          <Form.Item<SupplyTypeResponseModel>
            label="Tên"
            name="suppliesTypeName"
          >
            <Input placeholder="Nhập tên" />
          </Form.Item>
        </Form>
      </Modal>

      {/*=============== Add supply modal ========================*/}
      <Modal
        title="Thêm mới thuốc"
        open={openSupply}
        onCancel={() => setOpenSupply(false)}
        maskClosable={false}
        width="500px"
        footer={[
          <Button key="back" onClick={() => setOpenSupply(false)}>
            Hủy
          </Button>,
          <Button
            key="submit"
            type="primary"
            form="supplyAddForm"
            htmlType="submit"
          >
            Lưu
          </Button>,
        ]}
      >
        {/*====== Form Add Supply ============*/}
        <Form
          id="supplyAddForm"
          name="basic"
          layout="vertical"
          onFinish={handleInsertSupply}
          onFinishFailed={() => {
            message.error("Thêm mới thuốc thất bại", 2);
          }}
          autoComplete="off"
        >
          <Form.Item<SupplyAddModel> label="Tên thuốc" name="sName">
            <Input placeholder="Tên thuốc" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Cách dùng" name="uses">
            <Input placeholder="Cách dùng" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Ngày hết hạn" name="exp">
            <DatePicker format={"MM/DD/YYYY HH:mm:ss"} />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Nhà phân phối" name="distributor">
            <Input placeholder="Nhà phân phối" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Số lượng" name="unitInStock">
            <InputNumber min={1} max={10000000} placeholder="Số lượng" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Giá" name="price">
            <InputNumber min={1} max={100000000} placeholder="Giá" />
          </Form.Item>
        </Form>
      </Modal>

      {/*================== Edit supply modal ===================*/}
      <Modal
        title="Chỉnh sửa thuốc"
        key="editServiceModal"
        onCancel={handleCancelSupplyEdit}
        open={editSupplyModalVisible}
        footer={[
          <Button
            key="submitSupplyTypeF"
            form="editSupplyForm"
            type="primary"
            htmlType="submit"
          >
            Chỉnh sửa
          </Button>,
          <Button key="removeF" type="primary" onClick={handleCancelSupplyEdit}>
            Hủy
          </Button>,
        ]}
      >
        {/*===== Edit supply form =========*/}
        <Form
          id="editSupplyForm"
          form={editSupplyForm}
          layout="vertical"
          name="basic"
          onFinish={handleUpdateSupply}
        >
          <Form.Item<ServiceResponseModel> label="ID" name="serviceId">
            <Input placeholder="ID" disabled />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Tên thuốc" name="sName">
            <Input placeholder="Tên thuốc" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Cách dùng" name="uses">
            <Input placeholder="Cách dùng" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Ngày hết hạn" name="exp">
            <DatePicker format={"MM/DD/YYYY HH:mm:ss"} />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Nhà phân phối" name="distributor">
            <Input placeholder="Nhà phân phối" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Số lượng" name="unitInStock">
            <InputNumber min={1} max={1000000} placeholder="Số lượng" />
          </Form.Item>
          <Form.Item<SupplyAddModel> label="Giá" name="price">
            <InputNumber min={1} max={100000000} placeholder="Giá" />
          </Form.Item>
        </Form>
      </Modal>

      {/*================== Add supply type modal ===================*/}
      <Modal
        title="Thêm mới loại thuốc"
        open={openAddSupplyType}
        onCancel={() => setOpenAddSupplyType(false)}
        maskClosable={false}
        width="500px"
        footer={[
          <Button key="back" onClick={() => setOpenAddSupplyType(false)}>
            Hủy
          </Button>,
          <Button
            key="submit"
            type="primary"
            form="supplyTypeAddForm"
            htmlType="submit"
          >
            Lưu
          </Button>,
        ]}
      >
        {/*====== Form Add Supply Type ============*/}
        <Form
          id="supplyTypeAddForm"
          form={supplyTypeAddForm}
          name="basic"
          layout="vertical"
          onFinish={handleInsertSupplyType}
          onFinishFailed={() =>
            message.error("Thêm mới loại thuốc thất bại", 2)
          }
          autoComplete="off"
        >
          <Form.Item<SupplyTypeAddModel>
            label="Tên loại thuốc"
            name="suppliesTypeName"
          >
            <Input placeholder="Tên loại thuốc" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default CategoryTable;
