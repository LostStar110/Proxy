using DCVLicenseServer.Application;
using DCVLicenseServer.Application.APILicenses.Dto;
using DCVLicenseServer.Application.ProLicense;
using MKUtils.Security;
using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using TinyPinyin.Core;

namespace DCVLicenseServer.Web.Core.GrantSystem
{
    public class GrantManager
    {
        public static GrantManager Inst { get; } = new GrantManager();

        private readonly string rsaPublicKey= @"AwEAAZFKNZyVUjuLGaj6/aQ3qlMy5aTiiLqF6IHYTg6ChP5WjgKO+Gm6ilOIVSOKfGpEY1TP6V3dWG0PMC/GzXhLpBDtVEJGXevwigxw1I/lvfvGP8zI8MtGcfq0TGpZtr63cSLcwFwNS5o1C53zsYs/ocmutQqaTuSyUeqGj+6QTomv";

        private readonly string rsaPrivateKey = @"gEc2evmhXPmPcAfZ4hmsKSF3iITdfkwCV5jRJ01IXxx0201Yu1zDUIqp/6UYe2vw0kkfUVCvLejisWkxhcOJO8RNI9Iq1uqdgn+5J3NwNu6XDirCsWfn0VKW9ayYjHoEfQdTpedSfyi7NQyJ7Liu6buG0ZCKW2CBmIzh4MhwAv9xkUo1nJVSO4sZqPr9pDeqUzLlpOKIuoXogdhODoKE/laOAo74abqKU4hVI4p8akRjVM/pXd1YbQ8wL8bNeEukEO1UQkZd6/CKDHDUj+W9+8Y/zMjwy0Zx+rRMalm2vrdxItzAXA1LmjULnfOxiz+hya61CppO5LJR6oaP7pBOia8=";

        private readonly string desKey = "IDCV2019";

        //private readonly string garntPwd = "oWeT9osL1t";
        //public readonly string desKey = "1l*4&o_2";
        //public readonly string grantPwd = "F73A9FECE124135C516E612CCB313AA6";

        /// <summary>
        /// 创建产品授权
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public PLFileResult CreatePLGrantFile(CreatePLDto input,DateTime time)
        {
            var projectName = PinyinHelper.GetPinyinInitials(input.Project);

            var systemGrantFileName = GetPLGrantFileName("1", projectName, input.MachineCode);

            var systemGrantFileContent = CreatePLGrantContent(input);

            var timeGrantFileName = GetPLGrantFileName("2", projectName, input.MachineCode);

            var timeGrantFileContent = CreatePLTimeGrantContent(input);

            var timeNow = time.ToString("yyyy-MM-dd-HH-mm-ss");

            var dirPath = "wwwroot/GrantFiles/" + projectName + "/Product/" + timeNow;

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }


            MKUtils.IO.FileWriter.WriteFile(dirPath + "/" + systemGrantFileName, systemGrantFileContent);

            MKUtils.IO.FileWriter.WriteFile(dirPath + "/" + timeGrantFileName, timeGrantFileContent);

            var filePath = "wwwroot/GrantFiles/" + projectName + "/Product/" + projectName + timeNow + "授权文件.zip";

            ZipFile.CreateFromDirectory(dirPath, filePath);


            PLFileResult result = new PLFileResult();

            result.SysFileName = systemGrantFileName;
            result.SysFileContent = systemGrantFileContent;
            result.TimeFileName = timeGrantFileName;
            result.TimeFileContent = timeGrantFileContent;
            result.ZipFilePath = filePath.Replace("wwwroot/","");


            return result;
        }

        /// <summary>
        /// 创建API授权
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public ALFileResult CreateALGrantFile(CreateALDto input, DateTime time)
        {
            var projectName = PinyinHelper.GetPinyinInitials(input.Project);

            var GrantFileName = GetALGrantFileName( projectName, input.AppId);

            var GrantFileContent = CreateALGrantContent(input);


            var timeNow = time.ToString("yyyy-MM-dd-HH-mm-ss");

            var dirPath = "wwwroot/GrantFiles/" + projectName + "/API/" + timeNow;

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }


            MKUtils.IO.FileWriter.WriteFile(dirPath + "/" + GrantFileName, GrantFileContent);


            var filePath = "wwwroot/GrantFiles/" + projectName + "/API/" + projectName + timeNow + "授权文件.zip";

            ZipFile.CreateFromDirectory(dirPath, filePath);


            ALFileResult result = new ALFileResult();

            result.FileName = GrantFileName;
            result.FileContent = GrantFileContent;
            result.ZipFilePath = filePath.Replace("wwwroot/", "");


            return result;
        }


        private string GetALGrantFileName(string projectName,string appId)
        {
            string s = string.Concat(appId, projectName);
            return MD5Util.MD5Encrypt(@s, Encoding.ASCII);
        }

        //生成产品授权文件名
        private string GetPLGrantFileName(string type, string projectName,string machineCode)
        {
            string s = string.Concat(type, projectName, machineCode);
            return MD5Util.MD5Encrypt(@s, Encoding.ASCII);
        }

        private string CreatePLGrantContent(CreatePLDto input)
        {
            string w = string.Concat(PinyinHelper.GetPinyinInitials(input.Project), "_", input.MachineCode, "_",  input.DeviceNumber, "_", input.StartTime ,"_",(input.EndTime - input.StartTime));

            string res = RSAUtil.EncryptString(w, rsaPublicKey);
            return res;
        }

        private string CreatePLTimeGrantContent(CreatePLDto input)
        {
            string w = string.Concat(input.StartTime, (input.EndTime - input.StartTime), input.MachineCode);
            return RSAUtil.EncryptString(w, rsaPublicKey);
        }

        /// <summary>
        /// 创建产品授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CreateALGrantContent(CreateALDto input)
        {
            string w = string.Concat(PinyinHelper.GetPinyinInitials(input.Project), "_", input.AppId, "_", input.StartTime, "_",input.EndTime,"_", JsonConvert.SerializeObject(input.APIList));

            string res = DESUtil.EncryptString(w, desKey);

            return res;
        }


    }


   
}
