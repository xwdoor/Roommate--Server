using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.model
{
    public class ResponseJson
    {
        /// <summary>
        /// 若code为0，则属性error无效
        /// </summary>
        public int Code { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
    }
}