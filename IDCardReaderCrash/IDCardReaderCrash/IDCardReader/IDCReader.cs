using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SSCDC.IDCardReader
{
    public class IDCReader
    {
        public delegate void IDCReaderStatesChanged(string message);
        public event IDCReaderStatesChanged idcReaderStatesChangedEvent;

        public delegate void IDCReaderReadSuccess(IDCardInfo iDCardInfo);
        public event IDCReaderReadSuccess idcReaderReadSuccess;

        private int reInt = 0;
        private Thread Processingthread;
        private bool IsWorking { get; set; } //表明处理线程是否正在工作
        private object lockIsWorking = new object();//对IsWorking的同步对象


        //private static AutoResetEvent readEvent = new AutoResetEvent(false);

        public void startAsynRead()
        {
            Processingthread = new Thread(run);
            Processingthread.IsBackground = true;
            Processingthread.Start();
        }

        public void stopAsynRead()
        {
            lock (lockIsWorking)
            {
                IsWorking = false;
            }
        }

        private void run()
        {
            IsWorking = true;
            try
            {
                while (IsWorking)
                {
                    reInt = IDC_Authenticate();
                    if (reInt != 1)
                    {
                        if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("需要重新放卡"); }
                        continue;
                    }
                    IDCardInfo iDCardInfo = read();
                    if (iDCardInfo != null)
                    {
                        if (idcReaderReadSuccess != null) { idcReaderReadSuccess(iDCardInfo); }
                    }
                    System.Threading.Thread.Sleep(1111);
                    //readEvent.WaitOne();
                }
            }
            finally
            {
                lock (lockIsWorking)
                {
                    IsWorking = false;
                }

            }
        }

        public IDCardInfo read()
        {
            reInt = IDC_Authenticate();
            if (reInt != 1)
            {
                if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("需要重新放卡"); }
                MessageBox.Show("请重新放置身份证", "提示");
                return null;
            }
            IDCardInfo info = IDC_ReadIDC();
            if (info != null)
            {
                if (idcReaderReadSuccess != null) { idcReaderReadSuccess(info); }
            }
            return info;
            //return IDC_ReadIDCPhoto();
        }

        public IDCardInfo readLess()
        {
            reInt = IDC_Authenticate();
            if (reInt != 1)
            {
                if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("需要重新放卡"); }
                MessageBox.Show("请重新放置身份证", "提示");
                return null;
            }
            IDCardInfo info = IDC_ReadIDCLess();
            if (info != null)
            {
                if (idcReaderReadSuccess != null) { idcReaderReadSuccess(info); }
            }
            return info;
            //return IDC_ReadIDCPhoto();
        }


        public bool open()
        {
            //检查DLL支持文件
            if (!IDC_CheckDll())
            {
                if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("DLL文件丢失"); }
                return false;
            }

            //初始化接口
            reInt = IDC_InitComm();
            if (reInt != 1)
            {
                if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("未连接身份证读卡器"); }
                MessageBox.Show("未连接身份证读卡器", "提示");
                return false;
            }
            if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("已打开"); }
            //readEvent.Set();
            return true;
        }

        public bool close()
        {
            //检查DLL支持文件
            if (!IDC_CheckDll())
            {
                if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("未找到DLL支持文件"); }
                return false;
            }
            reInt = IDC_CloseComm();
            if (reInt != 1)
            {
                if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("关闭异常"); }
                return false;
            }
            if (idcReaderStatesChangedEvent != null) { idcReaderStatesChangedEvent("已关闭"); }
            return true;
        }

        #region 硬件接口
        /// <summary>
        /// 一定要注意堆栈的调用约定
        /// Visual C++ 代码的默认调用约定是C调用约定（__cdecl）
        /// 而不是标准调用约定（__stdcall）或Pascal调用约定（__pascal）
        /// 所以在DllImport中，CallingConvention参数一定要设置成CallingConvention.Cdecl。
        /// 当然，我们也可以通过修改Dll.dll的调用约定，如
        /// int WINAPI  add(int a,int b)  
        /// 将add函数的调用约定设置为WINAPI，也就是标准调用约定，
        /// 这样，在WPF中引入时，DllImport的CallingConvention参数就可以省略不写，因为默认是标准调用约定
        /// [DllImport("Dll.dll", EntryPoint = "add"]  
        /// </summary>
        /// <param name="iPort"></param>
        /// <returns></returns>

        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int InitComm(int iPort);//初始化

        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int CloseComm();//关闭端口

        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Authenticate();//卡认证

        //读取数据
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadBaseInfos(
            StringBuilder Name,
            StringBuilder Gender,
            StringBuilder Folk,
            StringBuilder BirthDay,
            StringBuilder Code,
            StringBuilder Address,
            StringBuilder Agency,
            StringBuilder ExpireStart,
            StringBuilder ExpireEnd);

        //读取数据
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadBaseInfosPhoto(
            StringBuilder Name,
            StringBuilder Gender,
            StringBuilder Folk,
            StringBuilder BirthDay,
            StringBuilder Code,
            StringBuilder Address,
            StringBuilder Agency,
            StringBuilder ExpireStart,
            StringBuilder ExpireEnd,
            string directory);

        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int HID_BeepLED(bool BeepON, bool LEDON, int duration); //蜂鸣器
        #endregion 硬件接口


        #region 公用方法

        public int IDC_InitComm()
        {
            return InitComm(1001);
        }

        public int IDC_CloseComm()
        {
            return CloseComm();
        }

        public int IDC_Authenticate()
        {
            int auth = Authenticate();
            if (auth == 1)
            {
                HID_BeepLED(false, true, 10);
            }
            else
            {
                HID_BeepLED(false, true, 500);
            }
            return auth;
        }

        public IDCardInfo IDC_ReadIDC()
        {
            StringBuilder Name = new StringBuilder(32);
            StringBuilder Gender = new StringBuilder(4);
            StringBuilder Folk = new StringBuilder(16);
            StringBuilder BirthDay = new StringBuilder(16);
            StringBuilder Code = new StringBuilder(32);
            StringBuilder Address = new StringBuilder(128);
            StringBuilder Agency = new StringBuilder(32);
            StringBuilder ExpireStart = new StringBuilder(16);
            StringBuilder ExpireEnd = new StringBuilder(16);
            string _directory = System.Environment.CurrentDirectory;
            string _bmp_photo_old = _directory + "\\photo.bmp";
            string _photo_old = _directory + "\\photo.jpg";
            string _front_old = _directory + "\\1.jpg";
            string _back_old = _directory + "\\2.jpg";

            if (File.Exists(_bmp_photo_old)) { File.Delete(_bmp_photo_old); }
            if (File.Exists(_photo_old)) { File.Delete(_photo_old); }
            if (File.Exists(_front_old)) { File.Delete(_front_old); }
            if (File.Exists(_back_old)) { File.Delete(_back_old); }


            int intReadBaseInfosRet = ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd);
            if (intReadBaseInfosRet != 1)
            {
                return null;
            }


            if (File.Exists(_bmp_photo_old) && !File.Exists(_photo_old))
            {
                Image image1 = Image.FromFile(_bmp_photo_old);
                image1.Save(_photo_old, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            string _photo = _directory + "\\" + Code.ToString() + "_id.jpg";
            string _front = _directory + "\\" + Code.ToString() + "_1.jpg";
            string _back = _directory + "\\" + Code.ToString() + "_2.jpg";

            if (File.Exists(_photo)) { File.Delete(_photo); }
            if (File.Exists(_front)) { File.Delete(_front); }
            if (File.Exists(_back)) { File.Delete(_back); }

            if (File.Exists(_photo_old)) File.Move(_photo_old, _photo);
            if (File.Exists(_front_old)) File.Move(_front_old, _front);
            if (File.Exists(_back_old)) File.Move(_back_old, _back);

            IDCardInfo info = new IDCardInfo
            {
                Name = Name.ToString().Trim(),
                Gender = Gender.ToString().Trim(),
                Folk = Folk.ToString().Trim(),
                BirthDay = BirthDay.ToString().Trim(),
                Code = Code.ToString().Trim(),
                Address = Address.ToString().Trim(),
                Agency = Agency.ToString().Trim(),
                ExpireStart = ExpireStart.ToString().Trim(),
                ExpireEnd = ExpireEnd.ToString().Trim(),
                directory = _directory.ToString().Trim(),
                photo = _photo,
                front = _front,
                back = _back
            };

            return info;
        }

        public IDCardInfo IDC_ReadIDCLess()
        {
            //StringBuilder Name = new StringBuilder(31);
            //StringBuilder Gender = new StringBuilder(3);
            //StringBuilder Folk = new StringBuilder(10);
            //StringBuilder BirthDay = new StringBuilder(9);
            //StringBuilder Code = new StringBuilder(19);
            //StringBuilder Address = new StringBuilder(71);
            //StringBuilder Agency = new StringBuilder(31);
            //StringBuilder ExpireStart = new StringBuilder(9);
            //StringBuilder ExpireEnd = new StringBuilder(9);
            StringBuilder Name = new StringBuilder(32);
            StringBuilder Gender = new StringBuilder(4);
            StringBuilder Folk = new StringBuilder(16);
            StringBuilder BirthDay = new StringBuilder(16);
            StringBuilder Code = new StringBuilder(32);
            StringBuilder Address = new StringBuilder(128);
            StringBuilder Agency = new StringBuilder(32);
            StringBuilder ExpireStart = new StringBuilder(16);
            StringBuilder ExpireEnd = new StringBuilder(16);
            string _directory = System.Environment.CurrentDirectory;
            string _bmp_photo_old = _directory + "\\photo.bmp";
            string _photo_old = _directory + "\\photo.jpg";
            string _front_old = _directory + "\\1.jpg";
            string _back_old = _directory + "\\2.jpg";
            if (File.Exists(_bmp_photo_old)) { File.Delete(_bmp_photo_old); }
            if (File.Exists(_photo_old)) { File.Delete(_photo_old); }
            if (File.Exists(_front_old)) { File.Delete(_front_old); }
            if (File.Exists(_back_old)) { File.Delete(_back_old); }

            int intReadBaseInfosRet = ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd);
            if (intReadBaseInfosRet != 1)
            {
                return null;
            }
            if (File.Exists(_bmp_photo_old)) { File.Delete(_bmp_photo_old); }
            if (File.Exists(_photo_old)) { File.Delete(_photo_old); }
            if (File.Exists(_front_old)) { File.Delete(_front_old); }
            if (File.Exists(_back_old)) { File.Delete(_back_old); }
            IDCardInfo info = new IDCardInfo
            {
                Name = Name.ToString().Trim(),
                Gender = Gender.ToString().Trim(),
                Folk = Folk.ToString().Trim(),
                BirthDay = BirthDay.ToString().Trim(),
                Code = Code.ToString().Trim(),
                Address = Address.ToString().Trim(),
                Agency = Agency.ToString().Trim(),
                ExpireStart = ExpireStart.ToString().Trim(),
                ExpireEnd = ExpireEnd.ToString().Trim(),
            };
            return info;
        }

        public IDCardInfo IDC_ReadIDCPhoto()
        {
            try
            {
                //Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string directory = System.Environment.CurrentDirectory;
                directory = System.Environment.GetEnvironmentVariable("TEMP");
                return IDC_ReadIDCPhoto(directory);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }


        public IDCardInfo IDC_ReadIDCPhoto(string directory)
        {
            StringBuilder Name = new StringBuilder(31);
            StringBuilder Gender = new StringBuilder(3);
            StringBuilder Folk = new StringBuilder(10);
            StringBuilder BirthDay = new StringBuilder(9);
            StringBuilder Code = new StringBuilder(19);
            StringBuilder Address = new StringBuilder(71);
            StringBuilder Agency = new StringBuilder(31);
            StringBuilder ExpireStart = new StringBuilder(9);
            StringBuilder ExpireEnd = new StringBuilder(9);
            string _directory = directory;
            string _photo = directory + "\\photo.jpg";
            string _front = directory + "\\1.jpg";
            string _back = directory + "\\2.jpg";

            FileInfo fileInfo = new FileInfo(_photo);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            } fileInfo = new FileInfo(_front);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            } fileInfo = new FileInfo(_back);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            int intReadBaseInfosRet = ReadBaseInfosPhoto(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd, _directory);

            if (intReadBaseInfosRet != 1)
            {
                return null;
            }

            IDCardInfo info = new IDCardInfo
            {
                Name = Name.ToString().Trim(),
                Gender = Gender.ToString().Trim(),
                Folk = Folk.ToString().Trim(),
                BirthDay = BirthDay.ToString().Trim(),
                Code = Code.ToString().Trim(),
                Address = Address.ToString().Trim(),
                Agency = Agency.ToString().Trim(),
                ExpireStart = ExpireStart.ToString().Trim(),
                ExpireEnd = ExpireEnd.ToString().Trim(),
                directory = _directory.ToString().Trim(),
                photo = _photo,
                front = _front,
                back = _back
            };

            return info;
        }


        public bool IDC_CheckDll()
        {
            if (!System.IO.File.Exists("Dewlt.dll"))
            {
                return false;
            }

            if (!System.IO.File.Exists("sdtapi.dll"))
            {
                return false;
            }

            if (!System.IO.File.Exists("SavePhoto.dll"))
            {
                return false;
            }

            return true;
        }
        #endregion 公用方法

    }
}
