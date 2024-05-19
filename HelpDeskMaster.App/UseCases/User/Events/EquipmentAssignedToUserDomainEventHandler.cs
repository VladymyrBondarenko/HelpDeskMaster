using HelpDeskMaster.Domain.DomainEvents;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Exceptions.UserExceptions;
using HelpDeskMaster.Infrastracture.Mailing;
using MediatR;
using System.Text;

namespace HelpDeskMaster.App.UseCases.User.Events
{
    internal class EquipmentAssignedToUserDomainEventHandler : INotificationHandler<EquipmentAssignedToUserDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public EquipmentAssignedToUserDomainEventHandler(
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(EquipmentAssignedToUserDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(notification.UserId, cancellationToken);

            if(user == null)
            {
                throw new UserIsGoneException(notification.UserId);
            }

            var assginedEquipment = user.Equipments.FirstOrDefault(x => 
                x.EquipmentId == notification.EquipmentId);

            if(assginedEquipment == null)
            {
                throw new UserEquipmentIsGoneException(user.Id, notification.EquipmentId);
            }

            var assignedDate = notification.AssignedDate.UtcDateTime.ToShortDateString();

            var emailBodyBuilder = new StringBuilder();
            emailBodyBuilder.AppendLine($"Hello, {user.Login.Value}!.");
            emailBodyBuilder.AppendLine($"You have been assigned with following equipment at {assignedDate}:");
            emailBodyBuilder.AppendLine($"Equipment model - {assginedEquipment.Equipment?.Model}");
            emailBodyBuilder.AppendLine($"Equipment price - {assginedEquipment.Equipment?.Price}");

            var emailSendParams = new EmailSendParams(
                user.Login.Value,
                user.Login.Value,
                "New equipment is assigned to you",
                emailBodyBuilder.ToString());

            await _emailService.SendAsync(emailSendParams, cancellationToken);
        }
    }
}