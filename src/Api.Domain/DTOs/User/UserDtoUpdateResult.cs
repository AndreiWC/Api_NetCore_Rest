using System;

namespace Api.Domain.DTOs.User
{
    //Dto para realizar update na UserEntity
    public class UserDtoUpdateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Emai { get; set; }
        public DateTime UpdateeAt { get; set; }
    }
}