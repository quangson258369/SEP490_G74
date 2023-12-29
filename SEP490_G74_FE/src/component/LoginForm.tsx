import { Form, Input, Checkbox, Button } from "antd";
import { FieldType, JWTTokenModel, UserLogin } from "../Models/AuthModel";
import { authService } from "../Services/AuthService";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { AuthContext, User } from "../ContextProvider/AuthContext";
import { useContext } from "react";
import Roles from "../Enums/Enums";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const LoginForm = () => {
    const { setAuthenticated } = useContext(AuthContext)
    const navigate = useNavigate()

    const authorizedNavigate = (role: string) => {
        switch (role) {
            case Roles.Admin: {
                navigate("/admin/patients")
                break;
            }
            case Roles.Doctor: {
                navigate("/doctor/patients")
                break;
            }
            case Roles.Nurse: {
                navigate("/nurse/patients")
                break;
            }
            case Roles.Cashier: {
                navigate("/nurse/patients")
                break;
            }
            default: {
                navigate("/error")
                break;
            }
        }
    }

    const onFinish = async (values: UserLogin) => {
        var token = await authService.login(values)

        if (token !== undefined) {
            localStorage.setItem("token", token)

            var jwtUser: JWTTokenModel = jwtDecode(token)
            let newUser: User = {
                role: jwtUser.role
            }

            setAuthenticated(newUser)
            toast.success("Login success, welcome " + newUser.role)
            authorizedNavigate(newUser.role)

        } else {
            toast.error("Login failed")
            return
        }
    };

    const onFinishFailed = (errorInfo: any) => {
        console.log('Failed:', errorInfo);
    };

    return (
        <div>
            <h1 style={{ paddingLeft: '85px', fontSize: '45px' }}>
                Heath Care System
            </h1>
            <Form
                name="basic"
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 16 }}
                style={{ maxWidth: 600 }}
                initialValues={{ remember: true }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
                size="large"
            >
                <Form.Item<FieldType>
                    label={<p style={{ fontWeight: 'bold', fontSize: '20px' }}> Email</p>}
                    name="email"
                    rules={[{ required: true, message: 'Please input your email!' }, { type: 'email', message: 'The input is not valid E-mail!' },]}
                >
                    <Input style={{ width: "400px", borderRadius: '30px' }} />
                </Form.Item>

                <Form.Item<FieldType>
                    label={<p style={{ fontWeight: 'bold', fontSize: '20px' }}> Password</p>}
                    name="password"
                    rules={[{ required: true, message: 'Please input your password!' }]}
                >
                    <Input.Password style={{ width: "400px", borderRadius: '30px' }} />
                </Form.Item>

                <Form.Item<FieldType>
                    name="remember"
                    valuePropName="checked"
                    wrapperCol={{ offset: 8, span: 16 }}
                >
                    <Checkbox>Remember me</Checkbox>
                </Form.Item>

                <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
                    <Button type="primary" htmlType="submit">
                        Submit
                    </Button>
                </Form.Item>
            </Form>
        </div>
    )
}

export default LoginForm