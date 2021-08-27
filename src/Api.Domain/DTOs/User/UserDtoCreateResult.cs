using System;

namespace Api.Domain.DTOs.User
{
    //Dto para realizar update na Insert na user Entity, é essa dto que ira retornar na api
    public class UserDtoCreateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}