﻿namespace Serdiuk.BookShop.Domain.Models.Identity.DTO
{
    public class RefreshRequestDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
