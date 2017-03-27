/*******************************************************************************
* 命名空间: SF.Entitys.Base
*
* 功 能： N/A
* 类 名： IUserLocation
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 12:16:36 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Entitys
{

    public interface IUserLocation
    {
        long Id { get; set; }
        long SiteId { get; set; }
        long UserId { get; set; }

        int CaptureCount { get; set; }
        string City { get; set; }
        string Continent { get; set; }
        string Country { get; set; }
        DateTime FirstCaptureUtc { get; set; }
        string HostName { get; set; }
        string IpAddress { get; set; }
        long IpAddressLong { get; set; }
        string Isp { get; set; }
        DateTime LastCaptureUtc { get; set; }

        //http://stackoverflow.com/questions/28068123/double-or-decimal-for-latitude-longitude-values-in-c-sharp
        //http://stackoverflow.com/questions/1440620/which-sql-server-data-type-best-represents-a-double-in-c

        double Latitude { get; set; }
        double Longitude { get; set; }

        string Region { get; set; }
        string TimeZone { get; set; }

    }
}