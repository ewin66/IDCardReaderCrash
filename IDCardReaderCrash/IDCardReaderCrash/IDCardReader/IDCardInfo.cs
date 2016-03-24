using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCDC.IDCardReader
{
    public class IDCardInfo
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Folk { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 身份号码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 住址；户口所在地
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 签发机关
        /// </summary>
        public string Agency { get; set; }

        /// <summary>
        /// 有效期起
        /// </summary>
        public string ExpireStart { get; set; }

        /// <summary>
        /// 有效期止
        /// </summary>
        public string ExpireEnd { get; set; }

        /// <summary>
        /// 图片目录
        /// </summary>
        public string directory { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        public string photo { get; set; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        public string front { get; set; }

        /// <summary>
        /// 身份证反面
        /// </summary>
        public string back { get; set; }
    }
}
