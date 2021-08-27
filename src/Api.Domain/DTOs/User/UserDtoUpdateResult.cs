using System;

namespace Api.Domain.DTOs.User
{
    //Dto para realizar update na UserEntity  é essa dto que ira retornar na api
    public class UserDtoUpdateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime UpdateeAt { get; set; }
    }
}