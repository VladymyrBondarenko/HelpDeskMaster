﻿using HelpDeskMaster.Domain.Abstractions;
using Ardalis.GuardClauses;

namespace HelpDeskMaster.Domain.Entities.Users
{
    public class User : Entity
    {
        private User() { }

        private User(Guid id, Login login, string? phoneNumber, DateTimeOffset createdAt) 
            : base(id, createdAt)
        {
            Login = Guard.Against.Null(login);
            PhoneNumber = phoneNumber;
        }

        public Login Login { get; private set; }

        public string? PhoneNumber { get; private set; }

        private readonly List<UserEquipment> _equipments = new();

        public IReadOnlyList<UserEquipment> Equipments => _equipments.ToList();

        public static User Create(Login login, string? phoneNumber)
        {
            return new User(Guid.NewGuid(), login, phoneNumber, DateTimeOffset.UtcNow);
        }

        public void UpdatePhoneNumber(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateLogin(string login)
        {
            Login = new Login(login);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public UserEquipment AssignEquipment(Guid equipmentId, DateTimeOffset assignDate)
        {
            var userEquipment = new UserEquipment(Guid.NewGuid(), DateTimeOffset.UtcNow,
                Id, equipmentId, assignDate);

            _equipments.Add(userEquipment);

            return userEquipment;
        }
    }
}