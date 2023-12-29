import { InfoCircleOutlined } from '@ant-design/icons';
import { Button, Form, Input, Radio, Tag, DatePicker, Dropdown, message } from 'antd';
import type { DatePickerProps, MenuProps } from 'antd';
import { PatientProps } from '../Models/MedicalRecordModel';
import dayjs from 'dayjs';
import type { RadioChangeEvent } from 'antd';
import { useState } from 'react';
import TextArea from 'antd/es/input/TextArea';
import { DownOutlined, UserOutlined } from '@ant-design/icons';

const MedicalRecordAddForm = ({ patientId }: PatientProps) => {

    const [gender, setGender] = useState<number>(1)

    const today = dayjs()

    const onFinish = (values: any) => {
        console.log(values)
        alert(values + "\n" + patientId)
    }

    const onFinishFailed = () => {
        alert("Create MR Failed")
    }

    const handleDoBOnchange = (values: any) => {
        alert(values)
    }

    const handleChangeGender = (values: any) => {
        setGender(values.target.value)
    }


    const handleMenuClick: MenuProps['onClick'] = (e : any) => {
        message.info('Mã số khoa: ' + e.key);
    };

    const items: MenuProps['items'] = [
        {
            label: 'Khoa nội',
            key: '1',
            icon: <UserOutlined />,
        },
        {
            label: 'Khoa ngoại',
            key: '2',
            icon: <UserOutlined />,
        },
        {
            label: 'Khoa tim mạch',
            key: '3',
            icon: <UserOutlined />,
        },
        {
            label: 'Khoa thần kinh',
            key: '4',
            icon: <UserOutlined />,
        },
    ];

    const menuProps = {
        items,
        onClick: handleMenuClick,
    };

    const handleButtonClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        message.info('Click on left button.');
        console.log('click left button', e);
      };

    return (
        <Form
            name='basic'
            layout="vertical"
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete='off'
        >
            <Form.Item label="Mã hồ sơ">
                <Input value={15} disabled />
            </Form.Item>
            <Form.Item label="Mã bệnh nhân">
                <Input value={patientId} disabled />
            </Form.Item>
            <Form.Item label="Họ và tên">
                <Input placeholder="Họ và tên" value={"Nguyễn A"} disabled />
            </Form.Item>
            <Form.Item label="Ngày sinh">
                <DatePicker format={"MM/DD/YYYY HH:mm:ss"} defaultValue={today} onChange={handleDoBOnchange} />
            </Form.Item>
            <Form.Item label="Giới tính">
                <Radio.Group onChange={handleChangeGender} value={gender}>
                    <Radio value={1}>Male</Radio>
                    <Radio value={2}>Female</Radio>
                </Radio.Group>
            </Form.Item>
            <Form.Item label="Số điện thoại">
                <Input placeholder="0983872xxx" />
            </Form.Item>
            <Form.Item label="Địa chỉ">
                <Input placeholder="Địa chỉ" />
            </Form.Item>
            <Form.Item label="Chiều cao">
                <Input placeholder="170" />
            </Form.Item>
            <Form.Item label="Cân nặng">
                <Input placeholder="55" />
            </Form.Item>
            <Form.Item label="Nhóm máu">
                <Input placeholder="A" />
            </Form.Item>
            <Form.Item label="Huyết áp">
                <Input placeholder="110" />
            </Form.Item>
            <Form.Item label="Ngày chỉnh sửa">
                <DatePicker disabled format={"MM/DD/YYYY HH:mm:ss"} defaultValue={today} />
            </Form.Item>
            <Form.Item label="Lí do khám">
                <TextArea placeholder="Lí do khám" />
            </Form.Item>
            <div>
                Khoa khám
                <div>
                    <Dropdown.Button menu={menuProps} onClick={handleButtonClick}>
                        Chọn khoa khám
                    </Dropdown.Button>
                </div>
            </div>
            <Form.Item>
                <Button type="primary" htmlType="submit">Tạo mới</Button>
            </Form.Item>
        </Form>
    );
};

export default MedicalRecordAddForm