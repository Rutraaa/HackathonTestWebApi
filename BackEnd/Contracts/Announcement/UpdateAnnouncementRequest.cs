﻿using BackApi.DataTypes;

namespace BackEnd.Contracts.Announcement
{
    public class UpdateAnnouncementRequest
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}
