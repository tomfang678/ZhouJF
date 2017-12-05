﻿using Newtonsoft.Json;
using SmartFast.Plugin.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartFast.BaseFrame.Utility;
using SmartFast.Plugin.Log;

namespace YHPT.Management.WebUI
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// </summary>
    public class FileUpload : UploadHandler
    {
        //返回给UploadyFile的Json结果
        public override string GetResult(string localFileName, string uploadFilePath, string err)
        {
            var result = new
            {
                msg = new
                {
                    localname = localFileName,
                    url = uploadFilePath.Substring(uploadFilePath.IndexOf("\\upload", StringComparison.OrdinalIgnoreCase)).Replace("\\", "/")
                },
                err = err
            };
            return JsonConvert.SerializeObject(result);
        }

        //即时生成如201323023_s.jpg的缩略图
        public override void OnUploaded(HttpContext context, string filePath)
        {
            try
            {
                var ext = filePath.Substring(filePath.LastIndexOf('.') + 1).ToLower();
                if (!ImageExt.Contains(ext))
                    return;

                if (string.IsNullOrEmpty(context.Request["thumbs"]))
                {
                    //this.MakeThumbnail(filePath, "s", context.Request["thumbwidth"].ToInt(85), context.Request["thumbheight"].ToInt(85), string.IsNullOrEmpty(context.Request["mode"]) ? "H" : context.Request["mode"]);
                    this.MakeThumbnail(filePath, "s", context.Request["thumbwidth"].ToInt(500), context.Request["thumbheight"].ToInt(280), string.IsNullOrEmpty(context.Request["mode"]) ? "H" : context.Request["mode"]);
                }
                else
                {
                    var thumbs = context.Request["thumbs"];
                    foreach (var thumb in thumbs.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var thumbparts = thumb.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        this.MakeThumbnail(filePath, thumbparts[0], thumbparts[1].ToInt(), thumbparts[2].ToInt(), thumbparts[3]);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(LoggerType.ServiceExceptionLog, "FileUpload.ashx下的OnUploaded()出现异常", ex);
            }
        }

        private void MakeThumbnail(string filePath, string suffix, int width, int height, string mode)
        {
            string fileExt = filePath.Substring(filePath.LastIndexOf('.'));
            string fileHead = filePath.Substring(0, filePath.LastIndexOf('.'));

            var thumbPath = string.Format("{0}_{1}{2}", fileHead, suffix, fileExt); ;
            ThumbnailHelper.MakeThumbnail(filePath, thumbPath, width, height, mode, false);
        }
    }
}