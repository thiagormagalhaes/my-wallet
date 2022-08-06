using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWallet.Domain.Notifications;

namespace MyWallet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HaveNotification();
        }

        protected new ActionResult Response(object? result = null)
        {
            if (ValidOperation())
            {
                if (result is null)
                {
                    return Ok();
                }

                return Ok(result);
            }

            return BadRequest(_notifier.GetNotifications().Select(x => x.Message));
        }

        protected ActionResult CustomResult(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotifyInvalidModelError(modelState);
            }

            return Response();
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
