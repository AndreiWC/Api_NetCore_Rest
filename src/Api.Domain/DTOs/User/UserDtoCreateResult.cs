using System;

namespace Api.Domain.DTOs.User
{
    //Dto para realizar update na Insert na user Entity
    public class UserDtoCreateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Emai { get; set; }
        public DateTime CreateAt { get; set; }
    }
}