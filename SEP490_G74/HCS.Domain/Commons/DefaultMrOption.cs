using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Commons
{
    public static class DefaultMrOption
    {
        public static int DefaultCategoryId { get; } = 1; // Kham so bo
        public static int DefaultServiceTypeId { get; } = 1; // Kham tong quat
        public static int DefaultServiceId { get; } = 1; // Đo chỉ số tổng quát
        public static int DefaultDoctorId { get; } = 2; // Bsi Son
    }

    public static class DefaultText
    {
        public static string NOT_AVAILABLE { get; } = "N/A";
    }
}
