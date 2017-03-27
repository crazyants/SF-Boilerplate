/*******************************************************************************
* 命名空间: SF.Entitys.Base
*
* 功 能： N/A
* 类 名： UserLocation
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 12:16:08 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Entitys
{
    public class UserLocation : BaseEntity<long>, IUserLocation
    {
        public UserLocation()
        {
            
        }

        public long UserId { get; set; }
        public long SiteId { get; set; }

        private string ipAddress = string.Empty;
        public string IpAddress
        {
            get { return ipAddress ?? string.Empty; }
            set { ipAddress = value; }
        }

        public long IpAddressLong { get; set; } = 0;

        private string hostName = string.Empty;
        public string HostName
        {
            get { return hostName ?? string.Empty; }
            set { hostName = value; }
        }

        public double Longitude { get; set; } = 0;
        public double Latitude { get; set; } = 0;

        private string isp = string.Empty;
        public string Isp
        {
            get { return isp ?? string.Empty; }
            set { isp = value; }
        }

        private string continent = string.Empty;
        public string Continent
        {
            get { return continent ?? string.Empty; }
            set { continent = value; }
        }

        private string country = string.Empty;
        public string Country
        {
            get { return country ?? string.Empty; }
            set { country = value; }
        }

        private string region = string.Empty;
        public string Region
        {
            get { return region ?? string.Empty; }
            set { region = value; }
        }

        private string city = string.Empty;
        public string City
        {
            get { return city ?? string.Empty; }
            set { city = value; }
        }

        private string timeZone = string.Empty;
        public string TimeZone
        {
            get { return timeZone ?? string.Empty; }
            set { timeZone = value; }
        }

        public int CaptureCount { get; set; } = 0;
        public DateTime FirstCaptureUtc { get; set; } = DateTime.UtcNow;
        public DateTime LastCaptureUtc { get; set; } = DateTime.UtcNow;

        public static UserLocation FromIUserLocation(IUserLocation i)
        {
            UserLocation l = new UserLocation();

            l.CaptureCount = i.CaptureCount;
            l.City = i.City;
            l.Continent = i.Continent;
            l.Country = i.Country;
            l.FirstCaptureUtc = i.FirstCaptureUtc;
            l.HostName = i.HostName;
            l.IpAddress = i.IpAddress;
            l.IpAddressLong = i.IpAddressLong;
            l.LastCaptureUtc = i.LastCaptureUtc;
            l.Latitude = i.Latitude;
            l.Longitude = i.Longitude;
            l.Region = i.Region;
            l.Id = i.Id;
            l.SiteId = i.SiteId;
            l.TimeZone = i.TimeZone;
            l.UserId = i.UserId;

            return l;
        }


    }
}
