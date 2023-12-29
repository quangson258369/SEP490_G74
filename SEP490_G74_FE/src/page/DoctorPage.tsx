import { Layout, Menu, Button } from "antd";
import Sider from "antd/es/layout/Sider";
import { Header, Content } from "antd/es/layout/layout";
import SubMenu from "antd/es/menu/SubMenu";
import { useState } from "react";
import { Link } from "react-router-dom";

interface ParentCompProps {
    childComp?: React.ReactNode;
}

const DoctorPage: React.FC<ParentCompProps> = (props: any) => {
    const [collapsed, setCollapsed] = useState(false);
    const { childComp } = props;
    return (
        <div style={{}}>
            <Layout>
                <Sider trigger={null} collapsible collapsed={collapsed} style={{ background: '#146C94', color: 'white', height: '100vh' }}>
                    <div className="demo-logo-vertical" />
                    <Menu
                        theme="light"
                        mode="inline"
                        defaultSelectedKeys={['1']}
                        style={{ background: '#146C94', color: 'white' }}
                    >
                        <Menu.Item key="1" >
                            <Link to={"/"}>Trang chủ</Link>
                        </Menu.Item>
                        <Menu.Item key="3" >
                            <Link to={"/"}>Hóa đơn thuốc</Link>
                        </Menu.Item>
                        <Menu.Item key="4" >
                            <Link to={"/"}>Hóa đơn dịch vụ</Link>
                        </Menu.Item>
                        <Menu.Item key="5" >
                            <Link to={"/"}>Nhân sự</Link>
                        </Menu.Item>
                        <Menu.Item key="6" >
                            <Link to={"/"}>Hồ sơ bệnh án</Link>
                        </Menu.Item>
                        <Menu.Item key="6" >
                            <Link to={"/"}>Hồ sơ cá nhân</Link>
                        </Menu.Item>

                    </Menu>
                </Sider>
                <Layout>
                    <Header style={{ padding: 0, backgroundColor: 'white' }}>
                        <Button
                            type="text"
                            onClick={() => setCollapsed(!collapsed)}
                            style={{
                                fontSize: '16px',
                                width: 64,
                                height: 64,
                            }}
                        />

                    </Header>
                    <Content
                        style={{
                            margin: '24px 16px',
                            padding: 24,
                            minHeight: 280,

                        }}
                    >
                        <div>{childComp}</div>
                    </Content>
                </Layout>
            </Layout>
        </div>
    )
}

export default DoctorPage;